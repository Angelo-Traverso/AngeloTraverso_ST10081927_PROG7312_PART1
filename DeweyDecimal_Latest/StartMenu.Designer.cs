namespace DeweyDecimal_Latest
{
    partial class StartMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartMenu));
            this.lblMenuHeading = new System.Windows.Forms.Label();
            this.btnFindCallNumbers = new System.Windows.Forms.Button();
            this.btnIdentifyAreas = new System.Windows.Forms.Button();
            this.btnReplaceBooks = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMenuHeading
            // 
            this.lblMenuHeading.AutoSize = true;
            this.lblMenuHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblMenuHeading.Font = new System.Drawing.Font("Microsoft JhengHei", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuHeading.Location = new System.Drawing.Point(196, 90);
            this.lblMenuHeading.Name = "lblMenuHeading";
            this.lblMenuHeading.Size = new System.Drawing.Size(162, 61);
            this.lblMenuHeading.TabIndex = 7;
            this.lblMenuHeading.Text = "Menu";
            // 
            // btnFindCallNumbers
            // 
            this.btnFindCallNumbers.Location = new System.Drawing.Point(194, 285);
            this.btnFindCallNumbers.Name = "btnFindCallNumbers";
            this.btnFindCallNumbers.Size = new System.Drawing.Size(164, 29);
            this.btnFindCallNumbers.TabIndex = 6;
            this.btnFindCallNumbers.Text = "Find Call Numbers";
            this.btnFindCallNumbers.UseVisualStyleBackColor = true;
            this.btnFindCallNumbers.Click += new System.EventHandler(this.btnFindCallNumbers_Click);
            // 
            // btnIdentifyAreas
            // 
            this.btnIdentifyAreas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(148)))), ((int)(((byte)(63)))));
            this.btnIdentifyAreas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIdentifyAreas.Location = new System.Drawing.Point(194, 227);
            this.btnIdentifyAreas.Name = "btnIdentifyAreas";
            this.btnIdentifyAreas.Size = new System.Drawing.Size(164, 44);
            this.btnIdentifyAreas.TabIndex = 5;
            this.btnIdentifyAreas.Text = "Identify Area";
            this.btnIdentifyAreas.UseVisualStyleBackColor = false;
            this.btnIdentifyAreas.Click += new System.EventHandler(this.btnIdentifyAreas_Click);
            this.btnIdentifyAreas.MouseHover += new System.EventHandler(this.btnIdentifyAreas_MouseHover);
            // 
            // btnReplaceBooks
            // 
            this.btnReplaceBooks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnReplaceBooks.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReplaceBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplaceBooks.Location = new System.Drawing.Point(194, 166);
            this.btnReplaceBooks.Name = "btnReplaceBooks";
            this.btnReplaceBooks.Size = new System.Drawing.Size(164, 44);
            this.btnReplaceBooks.TabIndex = 4;
            this.btnReplaceBooks.Text = "Replace Books";
            this.btnReplaceBooks.UseVisualStyleBackColor = false;
            this.btnReplaceBooks.Click += new System.EventHandler(this.btnReplaceBooks_Click);
            this.btnReplaceBooks.MouseHover += new System.EventHandler(this.btnReplaceBooks_MouseHover);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(194, 329);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(164, 30);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(557, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblMenuHeading);
            this.Controls.Add(this.btnFindCallNumbers);
            this.Controls.Add(this.btnIdentifyAreas);
            this.Controls.Add(this.btnReplaceBooks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "StartMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMenuHeading;
        private System.Windows.Forms.Button btnFindCallNumbers;
        private System.Windows.Forms.Button btnIdentifyAreas;
        private System.Windows.Forms.Button btnReplaceBooks;
        private System.Windows.Forms.Button btnExit;
    }
}