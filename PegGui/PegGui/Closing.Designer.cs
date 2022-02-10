namespace PegGui
{
    partial class Closing
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
            this.LblWinner = new System.Windows.Forms.Label();
            this.LblHighTime = new System.Windows.Forms.Label();
            this.LblTime = new System.Windows.Forms.Label();
            this.LblScore = new System.Windows.Forms.Label();
            this.LblHighScore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblWinner
            // 
            this.LblWinner.AutoSize = true;
            this.LblWinner.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblWinner.Location = new System.Drawing.Point(24, 36);
            this.LblWinner.Name = "LblWinner";
            this.LblWinner.Size = new System.Drawing.Size(0, 19);
            this.LblWinner.TabIndex = 0;
            this.LblWinner.Click += new System.EventHandler(this.LblWinner_Click);
            // 
            // LblHighTime
            // 
            this.LblHighTime.AutoSize = true;
            this.LblHighTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHighTime.Location = new System.Drawing.Point(304, 139);
            this.LblHighTime.Name = "LblHighTime";
            this.LblHighTime.Size = new System.Drawing.Size(0, 19);
            this.LblHighTime.TabIndex = 1;
            this.LblHighTime.Click += new System.EventHandler(this.LblHighTime_Click);
            // 
            // LblTime
            // 
            this.LblTime.AutoSize = true;
            this.LblTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTime.Location = new System.Drawing.Point(24, 139);
            this.LblTime.Name = "LblTime";
            this.LblTime.Size = new System.Drawing.Size(0, 19);
            this.LblTime.TabIndex = 3;
            this.LblTime.Click += new System.EventHandler(this.LblTime_Click);
            // 
            // LblScore
            // 
            this.LblScore.AutoSize = true;
            this.LblScore.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblScore.Location = new System.Drawing.Point(24, 86);
            this.LblScore.Name = "LblScore";
            this.LblScore.Size = new System.Drawing.Size(0, 19);
            this.LblScore.TabIndex = 4;
            this.LblScore.Click += new System.EventHandler(this.LblScore_Click);
            // 
            // LblHighScore
            // 
            this.LblHighScore.AutoSize = true;
            this.LblHighScore.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHighScore.Location = new System.Drawing.Point(304, 86);
            this.LblHighScore.Name = "LblHighScore";
            this.LblHighScore.Size = new System.Drawing.Size(0, 19);
            this.LblHighScore.TabIndex = 5;
            this.LblHighScore.Click += new System.EventHandler(this.LblHighScore_Click);
            // 
            // Closing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(408, 238);
            this.Controls.Add(this.LblHighScore);
            this.Controls.Add(this.LblScore);
            this.Controls.Add(this.LblTime);
            this.Controls.Add(this.LblHighTime);
            this.Controls.Add(this.LblWinner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Closing";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Game Over";
            this.Load += new System.EventHandler(this.Closing_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblWinner;
        private System.Windows.Forms.Label LblHighTime;
        private System.Windows.Forms.Label LblTime;
        private System.Windows.Forms.Label LblScore;
        private System.Windows.Forms.Label LblHighScore;
    }
}