using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    /// <summary>
    ///      struct used to ensure immutability and prevent unintended changes to the struct's fields.
    /// </summary>
    public struct Book
    {
        public string CallingNumber { get; set; }
        public Panel BookPanel { get; }
        public Color BookColor { get; }
        public bool IsPlaced { get; set; }

        public Book(string callingNumber, Panel bookPanel, Color bookColor)
        {
            CallingNumber = callingNumber;
            BookPanel = bookPanel;
            BookColor = bookColor;
            IsPlaced = false;
        }

        /// <summary>
        ///     Generate a random calling number
        /// </summary>
        /// <param name="bookIndex"></param>
        /// <returns></returns>
        public string GenerateRandomCallingNumber(int bookIndex, Random random)
        {
           

            double topicNumber = random.NextDouble() * 1000;
            string formattedTopicNumber = $"{topicNumber:000.00}";

            string authorAbbreviation = GenerateAuthorAbbreviation(bookIndex);

            return $"{formattedTopicNumber}\n{authorAbbreviation}";
        }

        /// <summary>
        ///     Method that generates the first the letters of each surname
        /// </summary>
        /// <param name="bookIndex"></param>
        /// <returns></returns>
        private string GenerateAuthorAbbreviation(int bookIndex)
        {
            string[] surnames = { "JAMES", "SMITH", "JOHNSON", "BROWN", "WILSON", "LEATHER", "TOAST", "SASHA", "BROKEY", "BLADE" };
            string surname = surnames[bookIndex % surnames.Length];

            return surname.Substring(0, Math.Min(3, surname.Length)).ToUpper();
        }
    }
}
