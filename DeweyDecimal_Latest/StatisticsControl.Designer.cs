namespace DeweyDecimal_Latest
{
    partial class StatisticsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTimeHeading = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBestTime = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblScoreHeading = new System.Windows.Forms.Label();
            this.lblNoStats = new System.Windows.Forms.Label();
            this.lblNoStatsDesc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTimeHeading
            // 
            this.lblTimeHeading.AutoSize = true;
            this.lblTimeHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeHeading.Location = new System.Drawing.Point(13, 329);
            this.lblTimeHeading.Name = "lblTimeHeading";
            this.lblTimeHeading.Size = new System.Drawing.Size(313, 37);
            this.lblTimeHeading.TabIndex = 3;
            this.lblTimeHeading.Text = "Best completion time";
            this.lblTimeHeading.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(390, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 42);
            this.label1.TabIndex = 2;
            this.label1.Text = "Game Statistics";
            // 
            // lblBestTime
            // 
            this.lblBestTime.AutoSize = true;
            this.lblBestTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBestTime.ForeColor = System.Drawing.Color.IndianRed;
            this.lblBestTime.Location = new System.Drawing.Point(390, 329);
            this.lblBestTime.Name = "lblBestTime";
            this.lblBestTime.Size = new System.Drawing.Size(313, 37);
            this.lblBestTime.TabIndex = 4;
            this.lblBestTime.Text = "Best completion time";
            this.lblBestTime.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.IndianRed;
            this.lblScore.Location = new System.Drawing.Point(399, 205);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(100, 37);
            this.lblScore.TabIndex = 6;
            this.lblScore.Text = "Score";
            // 
            // lblScoreHeading
            // 
            this.lblScoreHeading.AutoSize = true;
            this.lblScoreHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScoreHeading.Location = new System.Drawing.Point(13, 205);
            this.lblScoreHeading.Name = "lblScoreHeading";
            this.lblScoreHeading.Size = new System.Drawing.Size(150, 37);
            this.lblScoreHeading.TabIndex = 5;
            this.lblScoreHeading.Text = "My Score";
            // 
            // lblNoStats
            // 
            this.lblNoStats.AutoSize = true;
            this.lblNoStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoStats.Location = new System.Drawing.Point(268, 85);
            this.lblNoStats.Name = "lblNoStats";
            this.lblNoStats.Size = new System.Drawing.Size(494, 33);
            this.lblNoStats.TabIndex = 7;
            this.lblNoStats.Text = "You currenty have no stats available.";
            this.lblNoStats.Visible = false;
            // 
            // lblNoStatsDesc
            // 
            this.lblNoStatsDesc.AutoSize = true;
            this.lblNoStatsDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoStatsDesc.Location = new System.Drawing.Point(236, 137);
            this.lblNoStatsDesc.Name = "lblNoStatsDesc";
            this.lblNoStatsDesc.Size = new System.Drawing.Size(569, 33);
            this.lblNoStatsDesc.TabIndex = 8;
            this.lblNoStatsDesc.Text = "Play a and complete a game to get started.";
            this.lblNoStatsDesc.Visible = false;
            // 
            // StatisticsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNoStatsDesc);
            this.Controls.Add(this.lblNoStats);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblScoreHeading);
            this.Controls.Add(this.lblBestTime);
            this.Controls.Add(this.lblTimeHeading);
            this.Controls.Add(this.label1);
            this.Name = "StatisticsControl";
            this.Size = new System.Drawing.Size(970, 595);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTimeHeading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBestTime;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblScoreHeading;
        private System.Windows.Forms.Label lblNoStats;
        private System.Windows.Forms.Label lblNoStatsDesc;
    }
}
