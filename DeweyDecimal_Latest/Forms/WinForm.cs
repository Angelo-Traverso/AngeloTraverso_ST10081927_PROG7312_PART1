/*
 * Student Name: Angelo Traverso
 * Student Number: ST10081927
 * Project: Programming 3B POE
 * Project Title: Finding Call Numbers
 */

#region Usings

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace DeweyDecimal_Latest.Forms
{
    public partial class WinForm : Form
    {
        #region Declarations

        /// <summary>
        ///     Publically accessable to so that file worker can handle the program when the user chooses to play again
        /// </summary>
        public bool PlayAgain { get; private set; }

        /// <summary>
        ///     Uses to retrieve number of lives the user has left from file worker, to display on dialog
        /// </summary>
        public string lives { get; private set; }

        /// <summary>
        ///     New panel to draw underneath 'Yes'
        /// </summary>
        private Panel linePanelYes;

        /// <summary>
        ///     New panel to draw underneath 'No'
        /// </summary>
        private Panel linePanelNo;

        #endregion

        public WinForm(int lives)
        {
            InitializeComponent();

            // Dont allow resizing
            HandleWindowState();

            // Drawing the line under 'Yes' selected, and hiding it on start
            linePanelYes = new Panel
            {
                Height = 2,
                Width = lblYes.Width,
                BackColor = Color.Black,
                Location = new Point(lblYes.Left, lblYes.Bottom),
                Visible = false
            };

            // Drawing the line under 'No' selected, and hiding it on start
            linePanelNo = new Panel
            {
                Height = 2,
                Width = lblNo.Width,
                BackColor = Color.Black,
                Location = new Point(lblNo.Left, lblNo.Bottom),
                Visible = false
            };


            // Add controls to the form
            Controls.Add(lblYes);
            Controls.Add(linePanelYes);
            Controls.Add(lblNo);
            Controls.Add(linePanelNo);

            lblLives.Text = "x " + lives;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Ensures user cant resize the window
        /// </summary>
        private void HandleWindowState()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Click event for user selected 'Yes' label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblYes_Click(object sender, EventArgs e)
        {
            PlayAgain = true;
            this.Close();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///      Click event for user selected 'No' label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblNo_Click(object sender, EventArgs e)
        {
            PlayAgain = false;

            // Continue with navigating to the main menu
            var menu = new StartMenu();
            menu.Show();
            this.Close();
            FindingCallNumberGame obj = (FindingCallNumberGame)Application.OpenForms["FindingCallNumberGame"];
            obj.Close();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Alters cursor to be a hand, and sets the under line panel to visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblYes_MouseEnter(object sender, EventArgs e)
        {
            linePanelYes.Visible = true;
            Cursor = Cursors.Hand;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///      Alters cursor to go back to default, and sets the under line panel to invisible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblYes_MouseLeave(object sender, EventArgs e)
        {
            linePanelYes.Visible = false;
            Cursor = Cursors.Default;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///      Alters cursor to be a hand, and sets the under line panel to visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblNo_MouseEnter(object sender, EventArgs e)
        {
            linePanelNo.Visible = true;
            Cursor = Cursors.Hand;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///      Alters cursor to go back to default, and sets the under line panel to invisible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblNo_MouseLeave(object sender, EventArgs e)
        {
            linePanelNo.Visible = false;
            Cursor = Cursors.Default;
        }
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //
