using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public class BookPlacementHandler
    {
        /// <summary>
        ///     Method to validate whether a placeholder is occupied or not.
        ///     The method returns a boolean and the control which is occupying the space
        /// </summary>
        /// <param name="placeholder"></param>
        /// <param name="occupyingBook"></param>
        /// <returns></returns>
        public bool IsPlaceholderOccupied(Panel placeholder, List<Panel> bookList , out Control occupyingBook)
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
        ///     Calculates the distance from book to placeholder
        ///     Returns a dictionairy with the panel and its distance
        /// </summary>
        /// <param name="selectedBook"></param>
        /// <returns></returns>
        public Dictionary<Panel, double> CalculateDistancesToPlaceholders(Book selectedBook, List<Panel> placeHolderList)
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
        public Panel GetClosestPlaceholder(Dictionary<Panel, double> distanceToPlaceHolder)
        {
            var closestPair = distanceToPlaceHolder.OrderBy(pair => pair.Value).FirstOrDefault();

            return closestPair.Key;
        }

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
    }
}
