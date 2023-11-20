using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal_Latest.Forms
{
    public partial class FindingCallNumberGame : Form
    {
        public FindingCallNumberGame()
        {
            InitializeComponent();
            HandleWindowState();
        }

        private void HandleWindowState()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
    }
}
