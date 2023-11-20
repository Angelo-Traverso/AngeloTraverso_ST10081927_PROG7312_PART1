/*
 * Student Name: Angelo Traverso
 * Student Number: ST10081927
 * Project: Programming POE Part 2
 * Project Title: Farm Central Prototype Website
 */

using AxWMPLib;
using System;
using System.Drawing;
using System.Numerics;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimerLib;

namespace DeweyDecimal_Latest
{
    public partial class FindingCallNumberTreeControl : UserControl
    {

        /// <summary>
        ///     Event Handler to be notfied if th egame is completed
        /// </summary>
        public event EventHandler GameCompleted;

        /// <summary>
        ///     storing game completion state
        /// </summary>
        private bool isGameCompleted = false;

        /// <summary>
        ///     Holds the quickest completion time by the user
        /// </summary>
        private TimeSpan quickestTime = new TimeSpan(0, 1, 0);
        /// <summary>
        ///     
        /// </summary>
        FileWorker fileWorker;
        /// <summary>
        ///     Insantiating SoundPlayer
        /// </summary>
        private SoundPlayer soundPlayer = new SoundPlayer();

        /// <summary>
        ///     Using windows media player to play background music due to an issue soptting music with my own
        /// </summary>
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        /// <summary>
        ///     Instance of deweyTimer Library
        /// </summary>
        private Class1 deweyTimer;

        /// <summary>
        ///     Holding custom green color hex 
        /// </summary>
        private string green_hex = "#12ED1B";

        /// <summary>
        ///     Keeps track of the elapsed time for th eusers' game
        /// </summary>
        private int elapsedTimeInSeconds = 0;

        /// <summary>
        ///     Holds the number of games the user has played
        /// </summary>
        private int numOfGames = 0;

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Constructor
        /// </summary>
        public FindingCallNumberTreeControl()
        {

            InitializeComponent();

            // Instantiating fileWorker
            fileWorker = new FileWorker(this);

            // Attatching event handlers
            fileWorker.LifeLost += FileWorkerInstance_LifeLost;
            fileWorker.gameReset += GameHasReset;
            fileWorker.exitToMenu += UserWantsToExit;
            fileWorker.GameCompleted += FileWorker_GameCompleted;

            btnReady.Visible = true;
        }


        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     On Load method for the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindingCallNumberTreeControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                // Finding song, and playing it
                player.SoundLocation = @"Media\\AdventureWav.wav";
                player.Load();
               // player.Play();

                // Showing ready button
                btnReady.Visible = true;
                btnReady.Enabled = true;

                // Starting game by reading from file
                fileWorker.ReadFromFile();

