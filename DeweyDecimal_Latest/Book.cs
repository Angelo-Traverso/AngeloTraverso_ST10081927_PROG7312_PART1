using System;
using System.Drawing;
using System.Linq;
using System.Text;
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

            string authorAbbreviation = GenerateAuthorAbbreviation(bookIndex, random);

            return $"{formattedTopicNumber}\n{authorAbbreviation}";
        }

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
    }
}
