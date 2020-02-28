namespace BejeweledBot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picGameImage = new System.Windows.Forms.PictureBox();
            this.btnTakeCapture = new System.Windows.Forms.Button();
            this.txtGameLog = new System.Windows.Forms.TextBox();
            this.comboGametype = new System.Windows.Forms.ComboBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.checkAutoResetGame = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picGameImage)).BeginInit();
            this.SuspendLayout();
            // 
            // picGameImage
            // 
            this.picGameImage.Location = new System.Drawing.Point(12, 41);
            this.picGameImage.Name = "picGameImage";
            this.picGameImage.Size = new System.Drawing.Size(640, 480);
            this.picGameImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picGameImage.TabIndex = 1;
            this.picGameImage.TabStop = false;
            // 
            // btnTakeCapture
            // 
            this.btnTakeCapture.Location = new System.Drawing.Point(12, 12);
            this.btnTakeCapture.Name = "btnTakeCapture";
            this.btnTakeCapture.Size = new System.Drawing.Size(128, 23);
            this.btnTakeCapture.TabIndex = 2;
            this.btnTakeCapture.Text = "Initalize";
            this.btnTakeCapture.UseVisualStyleBackColor = true;
            this.btnTakeCapture.Click += new System.EventHandler(this.btnTakeCapture_Click);
            // 
            // txtGameLog
            // 
            this.txtGameLog.Location = new System.Drawing.Point(12, 527);
            this.txtGameLog.MaxLength = 0;
            this.txtGameLog.Multiline = true;
            this.txtGameLog.Name = "txtGameLog";
            this.txtGameLog.ReadOnly = true;
            this.txtGameLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGameLog.Size = new System.Drawing.Size(640, 128);
            this.txtGameLog.TabIndex = 3;
            // 
            // comboGametype
            // 
            this.comboGametype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGametype.Enabled = false;
            this.comboGametype.FormattingEnabled = true;
            this.comboGametype.Location = new System.Drawing.Point(278, 14);
            this.comboGametype.Name = "comboGametype";
            this.comboGametype.Size = new System.Drawing.Size(146, 21);
            this.comboGametype.TabIndex = 4;
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.Location = new System.Drawing.Point(146, 12);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(126, 23);
            this.btnPlay.TabIndex = 5;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // checkAutoResetGame
            // 
            this.checkAutoResetGame.AutoSize = true;
            this.checkAutoResetGame.Enabled = false;
            this.checkAutoResetGame.Location = new System.Drawing.Point(430, 18);
            this.checkAutoResetGame.Name = "checkAutoResetGame";
            this.checkAutoResetGame.Size = new System.Drawing.Size(110, 17);
            this.checkAutoResetGame.TabIndex = 6;
            this.checkAutoResetGame.Text = "Auto Reset Game";
            this.checkAutoResetGame.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 664);
            this.Controls.Add(this.checkAutoResetGame);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.comboGametype);
            this.Controls.Add(this.txtGameLog);
            this.Controls.Add(this.btnTakeCapture);
            this.Controls.Add(this.picGameImage);
            this.Name = "Form1";
            this.Text = "Bejeweled Bot";
            ((System.ComponentModel.ISupportInitialize)(this.picGameImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picGameImage;
        private System.Windows.Forms.Button btnTakeCapture;
        private System.Windows.Forms.TextBox txtGameLog;
        private System.Windows.Forms.ComboBox comboGametype;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.CheckBox checkAutoResetGame;
    }
}

