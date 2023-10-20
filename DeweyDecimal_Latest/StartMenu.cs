/*
 *  Full Name: Angelo Traverso
 *  Student Number: ST10081927
 *  Subject: Programming 3B
 *  Code: PROG7312
 */

using System;
using System.Management.Instrumentation;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
            HandleWindowState();
        }

        /// <summary>
        ///     Removes maximise and minimize boxes
        /// </summary>
        private void HandleWindowState()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        /// <summary>
        ///     Click event for game selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplaceBooks_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        /// <summary>
        ///     On click event for exiting application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void btnIdentifyAreas_Click(object sender, EventArgs e)
        {
            IdentifyingAreascs areas = new IdentifyingAreascs();

            areas.Show();
            this.Hide();
        }
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //
