using System.Drawing;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    /// <summary>
    ///      struct used to ensure immutability and prevent unintended changes to the struct's fields.
    /// </summary>
    public struct Book
    {
        public string CallingNumber { get; }
        public Panel BookPanel { get; }
        public Color BookColor { get; }

        public Book(string callingNumber, Panel bookPanel, Color bookColor)
        {
            CallingNumber = callingNumber;
            BookPanel = bookPanel;
            BookColor = bookColor;
        }
    }
}
