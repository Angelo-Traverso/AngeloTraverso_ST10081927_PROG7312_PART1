/*
 *  Full Name: Angelo Traverso
 *  Student Number: ST10081927
 *  Subject: Programming 3B
 *  Code: PROG7312
 */

using System;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class IdentifyingAreascs : Form
    {
        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public IdentifyingAreascs()
        {
            InitializeComponent();
            HandleWindowState();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Removes maximise and minimize boxes
        /// </summary>
        private void HandleWindowState()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void IdentifyingAreascs_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void IdentifyingAreascs_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //
