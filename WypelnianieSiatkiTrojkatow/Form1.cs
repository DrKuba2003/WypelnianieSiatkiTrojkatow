using FastBitmapLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using System.Xml.Serialization;
using WypelnianieSiatkiTrojkatow.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WypelnianieSiatkiTrojkatow
{
    public partial class Form1 : Form
    {
        private const string DEFAULT_PTS = "Resources\\Points\\punkty2.txt";
        private const string DEFAULT_TEXTURE = "Resources\\Textures\\154.jpg";
        private const string DEFAULT_NORMAL_VEC = "Resources\\NormalVectors\\154_norm.jpg";

        private int width, height;
        private BackgroundWorker drawBW;
        private bool restartDrawing = false;
        private BufferedGraphicsContext bufferGContext;
        private BufferedGraphics bufferG;

        private Model model;
        private DrawingParams drawingParams;

        // todo add show drawing in progrss and drawing restart btn
        // todo add drawing z filtering?
        // todo skalowanie punktow kontrolnych
        // todo parametry animacji

        public Form1()
        {
            InitializeComponent();

            SetLabelsText();

            width = panel1.Width;
            height = panel1.Height;

            bufferGContext = BufferedGraphicsManager.Current;
            bufferGContext.MaximumBuffer = new Size(width + 1, height + 1);
            bufferG = bufferGContext.Allocate(panel1.CreateGraphics(),
                new Rectangle(0, 0, width, height));

            // Zmiana ukladu wspolrzednych
            bufferG.Graphics.ScaleTransform(1, -1);
            bufferG.Graphics.TranslateTransform(width / 2, -height / 2);

            model = new Model(DEFAULT_PTS, netPrecisionTrack.Value,
                alfaAngleTrack.Value, betaAngleTrack.Value,
                zTrack.Value, animationBw_ProgressChanged);

            drawingParams = new DrawingParams(ReadKd(), ReadKs(),
                mTrack.Value,
                ColorToVector3(objectColorPanel.BackColor),
                ColorToVector3(lightColorPanel.BackColor),
                drawFillingCheck.Checked,
                solidColorRBtn.Checked,
                modifyNormalVecCheck.Checked,
                drawTriangleNetCheck.Checked,
                drawControlPtsCheck.Checked,
                drawLightPosCheck.Checked);

            LoadTexture(DEFAULT_TEXTURE);
            LoadNormalVectors(DEFAULT_NORMAL_VEC);

            drawBW = new BackgroundWorker();
            drawBW.WorkerSupportsCancellation = true;
            drawBW.DoWork += draw_DoWork;
            drawBW.RunWorkerCompleted += drawBw_Completed;

            Draw();
        }

        private void SetLabelsText()
        {
            netPrecValue.Text = netPrecisionTrack.Value.ToString();
            alfaValue.Text = alfaAngleTrack.Value.ToString();
            betaValue.Text = betaAngleTrack.Value.ToString();
            kdValue.Text = $"{kdTrack.Value}%";
            ksValue.Text = $"{ksTrack.Value}%";
            mValue.Text = mTrack.Value.ToString();
            zValue.Text = zTrack.Value.ToString();
            punktyPathValue.Text = DEFAULT_PTS.Split('\\')[2];
            texturePathLabel.Text = DEFAULT_TEXTURE.Split('\\')[2];
            normalVecPathLabel.Text = DEFAULT_NORMAL_VEC.Split('\\')[2];
            PauseResumeBtn.Text = "Resume";
        }

        #region Drawing

        public void DrawToBuffer(Graphics g, Func<bool> isCancelled)
        {
            if (isCancelled()) return;
            g.Clear(Color.WhiteSmoke);

            if (drawingParams.isDrawFilling)
            {
                if (isCancelled()) return;

                Bitmap bmp = new Bitmap(width, height);
                if (isCancelled()) return;

                DrawingUtil.FillMesh(bmp, model, drawingParams,
                    TranslatePtsToBitmap, isCancelled);
                if (isCancelled()) return;

                g.DrawImage(bmp, -width / 2, -height / 2);
            }

            if (drawingParams.isDrawMesh)
                DrawingUtil.DrawTriangles(g, model.Mesh, isCancelled);

            if (drawingParams.isDrawControlPts)
                DrawingUtil.DrawControlPts(g, model.ControlVertexes, isCancelled);

            if (drawingParams.isDrawLightPos)
                g.FillEllipse(Brushes.Gold, new Rectangle((int)model.lightPos.X - 5,
                    (int)model.lightPos.Y - 5, 10, 10));
        }

        public void Draw()
        {
            if (restartDrawing) return;

            if (drawBW.IsBusy)
            {
                drawBW.CancelAsync();
                restartDrawing = true;
            }
            else
                drawBW.RunWorkerAsync();
        }

        private void draw_DoWork(Object sender, DoWorkEventArgs e)
        {
            DrawToBuffer(bufferG.Graphics, () => drawBW.CancellationPending);
        }

        private void drawBw_Completed(Object sender, RunWorkerCompletedEventArgs e)
        {
            if (restartDrawing)
            {
                drawBW.RunWorkerAsync();
                restartDrawing = false;
            }
            else
                panel1.Refresh();
        }

        private (int, int) TranslatePtsToBitmap(int x, int y)
            => (x + width / 2,
                y + height / 2);

        private Vector3 ColorToVector3(Color color)
            => new Vector3(
                    color.R / 255F,
                    color.G / 255F,
                    color.B / 255F);

        #endregion

        #region Animation

        private void animationBw_ProgressChanged(object sender, ProgressChangedEventArgs e) => Draw();

        #endregion

        #region LoadData

        private void LoadTexture(string path)
        {
            using (Stream bmpStream = File.Open(path, FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                Bitmap texture = new Bitmap(image);
                int width = texture.Width;
                int height = texture.Height;
                drawingParams.textureArr = new Color[width, height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        drawingParams.textureArr[x, y] = texture.GetPixel(x, y);
                    }
                }
            }
        }

        private void LoadNormalVectors(string path)
        {
            using (Stream bmpStream = File.Open(path, FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                Bitmap normalMap = new Bitmap(image);
                int width = normalMap.Width;
                int height = normalMap.Height;
                drawingParams.normalMapArr = new Color[width, height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        drawingParams.normalMapArr[x, y] = normalMap.GetPixel(x, y);
                    }
                }
            }
        }

        #endregion

        #region ControlsEvents

        private void drawControlPtsCheck_CheckedChanged(object sender, EventArgs e)
        {
            drawingParams.isDrawControlPts = drawControlPtsCheck.Checked;
            Draw();
        }
        private void drawTriangleNetCheck_CheckedChanged(object sender, EventArgs e)
        {
            drawingParams.isDrawMesh = drawTriangleNetCheck.Checked;
            Draw();
        }
        private void drawFillingCheck_CheckedChanged(object sender, EventArgs e)
        {
            drawingParams.isDrawFilling = drawFillingCheck.Checked;
            Draw();
        }
        private void modifyNormalVecCheck_CheckedChanged(object sender, EventArgs e)
        {
            drawingParams.isModifyNormalVec = modifyNormalVecCheck.Checked;
            Draw();
        }
        private void solidColorRBtn_CheckedChanged(object sender, EventArgs e)
        {
            drawingParams.isSolidColor = solidColorRBtn.Checked;
            Draw();
        }
        private void textureRBtn_CheckedChanged(object sender, EventArgs e)
        {
            drawingParams.isSolidColor = solidColorRBtn.Checked;
            Draw();
        }
        private void drawLightPosCheck_CheckedChanged(object sender, EventArgs e)
        {
            drawingParams.isDrawLightPos = drawLightPosCheck.Checked;
            Draw();
        }

        private void netPrecisionTrack_Scroll(object sender, EventArgs e)
        {
            netPrecValue.Text = netPrecisionTrack.Value.ToString();
            model.LoadMesh(netPrecisionTrack.Value);
            model.RotateVertexes();
            Draw();
        }

        private void alfaAngleTrack_Scroll(object sender, EventArgs e)
        {
            alfaValue.Text = alfaAngleTrack.Value.ToString();
            model.alfa = alfaAngleTrack.Value;
            model.RotateVertexes();

            Draw();
        }

        private void betaAngleTrack_Scroll(object sender, EventArgs e)
        {
            betaValue.Text = betaAngleTrack.Value.ToString();
            model.beta = betaAngleTrack.Value;
            model.RotateVertexes();

            Draw();
        }

        private void kdTrack_Scroll(object sender, EventArgs e)
        {
            kdValue.Text = $"{kdTrack.Value}%";
            drawingParams.kd = kdTrack.Value / 100F;
            Draw();
        }

        private void ksTrack_Scroll(object sender, EventArgs e)
        {
            ksValue.Text = $"{ksTrack.Value}%";
            drawingParams.ks = ksTrack.Value / 100F;
            Draw();
        }

        private void mTrack_Scroll(object sender, EventArgs e)
        {
            mValue.Text = mTrack.Value.ToString();
            drawingParams.m = mTrack.Value;
            Draw();
        }

        private void zTrack_Scroll(object sender, EventArgs e)
        {
            zValue.Text = zTrack.Value.ToString();
            model.SetLightZ(zTrack.Value);
            Draw();
        }

        private void PauseResumeBtn_Click(object sender, EventArgs e)
        {
            bool isAnimationRunning = model.PauseResumeAnimation();
            PauseResumeBtn.Text = isAnimationRunning ? "Pause" : "Resume";
        }

        private void pickLightColorBtn_Click(object sender, EventArgs e)
        {
            using (ColorDialog MyDialog = new ColorDialog())
            {
                MyDialog.AllowFullOpen = true;
                MyDialog.Color = lightColorPanel.BackColor;

                // Update the text box color if the user clicks OK 
                if (MyDialog.ShowDialog() == DialogResult.OK)
                {
                    lightColorPanel.BackColor = MyDialog.Color;
                    drawingParams.lightColor = ColorToVector3(MyDialog.Color);
                    Draw();
                }
            }
        }

        private void pickObjectColorBtn_Click(object sender, EventArgs e)
        {
            using (ColorDialog MyDialog = new ColorDialog())
            {
                MyDialog.AllowFullOpen = true;
                MyDialog.Color = objectColorPanel.BackColor;

                // Update the text box color if the user clicks OK 
                if (MyDialog.ShowDialog() == DialogResult.OK)
                {
                    objectColorPanel.BackColor = MyDialog.Color;
                    drawingParams.objectColor = ColorToVector3(MyDialog.Color);
                    Draw();
                }
            }
        }

        private void pointFileBtn_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = Directory.GetCurrentDirectory() +
                    "\\Resources\\Points";
                fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                fileDialog.FilterIndex = 0;
                fileDialog.Multiselect = false;
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = fileDialog.FileName;
                    var arr = path.Split('\\');
                    punktyPathValue.Text = arr[arr.Length - 1];

                    model.CalculateModel(path, netPrecisionTrack.Value);

                    Draw();
                }
            }
        }

        private void textureFileBtn_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = Directory.GetCurrentDirectory() +
                    "\\Resources\\Textures";
                fileDialog.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|All files (*.*)|*.*";
                fileDialog.FilterIndex = 0;
                fileDialog.Multiselect = false;
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = fileDialog.FileName;
                    var arr = path.Split('\\');
                    texturePathLabel.Text = arr[arr.Length - 1];

                    LoadTexture(path);

                    Draw();
                }
            }
        }

        private void normalVecFileBtn_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = Directory.GetCurrentDirectory() +
                    "\\Resources\\NormalVectors";
                fileDialog.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|All files (*.*)|*.*";
                fileDialog.FilterIndex = 4;
                fileDialog.Multiselect = false;
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = fileDialog.FileName;
                    var arr = path.Split('\\');
                    normalVecPathLabel.Text = arr[arr.Length - 1];

                    LoadNormalVectors(path);

                    Draw();
                }
            }
        }

        private void resetLightXYBtn_Click(object sender, EventArgs e)
        {
            model.ResetLightPos();

            Draw();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            model.StopAnimation();
            restartDrawing = false;
            drawBW.CancelAsync();
            Thread.Sleep(100);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            bufferG.Render(e.Graphics);
        }

        #endregion

        #region ControlReadValue

        private float ReadKd()
            => kdTrack.Value / 100f;

        private float ReadKs()
            => ksTrack.Value / 100f;

        //delegate int GetSliderValueCallback(System.Windows.Forms.TrackBar trackBar);
        //private int GetTrackBarValue(System.Windows.Forms.TrackBar trackBar)
        //{
        //    if (trackBar.InvokeRequired)
        //    {
        //        GetSliderValueCallback cb = new GetSliderValueCallback(GetTrackBarValue);
        //        return (int)trackBar.Invoke(cb, trackBar);
        //    }
        //    else
        //    {
        //        return (int)trackBar.Value;
        //    }
        //}

        //delegate Color GetPanelColorCallback(Panel panel);
        //private Color GetPanelColor(Panel panel)
        //{
        //    if (panel.InvokeRequired)
        //    {
        //        GetPanelColorCallback cb = new GetPanelColorCallback(GetPanelColor);
        //        return (Color)panel.Invoke(cb, panel);
        //    }
        //    else
        //    {
        //        return panel.BackColor;
        //    }
        //}

        #endregion

    }
}
