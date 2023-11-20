/*
 * Student Name: Angelo Traverso
 * Student Number: ST10081927
 * Project: Programming POE Part 2
 * Project Title: Farm Central Prototype Website
 */

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimerLib;

namespace DeweyDecimal_Latest
{
    public partial class FindingCallNumberTreeControl : UserControl
    { 
        #region Declarations
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
        ///     Using windows media player to play background music due to an issue soptting music with my own
        /// </summary>
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        /// <summary>
        ///     Instance of deweyTimer Library
        /// </summary>
        private Class1 deweyTimer;

        /// <summary>
        ///     Keeps track of the elapsed time for th eusers' game
        /// </summary>
        private int elapsedTimeInSeconds = 0;

        /// <summary>
        ///     Holds the number of games the user has played
        /// </summary>
        private int numOfGames = 0;
        #endregion

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
        ///     Starts music and waits for user response to begin game
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
                player.Play();

                // Showing ready button
                btnReady.Visible = true;
                btnReady.Enabled = true;

                // Starting game by reading from file
                fileWorker.ReadFromFile();
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Handles user's lives UI components
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
            // Checks to see if the label is null or disposed, and returns if true
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

            // Checking if the label is not null, not disposed, and its handle is created
            if (lblMessageHeading != null && !lblMessageHeading.IsDisposed && lblMessageHeading.IsHandleCreated)
            {
                // Checking if invoking is required for updating the label visibility
                if (lblMessageHeading.InvokeRequired)
                {
                    // Invoking the update of the label visibility
                    Invoke(new MethodInvoker(() => { lblMessageHeading.Visible = false; }));
                }
                else
                {
                    // Updating the label visibility directly if invoking is not required
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
            // First checking if the form is disposed - stop the timer if its true
            if (IsDisposed)
            {
                // Stop the timer if the form is disposed
                timer_tree.Stop();
                return;
            }

            // Incrementing elapsed time
            elapsedTimeInSeconds++;

            // Checking if the game is completed
            if (isGameCompleted)
            {
                // Notifying subscribers that the game is completed
                GameCompleted?.Invoke(this, EventArgs.Empty);
            }

            // Checking to see if invoking is required for updating the timer label
            if (lblTimer.InvokeRequired)
            {
                if (!IsDisposed && lblTimer != null && lblTimer.IsHandleCreated)
                {
                    // Invoking the update of timer label in the UI thread
                    lblTimer.Invoke(new Action(() => lblTimer.Text = TimeSpan.FromSeconds(elapsedTimeInSeconds).ToString()));
                }
            }
            else
            {
                // Updating the timer label directly if invoking is not required
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

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Ready Button Click Event
        ///     Starts the game and disables/hides button
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

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Changes users cursor to hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Changes users' cursor to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Setting cursor to Hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Setting cursor to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Ensuring user wants to exit the application, and exiting if wanted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            var message = MessageBox.Show("You are about to exit, all your progress will be lost!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (message == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Takes user back to the home/menu form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, EventArgs e)
        {
            var message = MessageBox.Show("Are you sure you want to go to the Main Menu?\nYour current progress will be lost.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (message == DialogResult.Yes)
            {
                StopMusic();

                var parent = this.FindForm();
                var menu = new StartMenu();
                menu.Show();
                parent.Close();
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Stops the music from playing in the background
        /// </summary>
        public void StopMusic()
        {
            try
            {
                player.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping sound: {ex.Message}");
            }
        }
        
        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Changes cursor to hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReady_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Changes cursor to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReady_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //
