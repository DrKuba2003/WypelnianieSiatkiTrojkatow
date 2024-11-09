using FastBitmapLib;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.Serialization;
using WypelnianieSiatkiTrojkatow.Utils;

namespace WypelnianieSiatkiTrojkatow
{
    public partial class Form1 : Form
    {
        private const string DEFAULT_PTS = "Punkty\\punkty4.txt";

        private Bitmap drawArea;
        private Model model;

        public Form1()
        {
            InitializeComponent();

            // set labels text
            SetLabels();

            drawArea = new Bitmap(Canvas.Size.Width, Canvas.Size.Height);
            Canvas.Image = drawArea;

            model = new Model(DEFAULT_PTS, netPrecisionTrack.Value,
                alfaAngleTrack.Value, betaAngleTrack.Value);

            Draw();
        }

        public void Draw()
        {
            using (Graphics g = Graphics.FromImage(drawArea))
            {
                g.Clear(Color.WhiteSmoke);

                // Zmiana ukladu wspolrzednych
                g.ScaleTransform(1, -1);
                g.TranslateTransform(Canvas.Width / 2, -Canvas.Height / 2);

                if (drawFillingCheck.Checked)
                {
                    float kd = kdTrack.Value / 100F;
                    float ks = ksTrack.Value / 100F;
                    int m = mTrack.Value;
                    var objectC = new Vector3(
                        objectColorPanel.BackColor.R / 255F,
                        objectColorPanel.BackColor.G / 255F,
                        objectColorPanel.BackColor.B / 255F);
                    var light = new Vector3(0, 0, zTrack.Value);
                    var lightC = new Vector3(
                        lightColorPanel.BackColor.R / 255F,
                        lightColorPanel.BackColor.G / 255F,
                        lightColorPanel.BackColor.B / 255F);

                    DrawingUtil.FillMesh(drawArea, model.Mesh, kd, ks, m,
                        objectC, light, lightC, TranslatePtsToBitmap);
                }

                if (drawTriangleNetCheck.Checked)
                    DrawingUtil.DrawTriangles(g, model.Mesh);

                if (drawControlPtsCheck.Checked)
                    DrawingUtil.DrawControlPts(g, model.ControlVertexes);

            }
            Canvas.Refresh();
        }

        private void SetLabels()
        {
            netPrecValue.Text = netPrecisionTrack.Value.ToString();
            alfaValue.Text = alfaAngleTrack.Value.ToString();
            betaValue.Text = betaAngleTrack.Value.ToString();
            punktyPathValue.Text = DEFAULT_PTS;
            kdValue.Text = $"{kdTrack.Value}%";
            ksValue.Text = $"{ksTrack.Value}%";
            mValue.Text = mTrack.Value.ToString();
            zValue.Text = zTrack.Value.ToString();
        }

        private (int, int) TranslatePtsToBitmap(int x, int y)
            => (x + Canvas.Width / 2,
                -y + Canvas.Height / 2);


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

        private void pointFileBtn_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = Directory.GetCurrentDirectory() + "\\Punkty";
                fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                fileDialog.FilterIndex = 0;
                fileDialog.Multiselect = false;
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = fileDialog.FileName;
                    punktyPathValue.Text = path;

                    model.CalculateModel(path, netPrecisionTrack.Value,
                        alfaAngleTrack.Value, betaAngleTrack.Value);

                    Draw();
                }
            }
        }

        private void PauseResumeBtn_Click(object sender, EventArgs e)
        {

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
    }
}
