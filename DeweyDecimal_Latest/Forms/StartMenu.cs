/*
 *  Full Name: Angelo Traverso
 *  Student Number: ST10081927
 *  Subject: Programming 3B
 *  Code: PROG7312
 */

using DeweyDecimal_Latest.Forms;
using System;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class StartMenu : Form
    {
        /// <summary>
        ///  Instance of PanelHelper
        /// </summary>
        private PanelHelper panelHelper = new PanelHelper();

        /// <summary>
        ///     Identify area tooltip message
        /// </summary>
        const string IDENTIFY_AREAS = "Match columns to see just how good your dewey decimal knowledge is!";

        /// <summary>
        ///     Identify area tooltip message
        /// </summary>
        const string REPLACE_BOOKS = "Place library books in the correct dewey decimal order!";

        /// <summary>
        ///     Default constructor
        /// </summary>
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

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Launches Identify Area game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIdentifyAreas_Click(object sender, EventArgs e)
        {
            StartPlaying start = new StartPlaying();
            IdentifyingAreascs areas = new IdentifyingAreascs();
            areas.Show();
            this.Hide();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     On Hover for btnIdentifyAreas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIdentifyAreas_MouseHover(object sender, EventArgs e)
        {
            panelHelper.CreateToolTip(btnIdentifyAreas, IDENTIFY_AREAS);
        }

        /// <summary>
        ///     On Hover for btnReplaceBooks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplaceBooks_MouseHover(object sender, EventArgs e)
        {
            panelHelper.CreateToolTip(btnReplaceBooks, REPLACE_BOOKS);
        }

        private void btnFindCallNumbers_Click(object sender, EventArgs e)
        {
            //FileWorker fileWorker = new FileWorker();
            FindingCallNumberGame game = new FindingCallNumberGame();
            game.Show();
            this.Hide();
            // deweyGame.ReadFromFile();
        }
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //
