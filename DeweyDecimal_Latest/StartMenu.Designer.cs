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
            this.lblMenuHeading = new System.Windows.Forms.Label();
            this.btnFindCallNumbers = new System.Windows.Forms.Button();
            this.btnIdentifyAreas = new System.Windows.Forms.Button();
            this.btnReplaceBooks = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMenuHeading
            // 
            this.lblMenuHeading.AutoSize = true;
            this.lblMenuHeading.Font = new System.Drawing.Font("Goudy Stout", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuHeading.Location = new System.Drawing.Point(184, 99);
            this.lblMenuHeading.Name = "lblMenuHeading";
            this.lblMenuHeading.Size = new System.Drawing.Size(189, 44);
            this.lblMenuHeading.TabIndex = 7;
            this.lblMenuHeading.Text = "Menu";
            // 
            // btnFindCallNumbers
            // 
            this.btnFindCallNumbers.Location = new System.Drawing.Point(234, 307);
            this.btnFindCallNumbers.Name = "btnFindCallNumbers";
            this.btnFindCallNumbers.Size = new System.Drawing.Size(75, 44);
            this.btnFindCallNumbers.TabIndex = 6;
            this.btnFindCallNumbers.Text = "Find Call Numbers";
            this.btnFindCallNumbers.UseVisualStyleBackColor = true;
            // 
            // btnIdentifyAreas
            // 
            this.btnIdentifyAreas.Location = new System.Drawing.Point(234, 233);
            this.btnIdentifyAreas.Name = "btnIdentifyAreas";
            this.btnIdentifyAreas.Size = new System.Drawing.Size(75, 44);
            this.btnIdentifyAreas.TabIndex = 5;
            this.btnIdentifyAreas.Text = "Identify Area";
            this.btnIdentifyAreas.UseVisualStyleBackColor = true;
            // 
            // btnReplaceBooks
            // 
            this.btnReplaceBooks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnReplaceBooks.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReplaceBooks.Location = new System.Drawing.Point(234, 161);
            this.btnReplaceBooks.Name = "btnReplaceBooks";
            this.btnReplaceBooks.Size = new System.Drawing.Size(75, 44);
            this.btnReplaceBooks.TabIndex = 4;
            this.btnReplaceBooks.Text = "Replace Books";
            this.btnReplaceBooks.UseVisualStyleBackColor = false;
            this.btnReplaceBooks.Click += new System.EventHandler(this.btnReplaceBooks_Click);
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 450);
            this.Controls.Add(this.lblMenuHeading);
            this.Controls.Add(this.btnFindCallNumbers);
            this.Controls.Add(this.btnIdentifyAreas);
            this.Controls.Add(this.btnReplaceBooks);
            this.Name = "StartMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMenuHeading;
        private System.Windows.Forms.Button btnFindCallNumbers;
        private System.Windows.Forms.Button btnIdentifyAreas;
        private System.Windows.Forms.Button btnReplaceBooks;
    }
}