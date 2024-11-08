namespace WypelnianieSiatkiTrojkatow
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Canvas = new PictureBox();
            drawControlPtsCheck = new CheckBox();
            drawTriangleNetCheck = new CheckBox();
            drawFillingCheck = new CheckBox();
            netPrecisionTrack = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            alfaAngleTrack = new TrackBar();
            label3 = new Label();
            betaAngleTrack = new TrackBar();
            netPrecValue = new Label();
            alfaValue = new Label();
            betaValue = new Label();
            pointFileBtn = new Button();
            label4 = new Label();
            punktyPathValue = new Label();
            kdTrack = new TrackBar();
            label5 = new Label();
            kdValue = new Label();
            label6 = new Label();
            ksTrack = new TrackBar();
            ksValue = new Label();
            label7 = new Label();
            mValue = new Label();
            mTrack = new TrackBar();
            label8 = new Label();
            label9 = new Label();
            zTrack = new TrackBar();
            zValue = new Label();
            PauseResumeBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)Canvas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)netPrecisionTrack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)alfaAngleTrack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)betaAngleTrack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kdTrack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ksTrack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mTrack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)zTrack).BeginInit();
            SuspendLayout();
            // 
            // Canvas
            // 
            Canvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Canvas.BackColor = SystemColors.ControlLightLight;
            Canvas.Location = new Point(207, 1);
            Canvas.Name = "Canvas";
            Canvas.Size = new Size(875, 753);
            Canvas.TabIndex = 0;
            Canvas.TabStop = false;
            // 
            // drawControlPtsCheck
            // 
            drawControlPtsCheck.AutoSize = true;
            drawControlPtsCheck.Checked = true;
            drawControlPtsCheck.CheckState = CheckState.Checked;
            drawControlPtsCheck.Location = new Point(12, 12);
            drawControlPtsCheck.Name = "drawControlPtsCheck";
            drawControlPtsCheck.Size = new Size(180, 24);
            drawControlPtsCheck.TabIndex = 1;
            drawControlPtsCheck.Text = "Rysuj punkty kontrolne";
            drawControlPtsCheck.UseVisualStyleBackColor = true;
            drawControlPtsCheck.CheckedChanged += drawControlPtsCheck_CheckedChanged;
            // 
            // drawTriangleNetCheck
            // 
            drawTriangleNetCheck.AutoSize = true;
            drawTriangleNetCheck.Checked = true;
            drawTriangleNetCheck.CheckState = CheckState.Checked;
            drawTriangleNetCheck.Location = new Point(12, 42);
            drawTriangleNetCheck.Name = "drawTriangleNetCheck";
            drawTriangleNetCheck.Size = new Size(111, 24);
            drawTriangleNetCheck.TabIndex = 2;
            drawTriangleNetCheck.Text = "Rysuj siatke ";
            drawTriangleNetCheck.UseVisualStyleBackColor = true;
            drawTriangleNetCheck.CheckedChanged += drawTriangleNetCheck_CheckedChanged;
            // 
            // drawFillingCheck
            // 
            drawFillingCheck.AutoSize = true;
            drawFillingCheck.Checked = true;
            drawFillingCheck.CheckState = CheckState.Checked;
            drawFillingCheck.Location = new Point(12, 72);
            drawFillingCheck.Name = "drawFillingCheck";
            drawFillingCheck.Size = new Size(148, 24);
            drawFillingCheck.TabIndex = 3;
            drawFillingCheck.Text = "Rysuj wypelnienie";
            drawFillingCheck.UseVisualStyleBackColor = true;
            drawFillingCheck.CheckedChanged += drawFillingCheck_CheckedChanged;
            // 
            // netPrecisionTrack
            // 
            netPrecisionTrack.Location = new Point(12, 130);
            netPrecisionTrack.Maximum = 60;
            netPrecisionTrack.Minimum = 1;
            netPrecisionTrack.Name = "netPrecisionTrack";
            netPrecisionTrack.Size = new Size(180, 56);
            netPrecisionTrack.TabIndex = 4;
            netPrecisionTrack.TickStyle = TickStyle.None;
            netPrecisionTrack.Value = 12;
            netPrecisionTrack.Scroll += netPrecisionTrack_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 107);
            label1.Name = "label1";
            label1.Size = new Size(157, 20);
            label1.TabIndex = 5;
            label1.Text = "Dokladosc triangulacji";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 176);
            label2.Name = "label2";
            label2.Size = new Size(104, 20);
            label2.TabIndex = 6;
            label2.Text = "Kat alfa (Oś Z)";
            // 
            // alfaAngleTrack
            // 
            alfaAngleTrack.Location = new Point(11, 199);
            alfaAngleTrack.Maximum = 45;
            alfaAngleTrack.Minimum = -45;
            alfaAngleTrack.Name = "alfaAngleTrack";
            alfaAngleTrack.Size = new Size(180, 56);
            alfaAngleTrack.TabIndex = 7;
            alfaAngleTrack.TickStyle = TickStyle.None;
            alfaAngleTrack.Scroll += alfaAngleTrack_Scroll;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 244);
            label3.Name = "label3";
            label3.Size = new Size(109, 20);
            label3.TabIndex = 8;
            label3.Text = "Kat beta (Oś X)";
            // 
            // betaAngleTrack
            // 
            betaAngleTrack.Location = new Point(12, 267);
            betaAngleTrack.Maximum = 80;
            betaAngleTrack.Name = "betaAngleTrack";
            betaAngleTrack.Size = new Size(180, 56);
            betaAngleTrack.TabIndex = 9;
            betaAngleTrack.TickStyle = TickStyle.None;
            betaAngleTrack.Scroll += betaAngleTrack_Scroll;
            // 
            // netPrecValue
            // 
            netPrecValue.AutoSize = true;
            netPrecValue.Location = new Point(175, 107);
            netPrecValue.Name = "netPrecValue";
            netPrecValue.Size = new Size(17, 20);
            netPrecValue.TabIndex = 10;
            netPrecValue.Text = "n";
            // 
            // alfaValue
            // 
            alfaValue.AutoSize = true;
            alfaValue.Location = new Point(174, 176);
            alfaValue.Name = "alfaValue";
            alfaValue.Size = new Size(17, 20);
            alfaValue.TabIndex = 11;
            alfaValue.Text = "a";
            // 
            // betaValue
            // 
            betaValue.AutoSize = true;
            betaValue.Location = new Point(173, 244);
            betaValue.Name = "betaValue";
            betaValue.Size = new Size(18, 20);
            betaValue.TabIndex = 12;
            betaValue.Text = "b";
            // 
            // pointFileBtn
            // 
            pointFileBtn.Location = new Point(32, 712);
            pointFileBtn.Name = "pointFileBtn";
            pointFileBtn.Size = new Size(148, 29);
            pointFileBtn.TabIndex = 13;
            pointFileBtn.Text = "Wybierz plik";
            pointFileBtn.UseVisualStyleBackColor = true;
            pointFileBtn.Click += pointFileBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label4.Location = new Point(8, 664);
            label4.Name = "label4";
            label4.Size = new Size(115, 20);
            label4.TabIndex = 14;
            label4.Text = "Punkty sciezka:";
            // 
            // punktyPathValue
            // 
            punktyPathValue.AutoEllipsis = true;
            punktyPathValue.Location = new Point(12, 684);
            punktyPathValue.Name = "punktyPathValue";
            punktyPathValue.RightToLeft = RightToLeft.No;
            punktyPathValue.Size = new Size(189, 25);
            punktyPathValue.TabIndex = 15;
            punktyPathValue.Text = "Punkty\\punkty.txt";
            punktyPathValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // kdTrack
            // 
            kdTrack.Location = new Point(1088, 32);
            kdTrack.Name = "kdTrack";
            kdTrack.Size = new Size(207, 56);
            kdTrack.TabIndex = 16;
            kdTrack.TickStyle = TickStyle.None;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1088, 9);
            label5.Name = "label5";
            label5.Size = new Size(25, 20);
            label5.TabIndex = 17;
            label5.Text = "kd";
            // 
            // kdValue
            // 
            kdValue.AutoSize = true;
            kdValue.Location = new Point(1270, 12);
            kdValue.Name = "kdValue";
            kdValue.Size = new Size(17, 20);
            kdValue.TabIndex = 18;
            kdValue.Text = "0";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1088, 76);
            label6.Name = "label6";
            label6.Size = new Size(22, 20);
            label6.TabIndex = 19;
            label6.Text = "ks";
            // 
            // ksTrack
            // 
            ksTrack.Location = new Point(1088, 99);
            ksTrack.Name = "ksTrack";
            ksTrack.Size = new Size(199, 56);
            ksTrack.TabIndex = 20;
            ksTrack.TickStyle = TickStyle.None;
            // 
            // ksValue
            // 
            ksValue.AutoSize = true;
            ksValue.Location = new Point(1270, 76);
            ksValue.Name = "ksValue";
            ksValue.Size = new Size(17, 20);
            ksValue.TabIndex = 21;
            ksValue.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1088, 144);
            label7.Name = "label7";
            label7.Size = new Size(22, 20);
            label7.TabIndex = 22;
            label7.Text = "m";
            // 
            // mValue
            // 
            mValue.AutoSize = true;
            mValue.Location = new Point(1270, 144);
            mValue.Name = "mValue";
            mValue.Size = new Size(17, 20);
            mValue.TabIndex = 23;
            mValue.Text = "0";
            // 
            // mTrack
            // 
            mTrack.Location = new Point(1088, 167);
            mTrack.Name = "mTrack";
            mTrack.Size = new Size(199, 56);
            mTrack.TabIndex = 24;
            mTrack.TickStyle = TickStyle.None;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label8.Location = new Point(1088, 203);
            label8.Name = "label8";
            label8.Size = new Size(83, 20);
            label8.TabIndex = 25;
            label8.Text = "Animation";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(1097, 226);
            label9.Name = "label9";
            label9.Size = new Size(16, 20);
            label9.TabIndex = 26;
            label9.Text = "z";
            // 
            // zTrack
            // 
            zTrack.Location = new Point(1097, 249);
            zTrack.Name = "zTrack";
            zTrack.Size = new Size(190, 56);
            zTrack.TabIndex = 27;
            zTrack.TickStyle = TickStyle.None;
            // 
            // zValue
            // 
            zValue.AutoSize = true;
            zValue.Location = new Point(1270, 226);
            zValue.Name = "zValue";
            zValue.Size = new Size(17, 20);
            zValue.TabIndex = 28;
            zValue.Text = "0";
            // 
            // PauseResumeBtn
            // 
            PauseResumeBtn.Location = new Point(1152, 276);
            PauseResumeBtn.Name = "PauseResumeBtn";
            PauseResumeBtn.Size = new Size(94, 29);
            PauseResumeBtn.TabIndex = 29;
            PauseResumeBtn.Text = "Pause";
            PauseResumeBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1307, 753);
            Controls.Add(PauseResumeBtn);
            Controls.Add(zValue);
            Controls.Add(zTrack);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(mTrack);
            Controls.Add(mValue);
            Controls.Add(label7);
            Controls.Add(ksValue);
            Controls.Add(ksTrack);
            Controls.Add(label6);
            Controls.Add(kdValue);
            Controls.Add(label5);
            Controls.Add(kdTrack);
            Controls.Add(punktyPathValue);
            Controls.Add(label4);
            Controls.Add(pointFileBtn);
            Controls.Add(betaValue);
            Controls.Add(alfaValue);
            Controls.Add(netPrecValue);
            Controls.Add(betaAngleTrack);
            Controls.Add(label3);
            Controls.Add(alfaAngleTrack);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(drawFillingCheck);
            Controls.Add(drawTriangleNetCheck);
            Controls.Add(drawControlPtsCheck);
            Controls.Add(Canvas);
            Controls.Add(netPrecisionTrack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Wypelnianie siatki trojkatow";
            ((System.ComponentModel.ISupportInitialize)Canvas).EndInit();
            ((System.ComponentModel.ISupportInitialize)netPrecisionTrack).EndInit();
            ((System.ComponentModel.ISupportInitialize)alfaAngleTrack).EndInit();
            ((System.ComponentModel.ISupportInitialize)betaAngleTrack).EndInit();
            ((System.ComponentModel.ISupportInitialize)kdTrack).EndInit();
            ((System.ComponentModel.ISupportInitialize)ksTrack).EndInit();
            ((System.ComponentModel.ISupportInitialize)mTrack).EndInit();
            ((System.ComponentModel.ISupportInitialize)zTrack).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox Canvas;
        private CheckBox drawControlPtsCheck;
        private CheckBox drawTriangleNetCheck;
        private CheckBox drawFillingCheck;
        private TrackBar netPrecisionTrack;
        private Label label1;
        private Label label2;
        private TrackBar alfaAngleTrack;
        private Label label3;
        private TrackBar betaAngleTrack;
        private Label netPrecValue;
        private Label alfaValue;
        private Label betaValue;
        private Button pointFileBtn;
        private Label label4;
        private Label punktyPathValue;
        private TrackBar kdTrack;
        private Label label5;
        private Label kdValue;
        private Label label6;
        private TrackBar ksTrack;
        private Label ksValue;
        private Label label7;
        private Label mValue;
        private TrackBar mTrack;
        private Label label8;
        private Label label9;
        private TrackBar zTrack;
        private Label zValue;
        private Button PauseResumeBtn;
    }
}
