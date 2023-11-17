using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class FindCallNumbersGame : Form
    {
        public FindCallNumbersGame()
        {
            InitializeComponent();
            HandleWindowState();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Removes maximise and minimize boxes
        /// </summary>
        private void HandleWindowState()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void FindCallNumbersGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
