/*
 *  Full Name: Angelo Traverso
 *  Student Number: ST10081927
 *  Subject: Programming 3B
 *  Code: PROG7312
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class SortingGame : UserControl
    {
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
        ///     Holds number of user attempts to get it correct
        /// </summary>
        private int attempts = 0;

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

        /// <summary>
        ///     Keep state of all books being placed
        /// </summary>
        private bool allBooksPlaced = false;

        private Random random = new Random();



        /// <summary>
        ///     Instance of BookPlacementHandler
        /// </summary>
        private BookPlacementHandler BookPlacementHandler = new BookPlacementHandler();

        /// <summary>
        ///     Instance of SoundPlayer
        /// </summary>
        private SoundPlayer soundPlayer = new SoundPlayer();

        /// <summary>
        ///     Initialize
        /// </summary>
        public SortingGame()
        {

            InitializeComponent();

            // PlayBackgroundMusic();
            ShuffleBooks();

            PopulateBooks(bookList);

            PopulatePlaceHolder(placeHolderList);

            GenerateCorrectBookOrder();

            InitializeTimer();

            SetDraggable(bookList);

            InitializeCircleProgressBar();
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

        public void InitializeCircleProgressBar()
        {
            circPbPoints.Value = 0;
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

            // Add Pickup sound
        }

        /// <summary>
        ///     Finding nearest panel placeholder  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlBook_MouseUp(object sender, MouseEventArgs e)
        {
            // Log the event trigger
            Debug.WriteLine("MouseUp event triggered."); 

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

            Debug.WriteLine($"Selected book location: {selectedBookObject.BookPanel.Location}");
            Debug.WriteLine($"Placeholder location: {closestPlaceholder.Location}");
        }

        // -------------------------------- End Mouse Events -------------------------------- //

        private async Task<Bitmap> LoadImageFromPath(string imagePath)
        {
            try
            {
                return await Task.Run(() => new Bitmap(imagePath));
            }
            catch (Exception ex)
            {
                // Handle errors loading image
                Console.WriteLine("Error loading the background image: " + ex.Message);
                return null;
            }
        }

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
                    unsortedPanel.BorderStyle = BorderStyle.FixedSingle;
                    unsortedPanel.Tag = unsortedPanel.Location;
                    unsortedPanel.BackColor = GenerateRandomColor();
                    unsortedPanel.Enabled = true;

                    // Create a label for the call number
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
/*
                    string callNumber = $"Call Number: {i}";

                    bookCallNumberMap.Add(callNumber, unsortedPanel);*/
                }
            }
        }

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
                    PlaySound("Wink.mp3");
                    selectedBook.IsPlaced = true;
                    UpdateProgressBar();
                }
                else 
                {

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
                // Send back to original location
                selectedBook.BookPanel.Location = (Point)selectedBook.BookPanel.Tag;

                DecrementProgressBar();
            }
            else
            {
                MessageBox.Show("Error: Occupying book is null.");
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method generates random colors
        /// </summary>
        /// <returns></returns>
        private Color GenerateRandomColor()
        {
            // Generate random RGB values
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

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Pause the mp3
        /// </summary>
        /// <param name="mPlayer"></param>
        private void StopSound(WMPLib.WindowsMediaPlayer mPlayer)
        {
            soundPlayer.StopSound();
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
        ///     Checks the users' order of the books for a score
        /// </summary>
        private async void CheckBookOrder()
        {
            const string PROGRESS_COMEPLETE = "Complete";

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

            Statistics stats = new Statistics();

            double points = 0.0;

            if (isCorrectOrder)
            {
                points = circPbPoints.Value + 5;

                lblScore.Text = (circPbPoints.Value + 5).ToString();

                gameTimer.Stop();
                EndGame();
                // Give reward or award points
                MessageBox.Show("Congratulations! You placed the books in the correct order.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblProgressBar.Text = PROGRESS_COMEPLETE;

                attempts += 1;

                circPbPoints.Value = Convert.ToInt32((double)points);
                circPbPoints.Text = points.ToString();

                rounds += 1;

                stats.CaptureStats(statsList, points, attempts, TimeSpan.FromSeconds(elapsedTimeInSeconds));

            }
            else
            {
                gameTimer.Stop();
                await PlaySound("lose.mp3");

                HighlightIncorrectPlacements(placedBookList);

                EndGame();
                MessageBox.Show("Sorry, the books are not placed in the correct order. Try again.", "Incorrect Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                lblProgressBar.Text = PROGRESS_COMEPLETE;

                attempts += 1;

                lblScore.Text = (circPbPoints.Value - 5).ToString();

                points = circPbPoints.Value - 5;

                try
                {
                    circPbPoints.Value = Convert.ToInt32((double)points); // Issue here
                }
                catch (Exception)
                {
                    circPbPoints.Value = 0;
                }
                circPbPoints.Text = points.ToString();

                stats.CaptureStats(statsList, points, attempts, TimeSpan.FromSeconds(elapsedTimeInSeconds));
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
        ///     Updates progress bar value
        /// </summary>
        private void UpdateProgressBar()
        {
            try
            {
                progressBookPlacement.Value += 10;
            }
            catch (Exception)
            {
                progressBookPlacement.Value = 100;
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Decrements progress bar value
        /// </summary>
        private void DecrementProgressBar()
        {
            // Decrease the progress bar value by a certain amount
            int currentValue = progressBookPlacement.Value;

            int decrementAmount = 10;

            // Ensuring the value doesn't go below 0
            int newValue = currentValue - decrementAmount;

            try
            {
                // Update the progress bar value
                progressBookPlacement.Value = newValue;
            }
            catch (Exception)
            {
                progressBookPlacement.Value = 0;
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Resets game to its original state
        /// </summary>
        private void ResetGame()
        {
            elapsedTimeInSeconds = 0;
            lblTimer.Text = "00:00:00";
            lblProgressBar.Text = "Completion Status";

            bookList.Clear();
            placedBookList.Clear();
            correctBookOrder.Clear();
            allBooksPlaced = false;

            progressBookPlacement.Value = 0;

            // Remove event handlers and reset book panels
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
        ///     PLays background music
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
        ///     Button click for Statistics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeaderboard_Click(object sender, EventArgs e)
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

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlSorted2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HighlightIncorrectPlacements(List<Book> placedBooks)
        {
            string temp = string.Empty;
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

            MessageBox.Show("Incorrect books: " + temp.ToString());
        }

        /// <summary>
        ///     Disabled panels when user is complete, so that they cant move the books
        /// </summary>
        private void EndGame()
        {
            foreach (var panel in bookList)
            {
                panel.Enabled = false;
            }
        }

        /// <summary>
        ///     Returns the number of incorrect placements made by the user
        /// </summary>
        /// <returns></returns>
        private int IncorrectPlacements()
        {
            int incorrectCount = 0;

            // Ensure correctBookOrder and placedBookList have the same count
            if (correctBookOrder.Count != placedBookList.Count)
                return -1;  // Error: Lists have different lengths

            for (int i = 0; i < correctBookOrder.Count; i++)
            {
                string correctCallNumber = correctBookOrder[i];
                string placedCallNumber = placedBookList[i].CallingNumber;

                if (!string.Equals(correctCallNumber, placedCallNumber))
                    incorrectCount++;
            }

            return incorrectCount;
        }
    }
}
// ......ooooooo000000 END FILE 0000000oooooo...... //
