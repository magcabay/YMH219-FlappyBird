namespace FlappyBird
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
            components = new System.ComponentModel.Container();
            oyunTimer = new System.Windows.Forms.Timer(components);
            btnYeniOyun = new Button();
            SuspendLayout();
            //
            // oyunTimer
            //
            oyunTimer.Interval = 20;
            oyunTimer.Tick += oyunTimer_Tick;
            //
            // btnYeniOyun
            //
            btnYeniOyun.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnYeniOyun.Location = new Point(372, 12);
            btnYeniOyun.Name = "btnYeniOyun";
            btnYeniOyun.Size = new Size(96, 32);
            btnYeniOyun.TabIndex = 0;
            btnYeniOyun.TabStop = false;
            btnYeniOyun.Text = "Yeni Oyun";
            btnYeniOyun.UseVisualStyleBackColor = true;
            btnYeniOyun.Click += btnYeniOyun_Click;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SkyBlue;
            ClientSize = new Size(480, 640);
            Controls.Add(btnYeniOyun);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flappy Bird - YMH219";
            Load += Form1_Load;
            Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer oyunTimer;
        private Button btnYeniOyun;
    }
}
