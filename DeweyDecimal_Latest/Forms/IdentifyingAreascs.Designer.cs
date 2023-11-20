namespace DeweyDecimal_Latest
{
    partial class IdentifyingAreascs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IdentifyingAreascs));
            this.matchColumn_Control1 = new DeweyDecimal_Latest.MatchColumn_Control();
            this.SuspendLayout();
            // 
            // matchColumn_Control1
            // 
            this.matchColumn_Control1.BackColor = System.Drawing.Color.Transparent;
            this.matchColumn_Control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.matchColumn_Control1.Location = new System.Drawing.Point(0, 0);
            this.matchColumn_Control1.Name = "matchColumn_Control1";
            this.matchColumn_Control1.Size = new System.Drawing.Size(775, 623);
            this.matchColumn_Control1.TabIndex = 0;
            // 
            // IdentifyingAreascs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(775, 623);
            this.Controls.Add(this.matchColumn_Control1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Name = "IdentifyingAreascs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Match the Columns";
            this.ResumeLayout(false);

        }

        #endregion

        private MatchColumn_Control matchColumn_Control1;
    }
}