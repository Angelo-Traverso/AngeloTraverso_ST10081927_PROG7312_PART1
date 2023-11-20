namespace DeweyDecimal_Latest
{
    partial class MatchColumn_Control
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
            this.components = new System.ComponentModel.Container();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnlDraw = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblFastestTime = new System.Windows.Forms.Label();
            this.lblAnswerCounter = new System.Windows.Forms.Label();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.pnlSColumn6 = new System.Windows.Forms.Panel();
            this.pnlSColumn7 = new System.Windows.Forms.Panel();
            this.pnlSColumn5 = new System.Windows.Forms.Panel();
            this.pnlSColumn4 = new System.Windows.Forms.Panel();
            this.pnlSColumn3 = new System.Windows.Forms.Panel();
            this.pnlSColumn2 = new System.Windows.Forms.Panel();
            this.pnlSColumn1 = new System.Windows.Forms.Panel();
            this.pnlFColumn4 = new System.Windows.Forms.Panel();
            this.pnlFColumn3 = new System.Windows.Forms.Panel();
            this.pnlFColumn2 = new System.Windows.Forms.Panel();
            this.pnlFColumn1 = new System.Windows.Forms.Panel();
            this.matchColumnTimer = new System.Windows.Forms.Timer(this.components);
            this.btnExitApplication = new System.Windows.Forms.Button();
            this.pnlDraw.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDraw
            // 
            this.pnlDraw.BackColor = System.Drawing.Color.Transparent;
            this.pnlDraw.Controls.Add(this.btnExitApplication);
            this.pnlDraw.Controls.Add(this.btnExit);
            this.pnlDraw.Controls.Add(this.btnStart);
            this.pnlDraw.Controls.Add(this.panel1);
            this.pnlDraw.Controls.Add(this.lblAnswerCounter);
            this.pnlDraw.Controls.Add(this.btnNewGame);
            this.pnlDraw.Controls.Add(this.pnlSColumn6);
            this.pnlDraw.Controls.Add(this.pnlSColumn7);
            this.pnlDraw.Controls.Add(this.pnlSColumn5);
            this.pnlDraw.Controls.Add(this.pnlSColumn4);
            this.pnlDraw.Controls.Add(this.pnlSColumn3);
            this.pnlDraw.Controls.Add(this.pnlSColumn2);
            this.pnlDraw.Controls.Add(this.pnlSColumn1);
            this.pnlDraw.Controls.Add(this.pnlFColumn4);
            this.pnlDraw.Controls.Add(this.pnlFColumn3);
            this.pnlDraw.Controls.Add(this.pnlFColumn2);
            this.pnlDraw.Controls.Add(this.pnlFColumn1);
            this.pnlDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDraw.Location = new System.Drawing.Point(0, 0);
            this.pnlDraw.Name = "pnlDraw";
            this.pnlDraw.Size = new System.Drawing.Size(791, 627);
            this.pnlDraw.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = global::DeweyDecimal_Latest.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Location = new System.Drawing.Point(103, 19);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(53, 43);
            this.btnExit.TabIndex = 17;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackgroundImage = global::DeweyDecimal_Latest.Properties.Resources.play;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStart.Location = new System.Drawing.Point(360, 336);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 75);
            this.btnStart.TabIndex = 16;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(76)))), ((int)(((byte)(31)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTimer);
            this.panel1.Controls.Add(this.lblFastestTime);
            this.panel1.Location = new System.Drawing.Point(194, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 153);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Location = new System.Drawing.Point(-1, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(466, 3);
            this.panel2.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(-1, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 54);
            this.label1.TabIndex = 14;
            this.label1.Text = "Match the left column to the right column.\r\nThe left column can consist of either" +
    " calling numbers or descriptions.\r\nThe Contrery will be the second column.";
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.Color.White;
            this.lblTimer.Location = new System.Drawing.Point(156, 3);
            this.lblTimer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(143, 37);
            this.lblTimer.TabIndex = 8;
            this.lblTimer.Text = "00:00:00";
            // 
            // lblFastestTime
            // 
            this.lblFastestTime.AutoSize = true;
            this.lblFastestTime.BackColor = System.Drawing.Color.Transparent;
            this.lblFastestTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFastestTime.ForeColor = System.Drawing.Color.White;
            this.lblFastestTime.Location = new System.Drawing.Point(162, 41);
            this.lblFastestTime.Name = "lblFastestTime";
            this.lblFastestTime.Size = new System.Drawing.Size(97, 16);
            this.lblFastestTime.TabIndex = 13;
            this.lblFastestTime.Text = "Personal Best: ";
            // 
            // lblAnswerCounter
            // 
            this.lblAnswerCounter.AutoSize = true;
            this.lblAnswerCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnswerCounter.Location = new System.Drawing.Point(633, 178);
            this.lblAnswerCounter.Name = "lblAnswerCounter";
            this.lblAnswerCounter.Size = new System.Drawing.Size(42, 25);
            this.lblAnswerCounter.TabIndex = 11;
            this.lblAnswerCounter.Text = "0/4";
            // 
            // btnNewGame
            // 
            this.btnNewGame.BackgroundImage = global::DeweyDecimal_Latest.Properties.Resources.Back;
            this.btnNewGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewGame.Location = new System.Drawing.Point(360, 178);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(75, 64);
            this.btnNewGame.TabIndex = 10;
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Visible = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            this.btnNewGame.MouseHover += new System.EventHandler(this.btnNewGame_MouseHover);
            // 
            // pnlSColumn6
            // 
            this.pnlSColumn6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(148)))), ((int)(((byte)(63)))));
            this.pnlSColumn6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSColumn6.Location = new System.Drawing.Point(576, 493);
            this.pnlSColumn6.Name = "pnlSColumn6";
            this.pnlSColumn6.Size = new System.Drawing.Size(159, 51);
            this.pnlSColumn6.TabIndex = 7;
            // 
            // pnlSColumn7
            // 
            this.pnlSColumn7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(148)))), ((int)(((byte)(63)))));
            this.pnlSColumn7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSColumn7.Location = new System.Drawing.Point(576, 549);
            this.pnlSColumn7.Name = "pnlSColumn7";
            this.pnlSColumn7.Size = new System.Drawing.Size(159, 51);
            this.pnlSColumn7.TabIndex = 6;
            // 
            // pnlSColumn5
            // 
            this.pnlSColumn5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(148)))), ((int)(((byte)(63)))));
            this.pnlSColumn5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSColumn5.Location = new System.Drawing.Point(576, 437);
            this.pnlSColumn5.Name = "pnlSColumn5";
            this.pnlSColumn5.Size = new System.Drawing.Size(159, 51);
            this.pnlSColumn5.TabIndex = 5;
            // 
            // pnlSColumn4
            // 
            this.pnlSColumn4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(148)))), ((int)(((byte)(63)))));
            this.pnlSColumn4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSColumn4.Location = new System.Drawing.Point(576, 381);
            this.pnlSColumn4.Name = "pnlSColumn4";
            this.pnlSColumn4.Size = new System.Drawing.Size(159, 51);
            this.pnlSColumn4.TabIndex = 4;
            // 
            // pnlSColumn3
            // 
            this.pnlSColumn3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(148)))), ((int)(((byte)(63)))));
            this.pnlSColumn3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSColumn3.Location = new System.Drawing.Point(576, 325);
            this.pnlSColumn3.Name = "pnlSColumn3";
            this.pnlSColumn3.Size = new System.Drawing.Size(159, 51);
            this.pnlSColumn3.TabIndex = 3;
            // 
            // pnlSColumn2
            // 
            this.pnlSColumn2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(148)))), ((int)(((byte)(63)))));
            this.pnlSColumn2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSColumn2.Location = new System.Drawing.Point(576, 269);
            this.pnlSColumn2.Name = "pnlSColumn2";
            this.pnlSColumn2.Size = new System.Drawing.Size(159, 51);
            this.pnlSColumn2.TabIndex = 2;
            // 
            // pnlSColumn1
            // 
            this.pnlSColumn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(148)))), ((int)(((byte)(63)))));
            this.pnlSColumn1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSColumn1.Location = new System.Drawing.Point(576, 213);
            this.pnlSColumn1.Name = "pnlSColumn1";
            this.pnlSColumn1.Size = new System.Drawing.Size(159, 51);
            this.pnlSColumn1.TabIndex = 1;
            // 
            // pnlFColumn4
            // 
            this.pnlFColumn4.BackColor = System.Drawing.Color.LightGray;
            this.pnlFColumn4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFColumn4.Location = new System.Drawing.Point(54, 500);
            this.pnlFColumn4.Name = "pnlFColumn4";
            this.pnlFColumn4.Size = new System.Drawing.Size(159, 51);
            this.pnlFColumn4.TabIndex = 3;
            // 
            // pnlFColumn3
            // 
            this.pnlFColumn3.BackColor = System.Drawing.Color.LightGray;
            this.pnlFColumn3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFColumn3.Location = new System.Drawing.Point(54, 426);
            this.pnlFColumn3.Name = "pnlFColumn3";
            this.pnlFColumn3.Size = new System.Drawing.Size(159, 51);
            this.pnlFColumn3.TabIndex = 2;
            // 
            // pnlFColumn2
            // 
            this.pnlFColumn2.BackColor = System.Drawing.Color.LightGray;
            this.pnlFColumn2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFColumn2.Location = new System.Drawing.Point(54, 348);
            this.pnlFColumn2.Name = "pnlFColumn2";
            this.pnlFColumn2.Size = new System.Drawing.Size(159, 51);
            this.pnlFColumn2.TabIndex = 1;
            // 
            // pnlFColumn1
            // 
            this.pnlFColumn1.BackColor = System.Drawing.Color.LightGray;
            this.pnlFColumn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFColumn1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFColumn1.Location = new System.Drawing.Point(54, 275);
            this.pnlFColumn1.Name = "pnlFColumn1";
            this.pnlFColumn1.Size = new System.Drawing.Size(159, 51);
            this.pnlFColumn1.TabIndex = 0;
            // 
            // matchColumnTimer
            // 
            this.matchColumnTimer.Tick += new System.EventHandler(this.matchColumnTimer_Tick);
            // 
            // btnExitApplication
            // 
            this.btnExitApplication.Location = new System.Drawing.Point(691, 19);
            this.btnExitApplication.Name = "btnExitApplication";
            this.btnExitApplication.Size = new System.Drawing.Size(75, 23);
            this.btnExitApplication.TabIndex = 18;
            this.btnExitApplication.Text = "Exit";
            this.btnExitApplication.UseVisualStyleBackColor = true;
            this.btnExitApplication.Click += new System.EventHandler(this.btnExitApplication_Click);
            // 
            // MatchColumn_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlDraw);
            this.Name = "MatchColumn_Control";
            this.Size = new System.Drawing.Size(791, 627);
            this.pnlDraw.ResumeLayout(false);
            this.pnlDraw.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel pnlDraw;
        private System.Windows.Forms.Panel pnlFColumn1;
        private System.Windows.Forms.Panel pnlSColumn6;
        private System.Windows.Forms.Panel pnlSColumn7;
        private System.Windows.Forms.Panel pnlSColumn5;
        private System.Windows.Forms.Panel pnlSColumn4;
        private System.Windows.Forms.Panel pnlSColumn3;
        private System.Windows.Forms.Panel pnlSColumn2;
        private System.Windows.Forms.Panel pnlSColumn1;
        private System.Windows.Forms.Panel pnlFColumn4;
        private System.Windows.Forms.Panel pnlFColumn3;
        private System.Windows.Forms.Panel pnlFColumn2;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Label lblAnswerCounter;
        private System.Windows.Forms.Timer matchColumnTimer;
        private System.Windows.Forms.Label lblFastestTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExitApplication;
    }
}
