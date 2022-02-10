namespace BattleShipGame
{
    partial class Battleship
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
            this.BtnEasy = new System.Windows.Forms.Button();
            this.BtnGod = new System.Windows.Forms.Button();
            this.BtnHard = new System.Windows.Forms.Button();
            this.BtnMedium = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnEasy
            // 
            this.BtnEasy.BackColor = System.Drawing.Color.OrangeRed;
            this.BtnEasy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEasy.Font = new System.Drawing.Font("Microsoft Tai Le", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEasy.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnEasy.Location = new System.Drawing.Point(22, 55);
            this.BtnEasy.Name = "BtnEasy";
            this.BtnEasy.Size = new System.Drawing.Size(134, 42);
            this.BtnEasy.TabIndex = 0;
            this.BtnEasy.Tag = "0";
            this.BtnEasy.Text = "Easy";
            this.BtnEasy.UseVisualStyleBackColor = false;
            this.BtnEasy.Click += new System.EventHandler(this.BtnEasy_Click);
            // 
            // BtnGod
            // 
            this.BtnGod.BackColor = System.Drawing.Color.OrangeRed;
            this.BtnGod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGod.Font = new System.Drawing.Font("Microsoft Tai Le", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGod.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnGod.Location = new System.Drawing.Point(224, 148);
            this.BtnGod.Name = "BtnGod";
            this.BtnGod.Size = new System.Drawing.Size(134, 42);
            this.BtnGod.TabIndex = 1;
            this.BtnGod.Tag = "3";
            this.BtnGod.Text = "God";
            this.BtnGod.UseVisualStyleBackColor = false;
            this.BtnGod.Click += new System.EventHandler(this.BtnEasy_Click);
            // 
            // BtnHard
            // 
            this.BtnHard.BackColor = System.Drawing.Color.OrangeRed;
            this.BtnHard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnHard.Font = new System.Drawing.Font("Microsoft Tai Le", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHard.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnHard.Location = new System.Drawing.Point(22, 148);
            this.BtnHard.Name = "BtnHard";
            this.BtnHard.Size = new System.Drawing.Size(134, 42);
            this.BtnHard.TabIndex = 2;
            this.BtnHard.Tag = "2";
            this.BtnHard.Text = "Hard";
            this.BtnHard.UseVisualStyleBackColor = false;
            this.BtnHard.Click += new System.EventHandler(this.BtnEasy_Click);
            // 
            // BtnMedium
            // 
            this.BtnMedium.BackColor = System.Drawing.Color.OrangeRed;
            this.BtnMedium.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMedium.Font = new System.Drawing.Font("Microsoft Tai Le", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMedium.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnMedium.Location = new System.Drawing.Point(224, 55);
            this.BtnMedium.Name = "BtnMedium";
            this.BtnMedium.Size = new System.Drawing.Size(134, 42);
            this.BtnMedium.TabIndex = 3;
            this.BtnMedium.Tag = "1";
            this.BtnMedium.Text = "Medium";
            this.BtnMedium.UseVisualStyleBackColor = false;
            this.BtnMedium.Click += new System.EventHandler(this.BtnEasy_Click);
            // 
            // Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(386, 243);
            this.Controls.Add(this.BtnMedium);
            this.Controls.Add(this.BtnHard);
            this.Controls.Add(this.BtnGod);
            this.Controls.Add(this.BtnEasy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Battleship";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Choose Difficulty";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnEasy;
        private System.Windows.Forms.Button BtnGod;
        private System.Windows.Forms.Button BtnHard;
        private System.Windows.Forms.Button BtnMedium;
    }
}