using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimal_Latest.Models
{
    public class AreaBook
    {
        public string CallNumber { get; set; }
        public string Description { get; set; }

        public AreaBook(string callNumber, string description)
        {
            CallNumber = callNumber;
            Description = description;
        }
    }
}
