using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    internal class Book
    {
        public string CallingNumber { get; set; }
        public Panel BookPanel { get; set; }
        public Color BookColor { get; set; }

        public Book(string callingNumber, Panel bookPanel, Color bookColor)
        {
            CallingNumber = callingNumber;
            BookPanel = bookPanel;
            BookColor = bookColor;
        }


       /* public string ValidateCallingOrder(Book book, List<Book> bookList)
        {
            bool isValid;
            string message = string.Empty;

             
        }*/
    }
}
