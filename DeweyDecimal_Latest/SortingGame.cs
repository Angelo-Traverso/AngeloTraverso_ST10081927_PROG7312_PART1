/*
 *  Full Name: Angelo Traverso
 *  Student Number: ST10081927
 *  Subject: Programming 3B
 *  Code: PROG7312
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class SortingGame : UserControl
    {

        private DateTime pauseTime;  // Store the time when paused
        private TimeSpan remainingTime;  // Store the remaining time

        private bool isGameDone = false;
        /// <summary>
        ///     Instance of GameTimer
        /// </summary>
        private GameTimer gameTimer;

        /// <summary>
        ///     Holds the number of rounds the user has completed
        /// </summary>
        private int rounds = 0;

        /// <summary>
        ///     Dictionairy to hold the closest placeholder and distance
        /// </summary>
        private Dictionary<Panel, double> distanceToPlaceHolder = new Dictionary<Panel, double>();

        /// <summary>
        ///     Holds number of successful attempts
        /// </summary>
        private int successAttempts = 0;

        /// <summary>
        ///     Holds number of failed attempts by user
        /// </summary>
        private int failedAttempts = 0;

        /// <summary>
        ///     Holds elapsed time of timer
        /// </summary>
        private int elapsedTimeInSeconds = 0;

        /// <summary>
        ///     Holds all book panels
        /// </summary>
        List<Panel> bookList = new List<Panel>();

        /// <summary>
        ///     Holds place-holder Panels
        /// </summary>
        List<Panel> placeHolderList = new List<Panel>();

        /// <summary>
        ///     Holds book objects
        /// </summary>
        List<Book> bookObjectList = new List<Book>();

        /// <summary>
        ///     Stores books that have been placed
        /// </summary>
        List<Book> placedBookList = new List<Book>();

        /// <summary>
        ///     Holds the correct book order
        /// </summary>
        private List<string> correctBookOrder = new List<string>();

        /// <summary>
        ///     Holds user game stats
        /// </summary>
        private List<Statistics> statsList = new List<Statistics>();

        int score = 0;

        List<Point> bottomShelfPlacementPoints = new List<Point>();

        /// <summary>
        ///     Keep state of all books being placed
        /// </summary>
        private bool allBooksPlaced = false;

        private bool isPaused = true;

        private int numOfPauses = 0;

        private Random random = new Random();



        /// <summary>
        ///     Instance of BookPlacementHandler
        /// </summary>
        private BookPlacementHandler BookPlacementHandler = new BookPlacementHandler();

        /// <summary>
        ///     Instance of SoundPlayer
        /// </summary>
        private SoundPlayer soundPlayer = new SoundPlayer();

        Statistics stats = new Statistics();

        /// <summary>
        ///     Initialize
        /// </summary>
        public SortingGame()
        {
            InitializeComponent();

            _ = LoadBackgroundImageAsync();

            ShuffleBooks();

            PopulateBooks(bookList);

            PopulatePlaceHolder(placeHolderList);

            GenerateCorrectBookOrder();

            InitializeTimer();

            SetDraggable(bookList);

            SetPersonalBest();

            var pos = lblPersonalBest.Parent.PointToScreen(lblPersonalBest.Location);
            pos = pbBackground.PointToClient(pos);
            lblPersonalBest.Parent = pbBackground;
            lblPersonalBest.Location = pos;
            lblPersonalBest.BackColor = Color.Transparent;

            var pos2 = lblScore.Parent.PointToScreen(lblScore.Location);
            pos2 = pbBackground.PointToClient(pos2);
            lblScore.Parent = pbBackground;
            lblScore.Location = pos2;
            lblScore.BackColor = Color.Transparent;

            var pos3 = pnlTimer.Parent.PointToScreen(pnlTimer.Location);
            pos3 = pbBackground.PointToClient(pos3);
            pnlTimer.Parent = pbBackground;
            pnlTimer.Location = pos3;
            pnlTimer.BackColor = Color.Transparent;

           

        }

        private void StartGame()
        {
            var response = MessageBox.Show("Are you ready?\nClick 'Yes' to start, or 'No' to go back to the main menu", "Start Game", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (response == DialogResult.No)
            {
                StartMenu startMenu = new StartMenu();
                this.Hide();
                startMenu.Show();
            }
            else
            {
                // Uncomment for background image debugging
            }
        }

        private void GameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            elapsedTimeInSeconds = e.ElapsedTimeInSeconds;
            UpdateUITimer();  // Update your UI with the elapsed time
        }

        private void UpdateUITimer()
        {
            TimeSpan elapsedTime = TimeSpan.FromSeconds(elapsedTimeInSeconds);
            lblTimer.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }
        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Initializing game timer
        /// </summary>
        private void InitializeTimer()
        {
            gameTimer = new GameTimer(1000);
            gameTimer.Elapsed += GameTimer_Elapsed;
            gameTimer.Start();
        }

        // -------------------------------- Mouse Events -------------------------------- //
        /// <summary>
        ///     MouseDown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlBook_MouseDown(object sender, MouseEventArgs e)
        {
            ControlExtension.Draggable(sender as Control, true);

            Cursor.Current = Cursors.Hand;
        }
        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Finding nearest panel placeholder  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlBook_MouseUp(object sender, MouseEventArgs e)
        {
            Control selectedBook = sender as Control;

            if (selectedBook == null)
            {
                return;
            }

            Book selectedBookObject = bookObjectList.Find(book => book.BookPanel == selectedBook);
            Dictionary<Panel, double> distanceToPlaceHolder = BookPlacementHandler.CalculateDistancesToPlaceholders(selectedBookObject, placeHolderList);

            Panel closestPlaceholder = BookPlacementHandler.GetClosestPlaceholder(distanceToPlaceHolder);

            if (closestPlaceholder == null)
            {
                return;
            }

            HandleBookPlacement(selectedBookObject, closestPlaceholder);

            SetPersonalBest();
        }

        // -------------------------------- End Mouse Events -------------------------------- //

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method to set books panels to draggable
        /// </summary>
        private void SetDraggable(List<Panel> bList)
        {
            foreach (Control panel in bList)
            {
                panel.Draggable(true);
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method populates book list
        ///     Null is returned first to efficiancy
        /// </summary>
        private void PopulateBooks(List<Panel> tempList)
        {
            Book bookHelper = new Book();
            for (int i = 1; i <= 10; i++)
            {
                Panel unsortedPanel = Controls.Find($"pnlUnsorted{i}", true).FirstOrDefault() as Panel;
                bottomShelfPlacementPoints.Add(unsortedPanel.Location);

                if (unsortedPanel == null)
                {
                    return;
                }
                else
                {
                    unsortedPanel.Controls.Clear();

                    tempList.Add(unsortedPanel);
                    unsortedPanel.MouseDown += pnlBook_MouseDown;
                    unsortedPanel.MouseUp += pnlBook_MouseUp;
                    unsortedPanel.BorderStyle = BorderStyle.Fixed3D;
                    unsortedPanel.Tag = unsortedPanel.Location;
                    unsortedPanel.BackColor = GenerateRandomColor();
                    unsortedPanel.Enabled = true;

                    Label callNumberLabel = new Label();
                    callNumberLabel.Text = bookHelper.GenerateRandomCallingNumber(i, random);
                    callNumberLabel.Dock = DockStyle.Bottom;
                    callNumberLabel.TextAlign = ContentAlignment.MiddleCenter;
                    callNumberLabel.ForeColor = Color.Black;
                    callNumberLabel.AutoEllipsis = true;
                    callNumberLabel.Height = callNumberLabel.Height + 5;
                    callNumberLabel.Font = new Font("Arial", 8, FontStyle.Regular);


                    Book book = new Book(callNumberLabel.Text, unsortedPanel, GenerateRandomColor());

                    bookObjectList.Add(book);

                    unsortedPanel.Controls.Add(callNumberLabel);


                }
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method shuffles books so that they can be displayed randomly
        /// </summary>
        private void ShuffleBooks()
        {
            Random random = new Random();
            int n = correctBookOrder.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string value = correctBookOrder[k];
                correctBookOrder[k] = correctBookOrder[n];
                correctBookOrder[n] = value;
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method to populate the placeholder list
        /// </summary>
        /// <param name="tempList"></param>
        private void PopulatePlaceHolder(List<Panel> tempList)
        {
            for (int i = 1; i <= 10; i++)
            {
                Panel unsortedPanel = Controls.Find($"pnlSorted{i}", true).FirstOrDefault() as Panel;

                if (unsortedPanel == null)
                {
                    return;
                }
                else
                {
                    tempList.Add(unsortedPanel);
                }
            }
        }
        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Handles the placement of books
        /// </summary>
        /// <param name="selectedBook"></param>
        /// <param name="closestPlaceholder"></param>
        private void HandleBookPlacement(Book selectedBook, Panel closestPlaceholder)
        {
            bool isOccupied = BookPlacementHandler.IsPlaceholderOccupied(closestPlaceholder, bookList, out Control occupyingBook);


            if (!isOccupied)
            {
                if (!selectedBook.IsPlaced)
                {
                    _ = PlaySound("Wink.mp3");
                    selectedBook.IsPlaced = true;
                }

                selectedBook.BookPanel.Location = closestPlaceholder.Location;
            }
            else
            {
                HandleOccupiedPlaceholder(selectedBook, occupyingBook);
            }

            CheckAllBooksPlaced();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Handles the occupied placeholders
        /// </summary>
        /// <param name="selectedBook"></param>
        /// <param name="closestPlaceholder"></param>
        /// <param name="occupyingBook"></param>
        private void HandleOccupiedPlaceholder(Book selectedBook, Control occupyingBook)
        {
            // Handle swopping here

            if (occupyingBook != null)
            {
                selectedBook.BookPanel.Location = (Point)selectedBook.BookPanel.Tag;
            }
            else
            {
                MessageBox.Show("Error: Occupying book is null.");
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method generates random colors
        ///     Method is biased towards lighter colors
        /// </summary>
        /// <returns></returns>
        private Color GenerateRandomColor()
        {
            int red = random.Next(128, 256);
            int green = random.Next(128, 256);
            int blue = random.Next(128, 256);

            return Color.FromArgb(red, green, blue);
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method to play a sound, url path is passed so that the method is dynamic
        /// </summary>
        /// <param name="url"></param>
        private async Task PlaySound(string url)
        {
            await Task.Run(() =>
            {
                soundPlayer.PlaySound($"Media\\{url}");
            });
        }

        /// <summary>
        ///     Continue playing music
        /// </summary>
        /// <param name="newState"></param>
        private void OnPlayStateChange(int newState)
        {
            // Check if the media has ended
            if ((WMPLib.WMPPlayState)newState == WMPLib.WMPPlayState.wmppsStopped)
            {
                // Music has finished playing, do something if needed
                Console.WriteLine("Music finished playing.");
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Generates random book order
        /// </summary>
        private void GenerateCorrectBookOrder()
        {
            List<string> callNumbers = new List<string>();

            for (int i = 0; i < bookObjectList.Count; i++)
            {
                callNumbers.Add(bookObjectList[i].CallingNumber);
            }

            callNumbers.Sort();
            correctBookOrder = callNumbers.ToList();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Checks whether all books are placed or not
        /// </summary>
        private void CheckAllBooksPlaced()
        {
            allBooksPlaced = true;

            foreach (Panel placeholder in placeHolderList)
            {
                bool isOccupied = BookPlacementHandler.IsPlaceholderOccupied(placeholder, bookList, out Control occupyingBook);

                if (!isOccupied)
                {
                    allBooksPlaced = false;

                    break;
                }
            }

            if (allBooksPlaced)
            {
                game_timer.Stop();

                CheckBookOrder();
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method to set users' personal best time completed UI component
        /// </summary>
        private void SetPersonalBest()
        {
            string text = "Personal Best: ";
            lblPersonalBest.Text = text + stats.GetPersonalBest(statsList);
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Checks the users' order of the books for a score
        /// </summary>
        private async void CheckBookOrder()
        {
            LoadPlacedBooks();

            // Ensure the correct book order list is not empty
            if (correctBookOrder.Count == 0)
            {
                MessageBox.Show("Error: Correct book order list is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> placedBookCallNumbers = new List<string>();

            // Get the call numbers of the books in the order they were placed
            foreach (Book placedBook in placedBookList)
            {
                placedBookCallNumbers.Add(placedBook.CallingNumber);
            }

            // Check if the placed books are in the correct order
            bool isCorrectOrder = Enumerable.SequenceEqual(correctBookOrder, placedBookCallNumbers);





            if (isCorrectOrder)
            {


                lblScore.Text = "Score: " + (score + 5).ToString();

                gameTimer.Stop();
                EndGame();

                MessageBox.Show("Congratulations! You placed the books in the correct order.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                menuRestart.BackColor = Color.Green;

                successAttempts += 1;

                rounds += 1;

                stats.CaptureStats(statsList, score, successAttempts, failedAttempts, false, TimeSpan.FromSeconds(elapsedTimeInSeconds));

            }
            else
            {
                gameTimer.Stop();
                await PlaySound("lose.mp3");

                HighlightIncorrectPlacements(placedBookList);

                EndGame();
                MessageBox.Show("Sorry, the books are not placed in the correct order. Try again.", "Incorrect Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                failedAttempts += 1;

                menuRestart.BackColor = Color.Green;


                if (score - 5 >= 0)
                {
                    lblScore.Text = "Score: " + (score - 5).ToString();
                }
                else
                {
                    lblScore.Text = "Score: 0";
                    stats.CaptureStats(statsList, 0, successAttempts, failedAttempts, true, TimeSpan.FromSeconds(elapsedTimeInSeconds));
                }


            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Adds all place holder books to a list
        /// </summary>
        private void LoadPlacedBooks()
        {
            placedBookList.Clear();

            foreach (Panel placeholder in placeHolderList)
            {
                bool isOccupied = BookPlacementHandler.IsPlaceholderOccupied(placeholder, bookList, out Control occupyingBook);

                if (isOccupied && occupyingBook is Panel)
                {
                    Panel occupyingPanel = occupyingBook as Panel;

                    Book occupyingBookObject = bookObjectList.Find(book => book.BookPanel == occupyingPanel);

                    placedBookList.Add(occupyingBookObject);
                }
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Ticking timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void game_timer_Tick(object sender, EventArgs e)
        {
            elapsedTimeInSeconds++;

            lblTimer.Text = TimeSpan.FromSeconds(elapsedTimeInSeconds).ToString();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Resets game to its original state
        /// </summary>
        private void ResetGame()
        {
            elapsedTimeInSeconds = 0;
            lblTimer.Text = "00:00:00";

            bookList.Clear();
            placedBookList.Clear();
            correctBookOrder.Clear();
            allBooksPlaced = false;

            foreach (Book book in bookObjectList)
            {
                book.BookPanel.MouseDown -= pnlBook_MouseDown;
                book.BookPanel.MouseUp -= pnlBook_MouseUp;

                book.BookPanel.Location = (Point)book.BookPanel.Tag;
            }

            bookObjectList.Clear();
            placedBookList.Clear();
            correctBookOrder.Clear();
            distanceToPlaceHolder.Clear();

            PopulateBooks(bookList);

            GenerateCorrectBookOrder();

            SetDraggable(bookList);

            gameTimer.Stop();

            InitializeTimer();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Plays background music
        /// </summary>
        private void PlayBackgroundMusic()
        {
            while (true)
            {

                // Play the music
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(OnPlayStateChange);

                // Set the URL and play the music
                wplayer.URL = "Media\\Piano.mp3";
                wplayer.controls.play();

                // Wait for the music to finish or be paused
                while (wplayer.playState == WMPLib.WMPPlayState.wmppsPlaying || wplayer.playState == WMPLib.WMPPlayState.wmppsTransitioning)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Reset game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method Highlights incorrect placed books - red
        /// </summary>
        /// <param name="placedBooks"></param>
        private void HighlightIncorrectPlacements(List<Book> placedBooks)
        {
            for (int i = 0; i < placedBooks.Count; i++)
            {
                // Check if the book is incorrectly placed
                bool isIncorrect = correctBookOrder[i] != placedBooks[i].CallingNumber;


                if (isIncorrect)
                {
                    // Highlight the incorrectly placed book (e.g., change color)
                    placedBooks[i].BookPanel.BackColor = Color.Red;
                }
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Disabled panels when user is complete, so that they cant move the books
        /// </summary>
        private void EndGame()
        {
            DisableBooks();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Disables all book panels
        /// </summary>
        private void DisableBooks()
        {
            foreach (var panel in bookList)
            {
                panel.Enabled = false;
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Enabled book panels
        /// </summary>
        private void EnableBooks()
        {
            foreach (var panel in bookList)
            {
                panel.Enabled = true;
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Sets book panels to invisible
        /// </summary>
        private void HideBooks()
        {
            foreach (var panel in bookList)
            {
                panel.Visible = false;
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Sets book panels to visable
        /// </summary>
        private void ShowBooks()
        {
            foreach (var panel in bookList)
            {
                panel.Visible = true;
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method loads backghround image in a new thread
        /// </summary>
        /// <returns></returns>
        private async Task LoadBackgroundImageAsync()
        {
            string imagePath = @"Images\BackgroundRoom.jpg";
            Image backgroundImage = await Task.Run(() => LoadImageFromFile(imagePath));

            if (backgroundImage != null)
            {
                /* this.BackgroundImage = backgroundImage;
                 this.BackgroundImageLayout = ImageLayout.Stretch;*/
                pbBackground.Image = backgroundImage;
                pbBackground.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Loads image from a file path
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private Image LoadImageFromFile(string imagePath)
        {
            try
            {
                return Image.FromFile(imagePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load image: {ex.Message}");
                return null;
            }
        }

        private void menuMySkill_Click(object sender, EventArgs e)
        {
            if (statsList.Count == 0)
            {
                MessageBox.Show("You have no stats to display yet.\nComplete a game to get started.", "No data", MessageBoxButtons.OK, MessageBoxIcon.Information);  
            }
            else
            {
                StatisticsForm statsForm = new StatisticsForm(statsList);
                statsForm.ShowDialog();
            }
        }

        private void menuRestart_Click(object sender, EventArgs e)
        {
            menuRestart.BackColor = Color.Transparent;
            ResetGame();
        }

        private void menuPausePlay_Click(object sender, EventArgs e)
        {
            numOfPauses++;

            const string PAUSE = "Pause";
            const string PLAY = "Play";

            if (isPaused)
            {
                menuPausePlay.Text = PLAY;
                gameTimer.Pause();  // Pause the custom timer
                HideBooks();
                DisableBooks();
                isPaused = false;
            }
            else
            {
                menuPausePlay.Text = PAUSE;
                gameTimer.Resume();  // Resume the custom timer
                isPaused = true;
                EnableBooks();
                ShowBooks();
            }
        }

        private void menuReturnMenu_Click(object sender, EventArgs e)
        {
            var response = MessageBox.Show("You are about to leave your game.\nAll progress will be lost. Are you sure you want to leave?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (response == DialogResult.No)
            {
                return;
            }
            else
            {
                StartMenu sMenu = new StartMenu();
                sMenu.Show();
                this.ParentForm.Close();
            }
           
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Move the books around to find the correct dewey decimal order!\n" +
                "Have fun trying to beat your personal best time and score!\n\nTIP\n******\n* Drag a book from the bottom shelf and drop it on the top shelf to order them.\n" +
                "* left/right click a book to send it back to its original place.\n" +
                "* Click 'Pause/PLay' to pause and play the game.\n" +
                "* Click 'My Skill' to see how your games have been going.\n" +
                "* Click 'Restart' to restart your current game.\n" +
                "* In order to get a personal best score, you need to place all 10 books in the correct order.", "Help",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
// ------------------------------ ......ooooooo000000 END FILE 0000000oooooo...... ------------------------------ //
