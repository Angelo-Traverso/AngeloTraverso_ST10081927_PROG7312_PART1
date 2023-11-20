/*
 * Student Name: Angelo Traverso
 * Student Number: ST10081927
 * Project: Programming 3B POE
 * Project Title: Finding Call Numbers
 */
using System.Media;
using System.Windows.Forms;


namespace DeweyDecimal_Latest
{
    public partial class FindCallNumbersGame : Form
    {
        public FindCallNumbersGame()
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

        private void FindCallNumbersGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Environment.Exit(1);

        }
    }
}
