namespace DeweyDecimal_Latest
{
    partial class StartMenuControl
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
            this.btnReplaceBooks = new System.Windows.Forms.Button();
            this.btnIdentifyAreas = new System.Windows.Forms.Button();
            this.btnFindCallNumbers = new System.Windows.Forms.Button();
            this.lblMenuHeading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReplaceBooks
            // 
            this.btnReplaceBooks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnReplaceBooks.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReplaceBooks.Location = new System.Drawing.Point(183, 125);
            this.btnReplaceBooks.Name = "btnReplaceBooks";
            this.btnReplaceBooks.Size = new System.Drawing.Size(75, 44);
            this.btnReplaceBooks.TabIndex = 0;
            this.btnReplaceBooks.Text = "Replace Books";
            this.btnReplaceBooks.UseVisualStyleBackColor = false;
            // 
            // btnIdentifyAreas
            // 
            this.btnIdentifyAreas.Location = new System.Drawing.Point(183, 197);
            this.btnIdentifyAreas.Name = "btnIdentifyAreas";
            this.btnIdentifyAreas.Size = new System.Drawing.Size(75, 44);
            this.btnIdentifyAreas.TabIndex = 1;
            this.btnIdentifyAreas.Text = "Identify Area";
            this.btnIdentifyAreas.UseVisualStyleBackColor = true;
            // 
            // btnFindCallNumbers
            // 
            this.btnFindCallNumbers.Location = new System.Drawing.Point(183, 271);
            this.btnFindCallNumbers.Name = "btnFindCallNumbers";
            this.btnFindCallNumbers.Size = new System.Drawing.Size(75, 44);
            this.btnFindCallNumbers.TabIndex = 2;
            this.btnFindCallNumbers.Text = "Find Call Numbers";
            this.btnFindCallNumbers.UseVisualStyleBackColor = true;
            // 
            // lblMenuHeading
            // 
            this.lblMenuHeading.AutoSize = true;
            this.lblMenuHeading.Font = new System.Drawing.Font("Goudy Stout", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuHeading.Location = new System.Drawing.Point(133, 63);
            this.lblMenuHeading.Name = "lblMenuHeading";
            this.lblMenuHeading.Size = new System.Drawing.Size(189, 44);
            this.lblMenuHeading.TabIndex = 3;
            this.lblMenuHeading.Text = "Menu";
            // 
            // StartMenuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMenuHeading);
            this.Controls.Add(this.btnFindCallNumbers);
            this.Controls.Add(this.btnIdentifyAreas);
            this.Controls.Add(this.btnReplaceBooks);
            this.Name = "StartMenuControl";
            this.Size = new System.Drawing.Size(459, 427);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReplaceBooks;
        private System.Windows.Forms.Button btnIdentifyAreas;
        private System.Windows.Forms.Button btnFindCallNumbers;
        private System.Windows.Forms.Label lblMenuHeading;
    }
}
