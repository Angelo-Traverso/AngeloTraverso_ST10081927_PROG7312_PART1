using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class SortingGame : UserControl
    {

        private Random random = new Random();

        // List to hold all book panels
        List<Panel> bookList = new List<Panel>();

        // List to hold placeHolderPanels
        List<Panel> placeHolderList = new List<Panel>();

        private Dictionary<string, Panel> bookCallNumberMap = new Dictionary<string, Panel>();

        public SortingGame()
        {
            InitializeComponent();

            PopulateBooks(bookList);
            PopulatePlaceHolder(placeHolderList);

            SetDraggable(bookList);
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method to set a list of panels to draggable
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
                        Text = $"Call Number: {i}",
                        Dock = DockStyle.Bottom, // Position the label at the bottom of the panel
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.White,
                        AutoEllipsis = true,
                        Font = new Font("Arial", 8, FontStyle.Regular)
                    };

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

        // ------------------------------------------------------------------ //

        /// <summary>
        ///     MouseDown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlBook_MouseDown(object sender, MouseEventArgs e)
        {
            ControlExtension.Draggable(sender as Control, true);
            Cursor.Current = Cursors.Hand;
            //PlaySound("Pickup.mp3");
        }

        // ------------------------------------------------------------------ //

        /// <summary>
        ///     Finding nearest panel placeholder  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlBook_MouseUp(object sender, MouseEventArgs e)
        {
            // Determining the selected book/panel
            Control selectedBook = sender as Control;

            // Dictionary to hold each panel and their distance to placeholder
            Dictionary<Panel, double> distanceToPlaceHolder = new Dictionary<Panel, double>();

            foreach (Panel panel in placeHolderList)
            {
                // Calculate the distance between the selected book and the current placeholder
                double distance = CalculateDistance(selectedBook, panel);

                // Store the distance in the Dictionary
                distanceToPlaceHolder.Add(panel, distance);
            }

            var closestPair = distanceToPlaceHolder.OrderBy(pair => pair.Value).FirstOrDefault();

            if (closestPair.Key == null)
            {
                // No closest placeholder found
                return; 
            }

            Panel closestPlaceholder = closestPair.Key;
            double closestDistance = closestPair.Value;

            bool isOccupied = IsPlaceholderOccupied(closestPlaceholder);

            if (!isOccupied)
            {
                selectedBook.Location = closestPlaceholder.Location;
                PlaySound("Wink.mp3");
            }
            else
            {
                // Send book back to original position
                selectedBook.Location = (Point)selectedBook.Tag;
            }
        }

        // ------------------------------------------------------------------ //
        private bool IsPlaceholderOccupied(Panel placeholder)
        {
            foreach (Control bookPanel in bookList)
            {
                if (bookPanel.Location == placeholder.Location)
                {
                    return true;
                }
            }

            return false;
        }

        // ------------------------------------------------------------------ //

        /// <summary>
        ///     Method to calculate the distance between 2 controls (book and placeholder)
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

        /// <summary>
        ///     Method to play a sound, url path is passed so that the method is dynamic
        /// </summary>
        /// <param name="url"></param>
        private void PlaySound(string url)
        {

            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

            wplayer.URL = $"Media\\{url}";
            wplayer.controls.play();

        }
    }
}
