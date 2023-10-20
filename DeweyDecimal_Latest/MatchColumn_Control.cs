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
    public partial class MatchColumn_Control : UserControl
    {
        private Dictionary<string, string> AreaBookData;
        private List<string> firstColumnItems;
        private List<string> secondColumnItems;

        private List<string> correctAnswers;

        private bool matchDescriptionsToCallNumbers; // Randomly choose the scenario

        // Variables for drawing lines
        private Point? startPoint = null;
        private Point? endPoint = null;
        public MatchColumn_Control()
        {
            InitializeComponent();

            this.Paint += pnlDraw_Paint;
        }

        private void InitAreaBookData()
        {
            AreaBookData = new Dictionary<string, string>
            {
                { "000", "Computer Science, Information & general Works" },
                { "100", "Philosophy" },
                { "200", "Religion" },
                { "300", "Social Sciences" },
                { "400", "Language" },
                { "500", "Science" },
                { "600", "Technology" },
                { "700", "Art & Recreation" },
                { "800", "Literature" },
                { "900", "History & geography" }
            };
        }

        private void InitColumnItems()
        {
            // Randomly choose the matching scenario
            matchDescriptionsToCallNumbers = new Random().Next(2) == 0; // 0 or 1

            // Choose the source for the first column based on the scenario
            List<string> sourceItems = matchDescriptionsToCallNumbers
                ? AreaBookData.Values.ToList()
                : AreaBookData.Keys.ToList();

            // Initialize the first column with 4 random items
            firstColumnItems = sourceItems.OrderBy(x => Guid.NewGuid()).Take(4).ToList();

            // Initialize the second column with 7 possible answers (4 correct, 3 incorrect)
            secondColumnItems = new List<string>();
            correctAnswers = new List<string>();

            if (matchDescriptionsToCallNumbers)
            {
                // Matching descriptions to call numbers
                foreach (var description in firstColumnItems)
                {
                    var callNumber = AreaBookData.First(kv => kv.Value == description).Key;
                    secondColumnItems.Add(callNumber);
                    correctAnswers.Add(description); // Store the correct answer
                }
                // Add 3 incorrect call numbers
                var incorrectCallNumbers = AreaBookData.Keys.Except(secondColumnItems).OrderBy(x => Guid.NewGuid()).Take(3);
                secondColumnItems.AddRange(incorrectCallNumbers);
            }
            else
            {
                // Matching call numbers to descriptions
                foreach (var callNumber in firstColumnItems)
                {
                    var description = AreaBookData[callNumber];
                    secondColumnItems.Add(description);
                    correctAnswers.Add(callNumber); // Store the correct answer
                }
                // Add 3 incorrect descriptions
                var incorrectDescriptions = AreaBookData.Values.Except(secondColumnItems).OrderBy(x => Guid.NewGuid()).Take(3);
                secondColumnItems.AddRange(incorrectDescriptions);
            }

            // Shuffle the second column items
            secondColumnItems = secondColumnItems.OrderBy(x => Guid.NewGuid()).ToList();
        }
    

        private void DisplayRandomQuestion()
        {
            var random = new Random();

            // Clear both columns
            pnlFirstColumn.Controls.Clear();
            pnlSecondColumn.Controls.Clear();

            // Display the random items in the first column
            CreateLabels(firstColumnItems, pnlFirstColumn);

            if (string.IsNullOrWhiteSpace(lblDescription.Text))
            {
                // Handle the scenario where the description is empty
                lblDescription.Text = "Select the correct answer below";
            }

            // Display the second column items
            UpdatePanelItems(pnlSecondColumn, secondColumnItems);

            if (matchDescriptionsToCallNumbers)
            {
                // Matching descriptions to call numbers
                int correctAnswerIndex = firstColumnItems.IndexOf(lblDescription.Text);
                if (correctAnswerIndex >= 0)
                {
                    Label label = (Label)pnlFirstColumn.Controls[correctAnswerIndex];
                    label.Font = new Font(label.Font, FontStyle.Bold);
                }
            }
            else
            {
                // Matching call numbers to descriptions
                int correctAnswerIndex = secondColumnItems.IndexOf(lblDescription.Text);
                if (correctAnswerIndex >= 0)
                {
                    Label label = (Label)pnlSecondColumn.Controls[correctAnswerIndex];
                    label.Font = new Font(label.Font, FontStyle.Bold);
                }
            }
        }

        private void UpdatePanelItems(Panel panel, List<string> items)
        {
            panel.Controls.Clear();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                CreateLabel(items[i], panel);
            }
        }

        private void CreateLabel(string text, Panel panel)
        {
           
                Label label = new Label
                {
                    Text = text,
                    AutoSize = true,
                    Dock = DockStyle.Top // Stacked vertically within the panel
                };

                panel.Controls.Add(label);
            
        }

        private void CreateLabels(List<string> items, Panel panel)
        {
            panel.Controls.Clear();
            foreach (var item in items)
            {
                Label label = new Label
                {
                    Text = item,
                    AutoSize = true,
                    Dock = DockStyle.Top // Stacked vertically within the panel
                };

                panel.Controls.Add(label);
            }
        }

        private void MatchColumn_Control_Load(object sender, EventArgs e)
        {
            InitAreaBookData();
            InitColumnItems();
            DisplayRandomQuestion();
        }

        private void btnNextQuestion_Click(object sender, EventArgs e)
        {
            DisplayRandomQuestion();
        }

        private void Reset()
        {
            // Reinitialize the column items
            InitColumnItems();

            // Clear both columns
            pnlFirstColumn.Controls.Clear();
            pnlSecondColumn.Controls.Clear();

            // Clear the description label
            lblDescription.Text = string.Empty;

            // Display a new random question
            DisplayRandomQuestion();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }


        private void pnlDraw_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                endPoint = e.Location;
            }
        }

        private void pnlDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && startPoint.HasValue)
            {
                endPoint = e.Location;
                this.Invalidate(); // Refresh the control to trigger the Paint event
            }
        }

        private void pnlDraw_MouseUp(object sender, MouseEventArgs e)
        {
           /* if (e.Button == MouseButtons.Left)
            {*/
                endPoint = e.Location;

                // Optionally, store or process the line's start and end points
                // For example, you can save these points to a list or perform other actions.
           // }
        }

        private void pnlDraw_Paint(object sender, PaintEventArgs e)
        {
            if (startPoint.HasValue && endPoint.HasValue)
            {
                using (Pen pen = new Pen(Color.Black, 4))
                {
                    e.Graphics.DrawLine(pen, startPoint.Value, endPoint.Value);
                }
            }
        }
    }
}
