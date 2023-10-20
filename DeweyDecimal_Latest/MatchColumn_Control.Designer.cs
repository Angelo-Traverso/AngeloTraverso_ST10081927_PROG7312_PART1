namespace DeweyDecimal_Latest
{
    partial class MatchColumn_Control
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
            this.pnlFirstColumn = new System.Windows.Forms.Panel();
            this.pnlSecondColumn = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnlDraw = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlDraw.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFirstColumn
            // 
            this.pnlFirstColumn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlFirstColumn.Location = new System.Drawing.Point(46, 50);
            this.pnlFirstColumn.Name = "pnlFirstColumn";
            this.pnlFirstColumn.Size = new System.Drawing.Size(200, 364);
            this.pnlFirstColumn.TabIndex = 0;
            // 
            // pnlSecondColumn
            // 
            this.pnlSecondColumn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlSecondColumn.Location = new System.Drawing.Point(415, 50);
            this.pnlSecondColumn.Name = "pnlSecondColumn";
            this.pnlSecondColumn.Size = new System.Drawing.Size(373, 364);
            this.pnlSecondColumn.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(296, 20);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(35, 13);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "label1";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(299, 257);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlDraw
            // 
            this.pnlDraw.BackColor = System.Drawing.Color.Transparent;
            this.pnlDraw.Controls.Add(this.panel1);
            this.pnlDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDraw.Location = new System.Drawing.Point(0, 0);
            this.pnlDraw.Name = "pnlDraw";
            this.pnlDraw.Size = new System.Drawing.Size(791, 486);
            this.pnlDraw.TabIndex = 3;
            this.pnlDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDraw_Paint);
            this.pnlDraw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlDraw_MouseDown);
            this.pnlDraw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlDraw_MouseMove);
            this.pnlDraw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlDraw_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Location = new System.Drawing.Point(252, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 50);
            this.panel1.TabIndex = 0;
            // 
            // MatchColumn_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.pnlSecondColumn);
            this.Controls.Add(this.pnlFirstColumn);
            this.Controls.Add(this.pnlDraw);
            this.Name = "MatchColumn_Control";
            this.Size = new System.Drawing.Size(791, 486);
            this.Load += new System.EventHandler(this.MatchColumn_Control_Load);
            this.pnlDraw.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlFirstColumn;
        private System.Windows.Forms.Panel pnlSecondColumn;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnReset;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel pnlDraw;
        private System.Windows.Forms.Panel panel1;
    }
}
