using FastBitmapLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using WypelnianieSiatkiTrojkatow.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WypelnianieSiatkiTrojkatow
{
    public partial class Form1 : Form
    {
        private const string DEFAULT_PTS = "Resources\\Points\\punkty2.txt";
        private const string DEFAULT_TEXTURE = "Resources\\Textures\\pexels-steve-1509534.jpg";

        private Vector3 lightPos;
        private double radiuss = 50;
        private bool isAnimationRunnig = false;
        private BackgroundWorker animationBw;

        private Bitmap drawArea;
        private Bitmap buffer;
        private Model model;
        private Bitmap texture;

        public Form1()
        {
            InitializeComponent();

            // set labels text
            SetLabels();

            drawArea = new Bitmap(Canvas.Size.Width, Canvas.Size.Height);
            Canvas.Image = drawArea;

            model = new Model(DEFAULT_PTS, netPrecisionTrack.Value,
                alfaAngleTrack.Value, betaAngleTrack.Value);

            LoadTexture(DEFAULT_TEXTURE);

            lightPos = new Vector3(50, 0, zTrack.Value);
            animationBw = new BackgroundWorker();
            animationBw.DoWork += Animation;
            animationBw.ProgressChanged += animationBw_ProgressChanged;
            animationBw.WorkerReportsProgress = true;

            Draw();
        }

        public void Draw(Bitmap? bitmap=null)
        {
            Bitmap b = bitmap is null ? drawArea : bitmap;
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Color.WhiteSmoke);

                // Zmiana ukladu wspolrzednych
                g.ScaleTransform(1, -1);
                g.TranslateTransform(Canvas.Width / 2, -Canvas.Height / 2);

                if (drawFillingCheck.Checked)
                {
                    float kd = GetTrackBarValue(kdTrack) / 100F;
                    float ks = GetTrackBarValue(ksTrack) / 100F;
                    int m = GetTrackBarValue(mTrack);
                    Func<float, float, float, Vector3> ObjectColor;
                    if (solidColorRBtn.Checked)
                    {
                        Color objectColor = GetPanelColor(objectColorPanel);
                        var objectC = new Vector3(
                            objectColor.R / 255F,
                            objectColor.G / 255F,
                            objectColor.B / 255F);
                        ObjectColor = (u, v, w) => objectC;
                    }
                    else
                    {
                        ObjectColor = (u, v, w) =>
                        {
                            if (u is float.NaN) u = 0;
                            if (v is float.NaN) v = 0;

                            Color c = texture.GetPixel(
                                v <= 0 ? 1 : v >= 1 ? texture.Width - 1 :
                                    (int)(v * texture.Width),
                                u <= 0 ? 1 : u >= 1 ? texture.Height - 1 :
                                    (int)(u * texture.Height));
                            return new Vector3(
                                c.R / 255F,
                                c.G / 255F,
                                c.B / 255F);
                        };
                    }
                    Color lightColor = GetPanelColor(lightColorPanel);
                    var lightC = new Vector3(
                        lightColor.R / 255F,
                        lightColor.G / 255F,
                        lightColor.B / 255F);

                    DrawingUtil.FillMesh(drawArea, model.Mesh, kd, ks, m,
                        lightPos, lightC, ObjectColor, TranslatePtsToBitmap);
                }

                if (drawTriangleNetCheck.Checked)
                    DrawingUtil.DrawTriangles(g, model.Mesh);

                if (drawControlPtsCheck.Checked)
                    DrawingUtil.DrawControlPts(g, model.ControlVertexes);

                g.FillEllipse(Brushes.Gold, new Rectangle((int)lightPos.X - 5, (int)lightPos.Y - 5, 10, 10));
            }

            if (bitmap is not null)
                drawArea = buffer; 
            CanvasRefresh();
        }

        private void Animation(Object sender, DoWorkEventArgs e)
        {
            double angles = 0,
                 angleSpeed = 5,
                 rSpeed = 1;

            while (isAnimationRunnig)
            {
                angles = (angles + angleSpeed) % 360;
                radiuss += rSpeed;

                lightPos.X = (float)(radiuss * Math.Cos(MathUtil.ToRadians(angles)));
                lightPos.Y = (float)(radiuss * Math.Sin(MathUtil.ToRadians(angles)));

                animationBw.ReportProgress(0);
                Thread.Sleep(200);
            }
        }

        private void animationBw_ProgressChanged(object sender, ProgressChangedEventArgs e) => Draw();

        delegate int GetSliderValueCallback(System.Windows.Forms.TrackBar trackBar);
        private int GetTrackBarValue(System.Windows.Forms.TrackBar trackBar)
        {
            if (trackBar.InvokeRequired)
            {
                GetSliderValueCallback cb = new GetSliderValueCallback(GetTrackBarValue);
                return (int)trackBar.Invoke(cb, trackBar);
            }
            else
            {
                return (int)trackBar.Value;
            }
        }

        delegate Color GetPanelColorCallback(Panel panel);
        private Color GetPanelColor(Panel panel)
        {
            if (panel.InvokeRequired)
            {
                GetPanelColorCallback cb = new GetPanelColorCallback(GetPanelColor);
                return (Color)panel.Invoke(cb, panel);
            }
            else
            {
                return panel.BackColor;
            }
        }

        delegate void GetCanvasRefreshCallback();
        private void CanvasRefresh()
        {
            if (Canvas.InvokeRequired)
            {
                GetCanvasRefreshCallback cb = new GetCanvasRefreshCallback(CanvasRefresh);
                Canvas.Invoke(cb);
            }
            else
            {
                Canvas.Refresh();
            }
        }

        private void SetLabels()
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
            PauseResumeBtn.Text = "Resume";
        }

        private (int, int) TranslatePtsToBitmap(int x, int y)
            => (x + Canvas.Width / 2,
                -y + Canvas.Height / 2);

        private void LoadTexture(string path)
        {
            using (Stream bmpStream = File.Open(path, FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                texture = new Bitmap(image);
            }
        }

        private void drawControlPtsCheck_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void drawTriangleNetCheck_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void drawFillingCheck_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void netPrecisionTrack_Scroll(object sender, EventArgs e)
        {
            netPrecValue.Text = netPrecisionTrack.Value.ToString();
            model.LoadMesh(netPrecisionTrack.Value);
            model.RotateVertexes(alfaAngleTrack.Value, betaAngleTrack.Value);
            Draw();
        }

        private void alfaAngleTrack_Scroll(object sender, EventArgs e)
        {
            alfaValue.Text = alfaAngleTrack.Value.ToString();
            model.RotateVertexes(alfaAngleTrack.Value, betaAngleTrack.Value);

            Draw();
        }

        private void betaAngleTrack_Scroll(object sender, EventArgs e)
        {
            betaValue.Text = betaAngleTrack.Value.ToString();
            model.RotateVertexes(alfaAngleTrack.Value, betaAngleTrack.Value);

            Draw();
        }

        private void kdTrack_Scroll(object sender, EventArgs e)
        {
            kdValue.Text = $"{kdTrack.Value}%";
            Draw();
        }

        private void ksTrack_Scroll(object sender, EventArgs e)
        {
            ksValue.Text = $"{ksTrack.Value}%";
            Draw();
        }

        private void mTrack_Scroll(object sender, EventArgs e)
        {
            mValue.Text = mTrack.Value.ToString();
            Draw();
        }

        private void zTrack_Scroll(object sender, EventArgs e)
        {
            zValue.Text = zTrack.Value.ToString();
            Draw();
        }

        private void solidColorRBtn_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void textureRBtn_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
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

                    model.CalculateModel(path, netPrecisionTrack.Value,
                        alfaAngleTrack.Value, betaAngleTrack.Value);

                    Draw();
                }
            }
        }

        private void PauseResumeBtn_Click(object sender, EventArgs e)
        {
            if (isAnimationRunnig)
            {
                PauseResumeBtn.Text = "Resume";
                isAnimationRunnig = false;
            }
            else
            {
                PauseResumeBtn.Text = "Pause";
                isAnimationRunnig = true;
                animationBw.RunWorkerAsync();
            }
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

        private void resetLightXYBtn_Click(object sender, EventArgs e)
        {
            radiuss = 50;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            isAnimationRunnig = false;
        }
    }
}
