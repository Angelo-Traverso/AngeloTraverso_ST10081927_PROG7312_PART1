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
    public partial class StartPlaying : Form
    {
        public StartPlaying()
        {
            InitializeComponent();
            this.BackColor = Color.Cyan;
            this.TransparencyKey = Color.Cyan;
        }

        private void StartPlaying_Click(object sender, EventArgs e)
        {
            IdentifyingAreascs identifyingAreascs = new IdentifyingAreascs();
            this.Hide();
        }
    }
}
