namespace DeweyDecimal_Latest.Forms
{
    partial class WinForm
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
            this.lblLives = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.lblYes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLives
            // 
            this.lblLives.AutoSize = true;
            this.lblLives.BackColor = System.Drawing.Color.Transparent;
            this.lblLives.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLives.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblLives.Location = new System.Drawing.Point(57, 16);
            this.lblLives.Name = "lblLives";
            this.lblLives.Size = new System.Drawing.Size(54, 33);
            this.lblLives.TabIndex = 3;
            this.lblLives.Text = "x 3";
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.BackColor = System.Drawing.Color.Transparent;
            this.lblNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNo.Location = new System.Drawing.Point(474, 223);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(39, 25);
            this.lblNo.TabIndex = 5;
            this.lblNo.Text = "No";
            this.lblNo.Click += new System.EventHandler(this.lblNo_Click);
            this.lblNo.MouseEnter += new System.EventHandler(this.lblNo_MouseEnter);
            this.lblNo.MouseLeave += new System.EventHandler(this.lblNo_MouseLeave);
            // 
            // lblYes
            // 
            this.lblYes.AutoSize = true;
            this.lblYes.BackColor = System.Drawing.Color.Transparent;
            this.lblYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYes.Location = new System.Drawing.Point(365, 223);
            this.lblYes.Name = "lblYes";
            this.lblYes.Size = new System.Drawing.Size(50, 25);
            this.lblYes.TabIndex = 4;
            this.lblYes.Text = "Yes";
            this.lblYes.Click += new System.EventHandler(this.lblYes_Click);
            this.lblYes.MouseEnter += new System.EventHandler(this.lblYes_MouseEnter);
            this.lblYes.MouseLeave += new System.EventHandler(this.lblYes_MouseLeave);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DeweyDecimal_Latest.Properties.Resources.gameWin;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(924, 515);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.lblYes);
            this.Controls.Add(this.lblLives);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblLives;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblYes;
    }
}