                // Attempting to add a dropshaddow to a control
                pnlQuestionContainer.Paint += dropShadow;
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Method to apply a dropshaddow effect to control
        ///     Mike, https://stackoverflow.com/questions/2463519/drop-shadow-in-winforms-controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Handles user's lives (UI)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Executes when method is invoked by fileWorker.
        ///     If this reaches then the game has been reset.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameHasReset(object sender, EventArgs e)
        {
            ResetTimer();
            life3.Visible = true;
            life2.Visible = true;
            life1.Visible = true;
            isGameCompleted = false;
            deweyTimer.Start();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Executes when method is invoked by fileworker.
        ///     If this reaches then the user wants to exit to main menu, so we will redirect them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserWantsToExit(object sender, EventArgs e)
        {
            try
            {
                // Stopping music
                player.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping sound: {ex.Message}");
            }

            // Handling exit
            var parent = this.FindForm();
            var menu = new StartMenu();
            menu.Show();
            parent.Close();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Resets the timer
        /// </summary>
        private void ResetTimer()
        {
            deweyTimer.Stop();
            elapsedTimeInSeconds = 0;
            lblTimer.Text = "00:00:00";
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Executes when method is invoked by fileworker.
        ///     If this reaches then the program has a messagr to display for the user
        /// </summary>
        /// <param name="newText"></param>
        public async void UpdateLabelText(string newText)
        {
            if (lblMessageHeading == null || lblMessageHeading.IsDisposed)
            {
                return;
            }

            lblMessageHeading.Visible = true;
            lblMessageHeading.Text = newText;

            try
            {
                // Using Task.Delay to show the message for 2 seconds and then have it disappear
                await Task.Delay(2000);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Trying to perform something while the text is being displayed");
                return;
            }

            if (lblMessageHeading != null && !lblMessageHeading.IsDisposed && lblMessageHeading.IsHandleCreated)
            {
                if (lblMessageHeading.InvokeRequired)
                {
                    Invoke(new MethodInvoker(() => { lblMessageHeading.Visible = false; }));
                }
                else
                {
                    lblMessageHeading.Visible = false;
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Executes when method is invoked by fileworker.
        ///     If this reaches then the message label will be set to red
        /// </summary>
        public void SetLabelError()
        {
            lblMessageHeading.ForeColor = Color.Red;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Executes when method is invoked by fileworker.
        ///     If this reaches then the message label will be set to green
        /// </summary>
        public void SetLabelCorrect()
        {
            lblMessageHeading.ForeColor = Color.Green;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Timer_Tick event for the tree timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_tree_Tick(object sender, EventArgs e)
        {
            if (IsDisposed)
            {
                // Stop the timer if the form is disposed
                timer_tree.Stop();
                return;
            }

            elapsedTimeInSeconds++;

            if (isGameCompleted)
            {
                // Notifying subscribers that the game is completed
                GameCompleted?.Invoke(this, EventArgs.Empty);
            }

            if (lblTimer.InvokeRequired)
            {
                if (!IsDisposed && lblTimer != null && lblTimer.IsHandleCreated)
                {
                    lblTimer.Invoke(new Action(() => lblTimer.Text = TimeSpan.FromSeconds(elapsedTimeInSeconds).ToString()));
                }
            }
            else
            {
                lblTimer.Text = TimeSpan.FromSeconds(elapsedTimeInSeconds).ToString();
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Initializes game timer
        /// </summary>
        private void InitializeTimer()
        {
            deweyTimer = new Class1(1000);
            deweyTimer.Elapsed += timer_tree_Tick;
            deweyTimer.Start();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Executes when method is invoked by fileworker.
        ///     If this reaches then the game is completed successfully and the users'
        ///     personal best will be set if it is better than their previous attmept.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileWorker_GameCompleted(object sender, EventArgs e)
        {
            if (!IsDisposed)
            {
                var completedTime = TimeSpan.FromSeconds(elapsedTimeInSeconds);
                if (completedTime < quickestTime)
                {
                    lblPbDisplay.Text = completedTime.ToString();
                }
            }

        }

        /// <summary>
        ///     Ready Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReady_Click(object sender, EventArgs e)
        {
            if (numOfGames == 0)
            {
                // Setting the timer up
                InitializeTimer();

                // Binding panels to workerClass in order to alter value
                // Also displaying information to user and UI Components
                fileWorker.DisplayQuestion(new Panel[] { pnlOption1, pnlOption2, pnlOption3, pnlOption4 }, lblQuestion);

                btnReady.Enabled = false;
                btnReady.Visible = false;
            }

            numOfGames++;
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var message = MessageBox.Show("You are about to exit, all your progress will be lost!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (message == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {

            var message = MessageBox.Show("Are you sure you want to go to the Main Menu?\nYour current progress will be lost.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (message == DialogResult.Yes)
            {
                // Attempt to stop the sound
                try
                {
                    player.Stop();
                    //  soundPlayer.Dispose();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error stopping sound: {ex.Message}");
                }

                // Continue with navigating to the main menu
                var parent = this.FindForm();
                var menu = new StartMenu();
                menu.Show();
                parent.Close();
            }
        }

        private void btnReady_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void btnReady_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //
