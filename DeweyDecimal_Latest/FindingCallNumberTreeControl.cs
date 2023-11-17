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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DeweyDecimal_Latest
{
    public partial class FindingCallNumberTreeControl : UserControl
    {

        private bool isGameStarted = false;
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

        private int numOfGames = 0;

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Constructor
        /// </summary>
        public FindingCallNumberTreeControl()
        {
            if (!this.DesignMode)
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

                _ = PlaySound("Adventure.mp3");
            }
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
            // Your existing code for game reset
            deweyTimer.Stop();
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
            var startMenu = new StartMenu();
            startMenu.Show();
            this.Hide();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Resets the timer
        /// </summary>
        private void ResetTimer()
        {
            elapsedTimeInSeconds = 0;
            lblTimer.Text = "00:00:00";
            deweyTimer.Stop();
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

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Executes when method is invoked by fileworker.
        ///     If this reaches then the program has a messagr to display for the user
        /// </summary>
        /// <param name="newText"></param>
        public void UpdateLabelText(string newText)
        {
            lblMessageHeading.Visible = true;
            lblMessageHeading.Text = newText;

          
                Task.Delay(3000).ContinueWith(_ =>
                {
                    Invoke(new MethodInvoker(() => { lblMessageHeading.Visible = false; }));
                });
            
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
            elapsedTimeInSeconds++;

            if (!IsHandleCreated)
            {
                // Stoping the timer once the form is closed
                timer_tree.Stop();

                // Early return to remove redundant iterations
                return;
            }

            if (isGameCompleted)
            {
                // Notifying subscribers that the game is completed
                GameCompleted?.Invoke(this, EventArgs.Empty);
            }

            if (lblTimer.InvokeRequired)
            {
                lblTimer.Invoke(new Action(() => lblTimer.Text = TimeSpan.FromSeconds(elapsedTimeInSeconds).ToString()));
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
            var completedTime = TimeSpan.FromSeconds(elapsedTimeInSeconds);
            if (completedTime < quickestTime)
            {
                lblPbDisplay.Text = completedTime.ToString();
            }

        }

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
            else
            {
                isGameStarted = true;
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
                System.Environment.Exit(1);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
           
            var message = MessageBox.Show("Are you sure you want to go to the Main Menu?\nYour current progress will be lost.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (message == DialogResult.Yes)
            {
                soundPlayer.StopSound();
                var parent = this.FindForm();
                var menu = new StartMenu();
                menu.Show();
                parent.Close();
            }
        }

    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //
