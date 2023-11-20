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
    public partial class WinForm : Form
    {
        public bool PlayAgain { get; private set; }

        public string lives { get; private set; }


        private Panel linePanelYes;

        private Panel linePanelNo;

        public WinForm(int lives)
        {
            InitializeComponent();

            HandleWindowState();

            linePanelYes = new Panel
            {
                Height = 2,
                Width = lblYes.Width,
                BackColor = Color.Black,
                Location = new Point(lblYes.Left, lblYes.Bottom),
                Visible = false
            };

            linePanelNo = new Panel
            {
                Height = 2,
                Width = lblNo.Width,
                BackColor = Color.Black,
                Location = new Point(lblNo.Left, lblNo.Bottom),
                Visible = false
            };


            // Add controls to the form
            Controls.Add(lblYes);
            Controls.Add(linePanelYes);
            Controls.Add(lblNo);
            Controls.Add(linePanelNo);

            lblLives.Text = "x " + lives;
        }
        private void HandleWindowState()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
        private void lblUserTime_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblYes_Click(object sender, EventArgs e)
        {
            PlayAgain = true;
            this.Close();
        }

        private void lblNo_Click(object sender, EventArgs e)
        {
            PlayAgain = false;

            // Continue with navigating to the main menu
            var menu = new StartMenu();
            menu.Show();
            this.Close();
            FindingCallNumberGame obj = (FindingCallNumberGame)Application.OpenForms["FindingCallNumberGame"];
            obj.Close();
        }

        private void lblYes_MouseEnter(object sender, EventArgs e)
        {
            linePanelYes.Visible = true;
            Cursor = Cursors.Hand;
        }

        private void lblYes_MouseLeave(object sender, EventArgs e)
        {
            linePanelYes.Visible = false;
            Cursor = Cursors.Default;
        }

        private void lblNo_MouseEnter(object sender, EventArgs e)
        {
            linePanelNo.Visible = true;
            Cursor = Cursors.Hand;
        }

        private void lblNo_MouseLeave(object sender, EventArgs e)
        {
            linePanelNo.Visible = false;
            Cursor = Cursors.Default;
        }
    }
}
