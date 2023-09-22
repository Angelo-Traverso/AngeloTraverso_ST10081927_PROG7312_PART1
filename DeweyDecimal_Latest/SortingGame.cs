/*
 *  Full Name: Angelo Traverso
 *  Student Number: ST10081927
 *  Subject: Programming 3B
 *  Code: PROG7312
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class SortingGame : UserControl
    {

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
        ///     Map to store books and the nearest distance to each placeholder
        /// </summary>
        private Dictionary<string, Panel> bookCallNumberMap = new Dictionary<string, Panel>();

        /// <summary>
        ///     Initialize
        /// </summary>
        public SortingGame()
        {

            InitializeComponent();

            PlayBackgroundMusic();

            PopulateBooks(bookList);

            PopulatePlaceHolder(placeHolderList);

            GenerateRandomBookOrder();

            InitializeTimer();

            LoadBackground();

            SetDraggable(bookList);

            InitializeCircleProgressBar();
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
            game_timer.Interval = 1000;

            game_timer.Start();
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

        // ------------------------------------------------------------------ //

        /// <summary>
        ///     Finding nearest panel placeholder  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlBook_MouseUp(object sender, MouseEventArgs e)
        {
            // Determining the selected book
            Control selectedBook = sender as Control;

            if (selectedBook == null)
            {
                return;
            }

            Book selectedBookObject = bookObjectList.Find(book => book.BookPanel == selectedBook);
            Dictionary<Panel, double> distanceToPlaceHolder = CalculateDistancesToPlaceholders(selectedBookObject);

            Panel closestPlaceholder = GetClosestPlaceholder(distanceToPlaceHolder);

            if (closestPlaceholder == null)
            {
                return;
            }

            HandleBookPlacement(selectedBookObject, closestPlaceholder);
        }

        // -------------------------------- End Mouse Events -------------------------------- //
        /// <summary>
        ///     Loads image background
        /// </summary>
        private async void LoadBackground()
        {
            // Load the background image in a separate thread
            Bitmap image = await Task.Run(() =>
            {
                return LoadImageFromPath("Images\\podium.png");
            });

            // Update the UI (background image) on the main thread
            if (image != null)
            {
                this.Invoke((Action)delegate
                {
                    btnLeaderboard.BackgroundImage = image;
                    btnLeaderboard.BackgroundImageLayout = ImageLayout.Stretch;
                });
            }
        }

        private Bitmap LoadImageFromPath(string imagePath)
        {
            try
            {
                return new Bitmap(imagePath);
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
            for (int i = 1; i <= 10; i++)
            {
                Panel unsortedPanel = Controls.Find($"pnlUnsorted{i}", true).FirstOrDefault() as Panel;

                if (unsortedPanel == null)
                {
                    return;
                }
                else
                {
                    tempList.Add(unsortedPanel);
                    unsortedPanel.MouseDown += pnlBook_MouseDown;
                    unsortedPanel.MouseUp += pnlBook_MouseUp;
                    unsortedPanel.BorderStyle = BorderStyle.FixedSingle;
                    unsortedPanel.Tag = unsortedPanel.Location;
                    unsortedPanel.BackColor = GenerateRandomColor();

                    // Create a label for the call number
                    Label callNumberLabel = new Label
                    {
                        Text = GenerateRandomCallingNumber(i, 5),
                        Dock = DockStyle.Bottom, // Position the label at the bottom of the panel
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        AutoEllipsis = true,
                        Font = new Font("Arial", 8, FontStyle.Regular)
                    };

                    // Create book object
                    Book book = new Book(callNumberLabel.Text, unsortedPanel, GenerateRandomColor());

                    // Add Book to list
                    bookObjectList.Add(book);

                    // Add the label to the panel
                    unsortedPanel.Controls.Add(callNumberLabel);

                    // Add the panel and call number to the dictionary
                    string callNumber = $"Call Number: {i}";

                    bookCallNumberMap.Add(callNumber, unsortedPanel);
                }
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



        /// <summary>
        ///     Calculates the distance from book to placeholder
        ///     Returns a dictionairy with the panel and its distance
        /// </summary>
        /// <param name="selectedBook"></param>
        /// <returns></returns>
        private Dictionary<Panel, double> CalculateDistancesToPlaceholders(Book selectedBook)
        {
            Dictionary<Panel, double> distanceToPlaceHolder = new Dictionary<Panel, double>();

            foreach (Panel panel in placeHolderList)
            {
                double distance = CalculateDistance(selectedBook.BookPanel, panel);

                if (!distanceToPlaceHolder.ContainsKey(panel))
                    distanceToPlaceHolder.Add(panel, distance);
            }

            return distanceToPlaceHolder;
        }

        /// <summary>
        ///     Reuturns the closest placeholder panel's key 
        /// </summary>
        /// <param name="distanceToPlaceHolder"></param>
        /// <returns></returns>
        private Panel GetClosestPlaceholder(Dictionary<Panel, double> distanceToPlaceHolder)
        {
            var closestPair = distanceToPlaceHolder.OrderBy(pair => pair.Value).FirstOrDefault();

            return closestPair.Key;
        }

        /// <summary>
        ///     Handles the placement of books
        /// </summary>
        /// <param name="selectedBook"></param>
        /// <param name="closestPlaceholder"></param>
        private void HandleBookPlacement(Book selectedBook, Panel closestPlaceholder)
        {
            bool isOccupied = IsPlaceholderOccupied(closestPlaceholder, out Control occupyingBook);

            if (!isOccupied)
            {
                selectedBook.BookPanel.Location = closestPlaceholder.Location;

                PlaySound("Wink.mp3");

                UpdateProgressBar();
            }
            else
            {
                HandleOccupiedPlaceholder(selectedBook, occupyingBook);
            }

            CheckAllBooksPlaced();
        }

        /// <summary>
        ///     Handles the occupied placeholders
        /// </summary>
        /// <param name="selectedBook"></param>
        /// <param name="closestPlaceholder"></param>
        /// <param name="occupyingBook"></param>
        private void HandleOccupiedPlaceholder(Book selectedBook, Control occupyingBook)
        {
            if (occupyingBook != null)
            {
                selectedBook.BookPanel.Location = (Point)selectedBook.BookPanel.Tag;

                // Assuming the call number is in the first control (label)
                string callNumber = (occupyingBook.Controls[0] as Label)?.Text;
                DecrementProgressBar();
            }
            else
            {
                MessageBox.Show("Error: Occupying book is null.");
            }
        }

        // ------------------------------------------------------------------ //

        /// <summary>
        ///     Calculates the distance between 2 controls (book and placeholder)
        /// </summary>
        /// <param name="selectedBook"></param>
        /// <param name="placeholder"></param>
        /// <returns></returns>
        private double CalculateDistance(Control selectedBook, Control placeholder)
        {
            Point selectedBookCenter = new Point(selectedBook.Left + selectedBook.Width / 2, selectedBook.Top + selectedBook.Height / 2);

            Point placeholderCenter = new Point(placeholder.Left + placeholder.Width / 2, placeholder.Top + placeholder.Height / 2);

            return Math.Sqrt(Math.Pow(selectedBookCenter.X - placeholderCenter.X, 2) + Math.Pow(selectedBookCenter.Y - placeholderCenter.Y, 2));
        }

        // ------------------------------------------------------------------ //

        /// <summary>
        ///     Method generates random colors
        /// </summary>
        /// <returns></returns>
        private Color GenerateRandomColor()
        {
            // Generate random RGB values
            int red = random.Next(256);
            int green = random.Next(256);
            int blue = random.Next(256);

            return Color.FromArgb(red, green, blue);
        }

        // ------------------------------------------------------------------ //

        /// <summary>
        ///     Method to play a sound, url path is passed so that the method is dynamic
        /// </summary>
        /// <param name="url"></param>
        private async void PlaySound(string url)
        {
            await Task.Run(() =>
            {
                // Play the music
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(OnPlayStateChange);

                // Set the URL and play the music
                wplayer.URL = $"Media\\{url}";
                wplayer.controls.play();
            });
        }

        /// <summary>
        ///     Pause the mp3
        /// </summary>
        /// <param name="mPlayer"></param>
        private void Pause(WMPLib.WindowsMediaPlayer mPlayer)
        {
            mPlayer.controls.pause();
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
        /// <summary>
        ///     Method to validate whether a placeholder is occupied or not.
        ///     The method returns a boolean and the control which is occupying the space
        /// </summary>
        /// <param name="placeholder"></param>
        /// <param name="occupyingBook"></param>
        /// <returns></returns>
        private bool IsPlaceholderOccupied(Panel placeholder, out Control occupyingBook)
        {
            foreach (Control bookPanel in bookList)
            {
                // Check if the book panel is at the same location as the placeholder
                if (bookPanel.Location == placeholder.Location)
                {
                    occupyingBook = bookPanel;
                    return true;  // Placeholder is occupied
                }
            }

            occupyingBook = null;

            // Placeholder is not occupied
            return false;
        }

        /// <summary>
        ///     Generates random book order
        /// </summary>
        private void GenerateRandomBookOrder()
        {
            List<string> callNumbers = new List<string>();

            for (int i = 0; i < bookObjectList.Count; i++)
            {
                callNumbers.Add(bookObjectList[i].CallingNumber);
            }

            correctBookOrder = callNumbers.OrderBy(callNumber => callNumber).ToList();
        }

        /// <summary>
        ///     Checks whether all books are placed or not
        /// </summary>
        private void CheckAllBooksPlaced()
        {
            allBooksPlaced = true;

            foreach (Panel placeholder in placeHolderList)
            {
                bool isOccupied = IsPlaceholderOccupied(placeholder, out Control occupyingBook);

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

        /// <summary>
        ///     Generate a random calling number
        /// </summary>
        /// <param name="bookIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GenerateRandomCallingNumber(int bookIndex, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            Random random = new Random();

            // Using the book's index as a unique part of the calling number for now
            string uniquePart = bookIndex.ToString();

            string randomPart = new string(Enumerable.Repeat(chars, length - uniquePart.Length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return uniquePart + randomPart;
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Checks the users' order of the books for a score
        /// </summary>
        private void CheckBookOrder()
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

            Statistics stats = new Statistics();

            double points = 0.0;

            if (isCorrectOrder)
            {
                points = circPbPoints.Value + 5;

                // Give reward or award points
                MessageBox.Show("Congratulations! You placed the books in the correct order.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                attempts += 1;

                circPbPoints.Value = Convert.ToInt32((double)points);
                circPbPoints.Text = points.ToString();

                stats.CaptureStats(statsList, points, attempts);

            }
            else
            {
                PlaySound("lose.mp3");

                MessageBox.Show("Sorry, the books are not placed in the correct order. Try again.", "Incorrect Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                attempts += 1;

                points -= 5;

                circPbPoints.Value = Convert.ToInt32((double)points);
                circPbPoints.Text = points.ToString();

                stats.CaptureStats(statsList, points, attempts);
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
                bool isOccupied = IsPlaceholderOccupied(placeholder, out Control occupyingBook);

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

            lblTimer.Text = "Elapsed Time: " + TimeSpan.FromSeconds(elapsedTimeInSeconds).ToString();
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

            int decrementAmount = 10;  // You may adjust this value based on your requirement

            // Ensuring the value doesn't go below 0
            int newValue = currentValue - decrementAmount;

            // Update the progress bar value
            progressBookPlacement.Value = newValue;
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Resets game to its original state
        /// </summary>

        private void ResetGame()
        {
            elapsedTimeInSeconds = 0;
            lblTimer.Text = "Elapsed Time: 00:00:00";

            placedBookList.Clear();
            correctBookOrder.Clear();
            allBooksPlaced = false;

            progressBookPlacement.Value = 0;

            foreach (Book book in bookObjectList)
            {
                book.BookPanel.Location = (Point)book.BookPanel.Tag;
            }

            PopulatePlaceHolder(placeHolderList);

            GenerateRandomBookOrder();

            game_timer.Interval = 1000;

            game_timer.Start();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     PLays background music
        /// </summary>
        private async void PlayBackgroundMusic()
        {
            while (true)
            {
                await Task.Run(() =>
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
                });
            }
        }

        /// <summary>
        ///     Reset game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private bool IsReleasedOnPlacedBook(Control selectedBook, out Panel placedBookPanel)
        {
            foreach (Panel placeholder in placeHolderList)
            {
                if (selectedBook.Bounds.IntersectsWith(placeholder.Bounds))
                {
                    // The mouse was released on top of a placed book
                    placedBookPanel = placeholder;
                    return true;
                }
            }

            // Mouse was not released on top of a placed book
            placedBookPanel = null;
            return false;
        }
    }
}
