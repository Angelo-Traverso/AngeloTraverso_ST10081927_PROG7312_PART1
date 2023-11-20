namespace DeweyDecimal_Latest.Forms
{
    partial class FindingCallNumberGame
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
            this.findingCallNumberTreeControl1 = new DeweyDecimal_Latest.FindingCallNumberTreeControl();
            this.SuspendLayout();
            // 
            // findingCallNumberTreeControl1
            // 
            this.findingCallNumberTreeControl1.BackColor = System.Drawing.Color.Transparent;
            this.findingCallNumberTreeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingCallNumberTreeControl1.Location = new System.Drawing.Point(0, 0);
            this.findingCallNumberTreeControl1.Name = "findingCallNumberTreeControl1";
            this.findingCallNumberTreeControl1.Size = new System.Drawing.Size(811, 650);
            this.findingCallNumberTreeControl1.TabIndex = 0;
            // 
            // FindingCallNumberGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DeweyDecimal_Latest.Properties.Resources.BackgroundImage_FinalGame;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(811, 650);
            this.Controls.Add(this.findingCallNumberTreeControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Name = "FindingCallNumberGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FindingCallNumberGame";
            this.ResumeLayout(false);

        }

        #endregion

        private FindingCallNumberTreeControl findingCallNumberTreeControl1;
    }
}