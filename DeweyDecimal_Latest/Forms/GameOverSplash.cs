using System;
using System.Drawing;
using System.Windows.Forms;

namespace DeweyDecimal_Latest.Forms
{

    public partial class GameOverSplash : Form
    {
        public bool PlayAgain { get; private set; }


        private Panel linePanelYes;

        private Panel linePanelNo;

        public GameOverSplash()
        {
            InitializeComponent();

            HandleWindowState();

            // Create a panel to represent the line
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

        }
        private void HandleWindowState()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
        private void lblYes_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            // Show the line when the mouse enters
            linePanelYes.Visible = true;
        }

        private void lblYes_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            // Hide the line when the mouse leaves
            linePanelYes.Visible = false;
        }

        private void lblNo_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            // Hide the line when the mouse leaves
            linePanelNo.Visible = true;
        }

        private void lblNo_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            // Hide the line when the mouse leaves
            linePanelNo.Visible = false;
        }

        private void lblYes_Click(object sender, EventArgs e)
        {
            PlayAgain = true;
            this.Close();
        }

        private void lblNo_Click(object sender, EventArgs e)
        {
            var menu = new StartMenu();
            menu.Show();
            this.Close();
            FindingCallNumberGame obj = (FindingCallNumberGame)Application.OpenForms["FindingCallNumberGame"];

            if (obj != null)
            {
                if (obj.InvokeRequired)
                {
                    obj.Invoke(new Action(() => obj.Close()));
                }
                else
                {
                    obj.Close();
                }
            }
        }
    }
}
