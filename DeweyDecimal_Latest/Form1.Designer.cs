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
            this.sortingGame1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.sortingGame1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sortingGame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sortingGame1.Location = new System.Drawing.Point(0, 0);
            this.sortingGame1.Name = "sortingGame1";
            this.sortingGame1.Size = new System.Drawing.Size(1097, 746);
            this.sortingGame1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 746);
            this.Controls.Add(this.sortingGame1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replace - Dewey Decimal Game";
            this.ResumeLayout(false);

        }

        #endregion

        private SortingGame sortingGame1;
    }
}

