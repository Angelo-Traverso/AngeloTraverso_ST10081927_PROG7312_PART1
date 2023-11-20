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
    public partial class GameOverSplash : Form
    {
        /// <summary>
        ///     Publically accessable to so that file worker can handle the program when the user chooses to play again
        /// </summary>
        public bool PlayAgain { get; private set; }

        /// <summary>
        ///     New panel to draw underneath 'Yes'
        /// </summary>
        private Panel linePanelYes;

        /// <summary>
        ///     New panel to draw underneath 'No'
        /// </summary>
        private Panel linePanelNo;

        public GameOverSplash()
        {
            InitializeComponent();

            // Prevents user from trying to resize the dialog
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

        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Ensures the user cant resize the dialog
        /// </summary>
        private void HandleWindowState()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///      Alters cursor to be a hand, and sets the under line panel to visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblYes_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            // Show the line when the mouse enters
            linePanelYes.Visible = true;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Alters cursor to go back to default, and sets the under line panel to invisible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblYes_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            // Hide the line when the mouse leaves
            linePanelYes.Visible = false;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Alters cursor to be a hand, and sets the under line panel to visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblNo_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            linePanelNo.Visible = true;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Alters cursor to go back to default, and sets the under line panel to invisible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblNo_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            linePanelNo.Visible = false;
        }

        /// <summary>
        ///     Click event for user selected 'Yes' label
        ///     Starts the game over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblYes_Click(object sender, EventArgs e)
        {
            PlayAgain = true;
            this.Close();
        }


        /// <summary>
        ///     Click event for user selected 'No' label
        ///     Closed the game, and launches menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblNo_Click(object sender, EventArgs e)
        {
            var menu = new StartMenu();
            menu.Show();
            this.Close();
            FindingCallNumberGame obj = (FindingCallNumberGame)Application.OpenForms["FindingCallNumberGame"];

            // If the form is not already null, and Invoking is required, then Invoke the close event, otherwise just close it
            if (obj != null)
            {
                if (obj.InvokeRequired)
                {
                    obj.Invoke(new Action(() => obj.Close()));
                }
                else
                {
                    obj.Close();
                }
            }
        }
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //