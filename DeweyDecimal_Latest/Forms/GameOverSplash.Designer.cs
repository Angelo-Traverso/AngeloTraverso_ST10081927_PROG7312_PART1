namespace DeweyDecimal_Latest.Forms
{
    partial class GameOverSplash
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
            this.lblYes = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblYes
            // 
            this.lblYes.AutoSize = true;
            this.lblYes.BackColor = System.Drawing.Color.Transparent;
            this.lblYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYes.Location = new System.Drawing.Point(383, 238);
            this.lblYes.Name = "lblYes";
            this.lblYes.Size = new System.Drawing.Size(42, 24);
            this.lblYes.TabIndex = 0;
            this.lblYes.Text = "Yes";
            this.lblYes.Click += new System.EventHandler(this.lblYes_Click);
            this.lblYes.MouseEnter += new System.EventHandler(this.lblYes_MouseEnter);
            this.lblYes.MouseLeave += new System.EventHandler(this.lblYes_MouseLeave);
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.BackColor = System.Drawing.Color.Transparent;
            this.lblNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNo.Location = new System.Drawing.Point(509, 238);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(35, 24);
            this.lblNo.TabIndex = 1;
            this.lblNo.Text = "No";
            this.lblNo.Click += new System.EventHandler(this.lblNo_Click);
            this.lblNo.MouseEnter += new System.EventHandler(this.lblNo_MouseEnter);
            this.lblNo.MouseLeave += new System.EventHandler(this.lblNo_MouseLeave);
            // 
            // GameOverSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DeweyDecimal_Latest.Properties.Resources.GameOverSplash;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(924, 515);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.lblYes);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GameOverSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameOverSplash";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblYes;
        private System.Windows.Forms.Label lblNo;
    }
}