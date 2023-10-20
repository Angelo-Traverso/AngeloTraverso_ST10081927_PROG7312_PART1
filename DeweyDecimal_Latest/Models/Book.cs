/*
 *  Full Name: Angelo Traverso
 *  Student Number: ST10081927
 *  Subject: Programming 3B
 *  Code: PROG7312
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    /// <summary>
    ///      struct used to ensure immutability and prevent unintended changes to the struct's fields.
    /// </summary>
    public struct Book
    {

        /// <summary>
        ///     Holds the books' calling number
        /// </summary>
        public string CallingNumber { get; set; }

        /// <summary>
        ///     Holds the books panel
        /// </summary>
        public Panel BookPanel { get; }
        
        /// <summary>
        ///  Stores the color of the book   
        /// </summary>
        public Color BookColor { get; }

        /// <summary>
        ///     Holds whether the book has been placed or not
        /// </summary>
        public bool IsPlaced { get; set; }

        /// <summary>
        ///     Book constructor
        /// </summary>
        /// <param name="callingNumber"></param>
        /// <param name="bookPanel"></param>
        /// <param name="bookColor"></param>
        public Book(string callingNumber, Panel bookPanel, Color bookColor)
        {
            CallingNumber = callingNumber;
            BookPanel = bookPanel;
            BookColor = bookColor;
            IsPlaced = false;
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method generates random colors
        ///     Method is biased towards lighter colors
        /// </summary>
        /// <returns></returns>
        public Color GenerateRandomColor(Random random)
        {
            int red = random.Next(128, 256);
            int green = random.Next(128, 256);
            int blue = random.Next(128, 256);

            return Color.FromArgb(red, green, blue);
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Generates a random calling number
        /// </summary>
        /// <param name="bookIndex"></param>
        /// <returns></returns>
        public string GenerateRandomCallingNumber(int bookIndex, Random random)
        {

            double topicNumber = random.NextDouble() * 1000;
            string formattedTopicNumber = $"{topicNumber:000.00}";

            string authorAbbreviation = GenerateAuthorAbbreviation(bookIndex, random);

            return $"{formattedTopicNumber}\n{authorAbbreviation}";
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Method that generates the first the letters of each surname
        /// </summary>
        /// <param name="bookIndex"></param>
        /// <returns></returns>
        private string GenerateAuthorAbbreviation(int bookIndex, Random random)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder abbreviation = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                int randomIndex = random.Next(0, characters.Length);
                abbreviation.Append(characters[randomIndex]);
            }

            return abbreviation.ToString();
        }

        #region Sorting Alogorithm (NOT BEING USED)
        /// <summary>
        ///     Sorts a list of books using the Quicksort algorithm within the specified range
        ///     (Maze, 2022)
        /// </summary>
        /// <param name="books"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private void SortBooks(List<Book> books, int left, int right)
        {
            if (left < right)
            {
                int partitionIndex = Partition(books, left, right);

                SortBooks(books, left, partitionIndex - 1);
                SortBooks(books, partitionIndex + 1, right);
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Partitions the list of books for the Quicksort algorithm based on a pivot element
        ///     (Maze, 2022)
        /// </summary>
        /// <param name="books"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private int Partition(List<Book> books, int left, int right)
        {
            string pivot = books[right].CallingNumber;
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (string.Compare(books[j].CallingNumber, pivot) < 0)
                {
                    i++;
                    Swap(books, i, j);
                }
            }

            Swap(books, i + 1, right);
            return i + 1;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Swaps two books in the list
        ///     (Maze, 2022)
        /// </summary>
        /// <param name="books"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Swap(List<Book> books, int i, int j)
        {
            Book temp = books[i];
            books[i] = books[j];
            books[j] = temp;
        }
        #endregion
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //

// ---------------------------------------- REFERENCES ---------------------------------------- //
// Maze, C. (2022) Quicksort algorithm in C#, Quicksort Algorithm in C#. Available at: https://code-maze.com/csharp-quicksort-algorithm/ (Accessed: 26 September 2023). 