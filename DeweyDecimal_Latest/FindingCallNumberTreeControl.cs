using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Automation;

namespace DeweyDecimal_Latest
{
    public partial class FindingCallNumberTreeControl : UserControl
    {
        FileWorker fileWorker = new FileWorker();
        private SoundPlayer soundPlayer = new SoundPlayer();
        public FindingCallNumberTreeControl()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();
                fileWorker.LifeLost += FileWorkerInstance_LifeLost;
                fileWorker.gameReset += GameHasReset;

            }
        }

        private void FindingCallNumberTreeControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                fileWorker.ReadFromFile();

                fileWorker.DisplayQuestion(new Panel[] { pnlOption1, pnlOption2, pnlOption3, pnlOption4 }, lblQuestion);

                pnlQuestionContainer.Paint += dropShadow;
            }
        }

        private void dropShadow(object sender, PaintEventArgs e)
        {
            Color[] shadow = new Color[3];
            shadow[0] = Color.FromArgb(181, 181, 181);
            shadow[1] = Color.FromArgb(195, 195, 195);
            shadow[2] = Color.FromArgb(211, 211, 211);
            Pen pen = new Pen(shadow[0]);

            Panel p = pnlQuestionContainer;
            Point pt = p.Location;
            pt.Y += p.Height;
            for (var sp = 0; sp < 3; sp++)
            {
                pen.Color = shadow[sp];
                e.Graphics.DrawLine(pen, pt.X, pt.Y, pt.X + p.Width - 1, pt.Y);
                pt.Y++;
            }
        }

        private void FileWorkerInstance_LifeLost(object sender, EventArgs e)
        {
            switch (fileWorker.livesLeft)
            {
                case 2:
                    life1.Visible = false;
                    break;
                case 1:
                    life2.Visible = false;
                    break;
                case 0:
                    life3.Visible = false;
                    break;
            }
            // Handle the event, e.g., update UI or perform other actions
            // This method will be called when a life is lost in the FileWorker class
        }

        private void GameHasReset(object sender, EventArgs e)
        {
            life3.Visible = true;
            life2.Visible = true;
            life1.Visible = true;
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method to play a sound, url path is passed so that the method is dynamic
        /// </summary>
        /// <param name="url"></param>
        public async Task PlaySound(string url)
        {
            await Task.Run(() =>
            {
                soundPlayer.PlaySound($"Media\\{url}");
            });
        }
    }
}
