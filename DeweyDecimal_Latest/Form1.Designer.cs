﻿namespace DeweyDecimal_Latest
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
            this.sortingGame1 = new DeweyDecimal_Latest.SortingGame();
            this.SuspendLayout();
            // 
            // sortingGame1
            // 
            this.sortingGame1.Location = new System.Drawing.Point(71, 36);
            this.sortingGame1.Name = "sortingGame1";
            this.sortingGame1.Size = new System.Drawing.Size(937, 710);
            this.sortingGame1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 749);
            this.Controls.Add(this.sortingGame1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private SortingGame sortingGame1;
    }
}
