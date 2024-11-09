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
            label10 = new Label();
            pickLightColorBtn = new Button();
            lightColorPanel = new Panel();
            label11 = new Label();
            solidColorRBtn = new RadioButton();
            textureRBtn = new RadioButton();
            objectColorPanel = new Panel();
            pickObjectColorBtn = new Button();
            texturePathLabel = new Label();
            label13 = new Label();
            textureFileBtn = new Button();
            modifyNormalVecCheck = new CheckBox();
            normalVecPathLabel = new Label();
            label14 = new Label();
            normalVecFileBtn = new Button();
            label15 = new Label();
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
            alfaAngleTrack.Maximum = 90;
            alfaAngleTrack.Minimum = -90;
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
            betaAngleTrack.Maximum = 90;
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
            label4.Font = new Font("Segoe UI Light", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label4.Location = new Point(20, 664);
            label4.Name = "label4";
            label4.Size = new Size(87, 20);
            label4.TabIndex = 14;
            label4.Text = "Nazwa pliku:";
            // 
            // punktyPathValue
            // 
            punktyPathValue.AutoEllipsis = true;
            punktyPathValue.Location = new Point(20, 684);
            punktyPathValue.Name = "punktyPathValue";
            punktyPathValue.RightToLeft = RightToLeft.No;
            punktyPathValue.Size = new Size(181, 25);
            punktyPathValue.TabIndex = 15;
            punktyPathValue.Text = "Punkty\\punkty.txt";
            punktyPathValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // kdTrack
            // 
            kdTrack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            kdTrack.Location = new Point(1088, 32);
            kdTrack.Maximum = 100;
            kdTrack.Name = "kdTrack";
            kdTrack.Size = new Size(207, 56);
            kdTrack.TabIndex = 16;
            kdTrack.TickStyle = TickStyle.None;
            kdTrack.Value = 100;
            kdTrack.Scroll += kdTrack_Scroll;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(1088, 9);
            label5.Name = "label5";
            label5.Size = new Size(25, 20);
            label5.TabIndex = 17;
            label5.Text = "kd";
            // 
            // kdValue
            // 
            kdValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            kdValue.AutoSize = true;
            kdValue.Location = new Point(1250, 9);
            kdValue.Name = "kdValue";
            kdValue.Size = new Size(45, 20);
            kdValue.TabIndex = 18;
            kdValue.Text = "100%";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(1088, 76);
            label6.Name = "label6";
            label6.Size = new Size(22, 20);
            label6.TabIndex = 19;
            label6.Text = "ks";
            // 
            // ksTrack
            // 
            ksTrack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ksTrack.Location = new Point(1088, 99);
            ksTrack.Maximum = 100;
            ksTrack.Name = "ksTrack";
            ksTrack.Size = new Size(207, 56);
            ksTrack.TabIndex = 20;
            ksTrack.TickStyle = TickStyle.None;
            ksTrack.Value = 100;
            ksTrack.Scroll += ksTrack_Scroll;
            // 
            // ksValue
            // 
            ksValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ksValue.AutoSize = true;
            ksValue.Location = new Point(1250, 76);
            ksValue.Name = "ksValue";
            ksValue.Size = new Size(45, 20);
            ksValue.TabIndex = 21;
            ksValue.Text = "100%";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(1088, 144);
            label7.Name = "label7";
            label7.Size = new Size(22, 20);
            label7.TabIndex = 22;
            label7.Text = "m";
            // 
            // mValue
            // 
            mValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mValue.AutoSize = true;
            mValue.Location = new Point(1258, 144);
            mValue.Name = "mValue";
            mValue.Size = new Size(17, 20);
            mValue.TabIndex = 23;
            mValue.Text = "1";
            // 
            // mTrack
            // 
            mTrack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mTrack.Location = new Point(1088, 167);
            mTrack.Maximum = 100;
            mTrack.Minimum = 1;
            mTrack.Name = "mTrack";
            mTrack.Size = new Size(207, 56);
            mTrack.TabIndex = 24;
            mTrack.TickStyle = TickStyle.None;
            mTrack.Value = 1;
            mTrack.Scroll += mTrack_Scroll;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label8.Location = new Point(1088, 206);
            label8.Name = "label8";
            label8.Size = new Size(83, 20);
            label8.TabIndex = 25;
            label8.Text = "Animation";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Location = new Point(1097, 235);
            label9.Name = "label9";
            label9.Size = new Size(16, 20);
            label9.TabIndex = 26;
            label9.Text = "z";
            // 
            // zTrack
            // 
            zTrack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            zTrack.Location = new Point(1097, 258);
            zTrack.Maximum = 1000;
            zTrack.Name = "zTrack";
            zTrack.Size = new Size(198, 56);
            zTrack.TabIndex = 27;
            zTrack.TickStyle = TickStyle.None;
            zTrack.Value = 100;
            zTrack.Scroll += zTrack_Scroll;
            // 
            // zValue
            // 
            zValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            zValue.AutoSize = true;
            zValue.Location = new Point(1258, 235);
            zValue.Name = "zValue";
            zValue.Size = new Size(33, 20);
            zValue.TabIndex = 28;
            zValue.Text = "100";
            // 
            // PauseResumeBtn
            // 
            PauseResumeBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PauseResumeBtn.Location = new Point(1147, 304);
            PauseResumeBtn.Name = "PauseResumeBtn";
            PauseResumeBtn.Size = new Size(94, 29);
            PauseResumeBtn.TabIndex = 29;
            PauseResumeBtn.Text = "Pause";
            PauseResumeBtn.UseVisualStyleBackColor = true;
            PauseResumeBtn.Click += PauseResumeBtn_Click;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label10.Location = new Point(1088, 345);
            label10.Name = "label10";
            label10.Size = new Size(100, 20);
            label10.TabIndex = 30;
            label10.Text = "Kolor swiatla";
            // 
            // pickLightColorBtn
            // 
            pickLightColorBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pickLightColorBtn.Location = new Point(1219, 368);
            pickLightColorBtn.Name = "pickLightColorBtn";
            pickLightColorBtn.Size = new Size(66, 36);
            pickLightColorBtn.TabIndex = 31;
            pickLightColorBtn.Text = "Pick";
            pickLightColorBtn.UseVisualStyleBackColor = true;
            pickLightColorBtn.Click += pickLightColorBtn_Click;
            // 
            // lightColorPanel
            // 
            lightColorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lightColorPanel.BackColor = Color.White;
            lightColorPanel.BorderStyle = BorderStyle.FixedSingle;
            lightColorPanel.Location = new Point(1106, 368);
            lightColorPanel.Name = "lightColorPanel";
            lightColorPanel.Size = new Size(83, 36);
            lightColorPanel.TabIndex = 32;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label11.Location = new Point(1088, 428);
            label11.Name = "label11";
            label11.Size = new Size(104, 20);
            label11.TabIndex = 33;
            label11.Text = "Kolor obiektu";
            // 
            // solidColorRBtn
            // 
            solidColorRBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            solidColorRBtn.AutoSize = true;
            solidColorRBtn.Checked = true;
            solidColorRBtn.Location = new Point(1097, 451);
            solidColorRBtn.Name = "solidColorRBtn";
            solidColorRBtn.Size = new Size(100, 24);
            solidColorRBtn.TabIndex = 34;
            solidColorRBtn.TabStop = true;
            solidColorRBtn.Text = "Kolor stały";
            solidColorRBtn.UseVisualStyleBackColor = true;
            solidColorRBtn.CheckedChanged += solidColorRBtn_CheckedChanged;
            // 
            // textureRBtn
            // 
            textureRBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textureRBtn.AutoSize = true;
            textureRBtn.Location = new Point(1097, 523);
            textureRBtn.Name = "textureRBtn";
            textureRBtn.Size = new Size(84, 24);
            textureRBtn.TabIndex = 35;
            textureRBtn.Text = "Tekstura";
            textureRBtn.UseVisualStyleBackColor = true;
            textureRBtn.CheckedChanged += textureRBtn_CheckedChanged;
            // 
            // objectColorPanel
            // 
            objectColorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            objectColorPanel.BackColor = Color.Purple;
            objectColorPanel.BorderStyle = BorderStyle.FixedSingle;
            objectColorPanel.Location = new Point(1105, 481);
            objectColorPanel.Name = "objectColorPanel";
            objectColorPanel.Size = new Size(83, 36);
            objectColorPanel.TabIndex = 36;
            // 
            // pickObjectColorBtn
            // 
            pickObjectColorBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pickObjectColorBtn.Location = new Point(1225, 481);
            pickObjectColorBtn.Name = "pickObjectColorBtn";
            pickObjectColorBtn.Size = new Size(66, 36);
            pickObjectColorBtn.TabIndex = 37;
            pickObjectColorBtn.Text = "Pick";
            pickObjectColorBtn.UseVisualStyleBackColor = true;
            pickObjectColorBtn.Click += pickObjectColorBtn_Click;
            // 
            // texturePathLabel
            // 
            texturePathLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            texturePathLabel.AutoEllipsis = true;
            texturePathLabel.Location = new Point(1106, 570);
            texturePathLabel.Name = "texturePathLabel";
            texturePathLabel.RightToLeft = RightToLeft.No;
            texturePathLabel.Size = new Size(189, 25);
            texturePathLabel.TabIndex = 40;
            texturePathLabel.Text = "Punkty\\punkty.txt";
            texturePathLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI Light", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label13.Location = new Point(1105, 550);
            label13.Name = "label13";
            label13.Size = new Size(87, 20);
            label13.TabIndex = 39;
            label13.Text = "Nazwa pliku:";
            // 
            // textureFileBtn
            // 
            textureFileBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textureFileBtn.Location = new Point(1127, 598);
            textureFileBtn.Name = "textureFileBtn";
            textureFileBtn.Size = new Size(137, 29);
            textureFileBtn.TabIndex = 38;
            textureFileBtn.Text = "Wybierz plik";
            textureFileBtn.UseVisualStyleBackColor = true;
            textureFileBtn.Click += textureFileBtn_Click;
            // 
            // modifyNormalVecCheck
            // 
            modifyNormalVecCheck.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            modifyNormalVecCheck.AutoSize = true;
            modifyNormalVecCheck.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            modifyNormalVecCheck.Location = new Point(1085, 640);
            modifyNormalVecCheck.Name = "modifyNormalVecCheck";
            modifyNormalVecCheck.Size = new Size(226, 24);
            modifyNormalVecCheck.TabIndex = 41;
            modifyNormalVecCheck.Text = "Modyfikuj wektor normalny";
            modifyNormalVecCheck.UseVisualStyleBackColor = true;
            // 
            // normalVecPathLabel
            // 
            normalVecPathLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            normalVecPathLabel.AutoEllipsis = true;
            normalVecPathLabel.Location = new Point(1106, 687);
            normalVecPathLabel.Name = "normalVecPathLabel";
            normalVecPathLabel.RightToLeft = RightToLeft.No;
            normalVecPathLabel.Size = new Size(189, 25);
            normalVecPathLabel.TabIndex = 44;
            normalVecPathLabel.Text = "Punkty\\punkty.txt";
            normalVecPathLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI Light", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label14.Location = new Point(1105, 667);
            label14.Name = "label14";
            label14.Size = new Size(87, 20);
            label14.TabIndex = 43;
            label14.Text = "Nazwa pliku:";
            // 
            // normalVecFileBtn
            // 
            normalVecFileBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            normalVecFileBtn.Location = new Point(1127, 715);
            normalVecFileBtn.Name = "normalVecFileBtn";
            normalVecFileBtn.Size = new Size(137, 29);
            normalVecFileBtn.TabIndex = 42;
            normalVecFileBtn.Text = "Wybierz plik";
            normalVecFileBtn.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label15.Location = new Point(11, 644);
            label15.Name = "label15";
            label15.Size = new Size(58, 20);
            label15.TabIndex = 45;
            label15.Text = "Punkty";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1307, 753);
            Controls.Add(label15);
            Controls.Add(normalVecPathLabel);
            Controls.Add(label14);
            Controls.Add(normalVecFileBtn);
            Controls.Add(modifyNormalVecCheck);
            Controls.Add(texturePathLabel);
            Controls.Add(label13);
            Controls.Add(textureFileBtn);
            Controls.Add(pickObjectColorBtn);
            Controls.Add(objectColorPanel);
            Controls.Add(textureRBtn);
            Controls.Add(solidColorRBtn);
            Controls.Add(label11);
            Controls.Add(lightColorPanel);
            Controls.Add(pickLightColorBtn);
            Controls.Add(label10);
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
        private Label label10;
        private Button pickLightColorBtn;
        private Panel lightColorPanel;
        private Label label11;
        private RadioButton solidColorRBtn;
        private RadioButton textureRBtn;
        private Panel objectColorPanel;
        private Button pickObjectColorBtn;
        private Label texturePathLabel;
        private Label label13;
        private Button textureFileBtn;
        private CheckBox modifyNormalVecCheck;
        private Label normalVecPathLabel;
        private Label label14;
        private Button normalVecFileBtn;
        private Label label15;
    }
}
