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
            ((System.ComponentModel.ISupportInitialize)Canvas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)netPrecisionTrack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)alfaAngleTrack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)betaAngleTrack).BeginInit();
            SuspendLayout();
            // 
            // Canvas
            // 
            Canvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Canvas.BackColor = SystemColors.ControlLightLight;
            Canvas.Location = new Point(207, 1);
            Canvas.Name = "Canvas";
            Canvas.Size = new Size(875, 703);
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
            label2.Location = new Point(12, 189);
            label2.Name = "label2";
            label2.Size = new Size(104, 20);
            label2.TabIndex = 6;
            label2.Text = "Kat alfa (Oś Z)";
            // 
            // alfaAngleTrack
            // 
            alfaAngleTrack.Location = new Point(12, 212);
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
            label3.Location = new Point(12, 271);
            label3.Name = "label3";
            label3.Size = new Size(109, 20);
            label3.TabIndex = 8;
            label3.Text = "Kat beta (Oś X)";
            // 
            // betaAngleTrack
            // 
            betaAngleTrack.Location = new Point(12, 294);
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
            netPrecValue.Location = new Point(94, 166);
            netPrecValue.Name = "netPrecValue";
            netPrecValue.Size = new Size(17, 20);
            netPrecValue.TabIndex = 10;
            netPrecValue.Text = "n";
            // 
            // alfaValue
            // 
            alfaValue.AutoSize = true;
            alfaValue.Location = new Point(94, 248);
            alfaValue.Name = "alfaValue";
            alfaValue.Size = new Size(17, 20);
            alfaValue.TabIndex = 11;
            alfaValue.Text = "a";
            // 
            // betaValue
            // 
            betaValue.AutoSize = true;
            betaValue.Location = new Point(93, 330);
            betaValue.Name = "betaValue";
            betaValue.Size = new Size(18, 20);
            betaValue.TabIndex = 12;
            betaValue.Text = "b";
            // 
            // pointFileBtn
            // 
            pointFileBtn.Location = new Point(31, 662);
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
            label4.Location = new Point(6, 605);
            label4.Name = "label4";
            label4.Size = new Size(115, 20);
            label4.TabIndex = 14;
            label4.Text = "Punkty sciezka:";
            // 
            // punktyPathValue
            // 
            punktyPathValue.AutoEllipsis = true;
            punktyPathValue.Location = new Point(12, 625);
            punktyPathValue.Name = "punktyPathValue";
            punktyPathValue.RightToLeft = RightToLeft.No;
            punktyPathValue.Size = new Size(189, 25);
            punktyPathValue.TabIndex = 15;
            punktyPathValue.Text = "Punkty\\punkty.txt";
            punktyPathValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 703);
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
    }
}
