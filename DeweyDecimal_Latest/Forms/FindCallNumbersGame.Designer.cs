namespace DeweyDecimal_Latest
{
    partial class FindCallNumbersGame
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
            this.findingCallNumberTreeControl2 = new DeweyDecimal_Latest.FindingCallNumberTreeControl();
            this.findingCallNumberTreeControl1 = new DeweyDecimal_Latest.FindingCallNumberTreeControl();
            this.findingCallNumberTreeControl3 = new DeweyDecimal_Latest.FindingCallNumberTreeControl();
            this.SuspendLayout();
            // 
            // findingCallNumberTreeControl2
            // 
            this.findingCallNumberTreeControl2.BackColor = System.Drawing.Color.Transparent;
            this.findingCallNumberTreeControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingCallNumberTreeControl2.Location = new System.Drawing.Point(0, 0);
            this.findingCallNumberTreeControl2.Name = "findingCallNumberTreeControl2";
            this.findingCallNumberTreeControl2.Size = new System.Drawing.Size(811, 650);
            this.findingCallNumberTreeControl2.TabIndex = 0;
            // 
            // findingCallNumberTreeControl1
            // 
            this.findingCallNumberTreeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingCallNumberTreeControl1.Location = new System.Drawing.Point(0, 0);
            this.findingCallNumberTreeControl1.Name = "findingCallNumberTreeControl1";
            this.findingCallNumberTreeControl1.Size = new System.Drawing.Size(811, 557);
            this.findingCallNumberTreeControl1.TabIndex = 0;
            // 
            // findingCallNumberTreeControl3
            // 
            this.findingCallNumberTreeControl3.BackColor = System.Drawing.Color.Transparent;
            this.findingCallNumberTreeControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findingCallNumberTreeControl3.Location = new System.Drawing.Point(0, 0);
            this.findingCallNumberTreeControl3.Name = "findingCallNumberTreeControl3";
            this.findingCallNumberTreeControl3.Size = new System.Drawing.Size(811, 650);
            this.findingCallNumberTreeControl3.TabIndex = 0;
            // 
            // FindCallNumbersGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DeweyDecimal_Latest.Properties.Resources.BackgroundImage_FinalGame;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(811, 650);
            this.Controls.Add(this.findingCallNumberTreeControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Name = "FindCallNumbersGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FindCallNumbersGame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindCallNumbersGame_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private FindingCallNumberTreeControl findingCallNumberTreeControl1;
        private FindingCallNumberTreeControl findingCallNumberTreeControl2;
        private FindingCallNumberTreeControl findingCallNumberTreeControl3;
    }
}