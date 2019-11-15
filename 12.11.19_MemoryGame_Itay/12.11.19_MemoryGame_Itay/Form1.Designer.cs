namespace _12._11._19_MemoryGame_Itay
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
            this.cmbBoardSize = new System.Windows.Forms.ComboBox();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.lblIdentityRevealing = new System.Windows.Forms.Label();
            this.lblYourScore = new System.Windows.Forms.Label();
            this.lblOpponentScore = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDifficultyLevel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCharset = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbBoardSize
            // 
            this.cmbBoardSize.FormattingEnabled = true;
            this.cmbBoardSize.Location = new System.Drawing.Point(703, 12);
            this.cmbBoardSize.Name = "cmbBoardSize";
            this.cmbBoardSize.Size = new System.Drawing.Size(56, 21);
            this.cmbBoardSize.TabIndex = 0;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(691, 60);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(75, 23);
            this.btnNewGame.TabIndex = 1;
            this.btnNewGame.Text = "new game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // lblIdentityRevealing
            // 
            this.lblIdentityRevealing.AutoSize = true;
            this.lblIdentityRevealing.Location = new System.Drawing.Point(12, 417);
            this.lblIdentityRevealing.Name = "lblIdentityRevealing";
            this.lblIdentityRevealing.Size = new System.Drawing.Size(0, 13);
            this.lblIdentityRevealing.TabIndex = 2;
            // 
            // lblYourScore
            // 
            this.lblYourScore.AutoSize = true;
            this.lblYourScore.Location = new System.Drawing.Point(689, 104);
            this.lblYourScore.Name = "lblYourScore";
            this.lblYourScore.Size = new System.Drawing.Size(70, 13);
            this.lblYourScore.TabIndex = 3;
            this.lblYourScore.Text = "Your score: 0";
            // 
            // lblOpponentScore
            // 
            this.lblOpponentScore.AutoSize = true;
            this.lblOpponentScore.Location = new System.Drawing.Point(688, 134);
            this.lblOpponentScore.Name = "lblOpponentScore";
            this.lblOpponentScore.Size = new System.Drawing.Size(95, 13);
            this.lblOpponentScore.TabIndex = 4;
            this.lblOpponentScore.Text = "Opponent score: 0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(688, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Difficulty:";
            // 
            // cmbDifficultyLevel
            // 
            this.cmbDifficultyLevel.FormattingEnabled = true;
            this.cmbDifficultyLevel.Location = new System.Drawing.Point(743, 160);
            this.cmbDifficultyLevel.Name = "cmbDifficultyLevel";
            this.cmbDifficultyLevel.Size = new System.Drawing.Size(45, 21);
            this.cmbDifficultyLevel.TabIndex = 6;
            this.cmbDifficultyLevel.SelectedIndexChanged += new System.EventHandler(this.cmbDifficultyLevel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(692, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Charset:";
            // 
            // cmbCharset
            // 
            this.cmbCharset.FormattingEnabled = true;
            this.cmbCharset.Location = new System.Drawing.Point(695, 212);
            this.cmbCharset.Name = "cmbCharset";
            this.cmbCharset.Size = new System.Drawing.Size(93, 21);
            this.cmbCharset.TabIndex = 8;
            this.cmbCharset.SelectedIndexChanged += new System.EventHandler(this.cmbCharset_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbCharset);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDifficultyLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblOpponentScore);
            this.Controls.Add(this.lblYourScore);
            this.Controls.Add(this.lblIdentityRevealing);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.cmbBoardSize);
            this.Name = "Form1";
            this.Text = "MemoryGame";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBoardSize;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Label lblIdentityRevealing;
        private System.Windows.Forms.Label lblYourScore;
        private System.Windows.Forms.Label lblOpponentScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDifficultyLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCharset;
    }
}

