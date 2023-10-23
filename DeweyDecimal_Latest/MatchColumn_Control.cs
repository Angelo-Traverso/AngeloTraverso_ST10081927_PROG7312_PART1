using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using DeweyDecimal_Latest.Models;

namespace DeweyDecimal_Latest
{
    public partial class MatchColumn_Control : UserControl
    {
        private Panel selectedQuestion = null;
        private Panel selectedAnswer = null;

        private List<Panel> firstColumnPanels;
        private List<Panel> secondColumnPanels;
        private List<QuestionAnswerPair> questionAnswerPairs;
        private int questionsWithLinesDrawn = 0;
        private List<Line> lines = new List<Line>();
        /// <summary>
        ///  Holds the Calling number associated with each Description
        /// </summary>
        private Dictionary<string, string> AreaBookData;

        /// <summary>
        ///     Holds the first Column items to be displayed
        /// </summary>
        private List<string> firstColumnItems;

        /// <summary>
        ///     Holds the second Column items to be displayed
        /// </summary>
        private List<string> secondColumnItems;

        /// <summary>
        ///     Holds the correct answers
        /// </summary>
        private List<string> correctAnswers;

        /// <summary>
        ///     Holds scenario
        /// </summary>
        private bool matchDescriptionsToCallNumbers;

        private Dictionary<Panel, bool> questionLineDrawn = new Dictionary<Panel, bool>();
        private Dictionary<string, string> linkedQuestionsAnswers = new Dictionary<string, string>();
        public MatchColumn_Control()
        {
            InitializeComponent();

            // Initialize the questionAnswerPairs list with questions and correct answers

            questionAnswerPairs = new List<QuestionAnswerPair>
    {
        new QuestionAnswerPair("Computer Science, Information & general Works", "000"),
        new QuestionAnswerPair("Philosophy", "100"),
        new QuestionAnswerPair("Religion", "200"),
        new QuestionAnswerPair("Social Sciences", "300"),
        new QuestionAnswerPair("Language", "400"),
        new QuestionAnswerPair("Science", "500"),
        new QuestionAnswerPair("Technology", "600"),
        new QuestionAnswerPair("Art & Recreation", "700"),
        new QuestionAnswerPair("Literature", "800"),
        new QuestionAnswerPair("History & geography", "900")
    };


            firstColumnPanels = new List<Panel>
    {
        pnlFColumn1,
        pnlFColumn2,
        pnlFColumn3,
        pnlFColumn4
    };

            secondColumnPanels = new List<Panel>
    {
        pnlSColumn1,
        pnlSColumn2,
        pnlSColumn3,
        pnlSColumn4,
        pnlSColumn5,
        pnlSColumn6,
        pnlSColumn7
    };
            AttachMouseClickEventHandlers(firstColumnPanels, pnlFColumn_MouseClick);
            AttachMouseClickEventHandlers(secondColumnPanels, pnlSColumn_MouseClick);


            foreach (var panel in firstColumnPanels)
            {
                questionLineDrawn[panel] = false;
            }

            /* firstColumnItems = new List<string>();

             for (int i = 0; i < firstColumnItems.Count; i++)
             {
                 var question = firstColumnItems[i];
                 var answer = secondColumnItems[i];
                 var pair = new QuestionAnswerPair(question, answer);
                 questionAnswerPairs.Add(pair);
             }*/
            InitAreaBookData(); // Initialize AreaBookData
            InitData();
        }

        private void InitData()
        {
            InitColumnItems();
        }

        /// <summary>
        ///     Initializes dictionairy storing the Dewey Decimal Calling Number and description
        /// </summary>
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
            // Define the items that should be displayed in the first column
            List<string> itemsForFirstColumn;

            // Randomly choose the matching scenario
            matchDescriptionsToCallNumbers = new Random().Next(2) == 0; // 0 or 1

            if (matchDescriptionsToCallNumbers)
            {
                // Matching descriptions to call numbers
                itemsForFirstColumn = AreaBookData.Values.ToList();
            }
            else
            {
                // Matching call numbers to descriptions
                itemsForFirstColumn = AreaBookData.Keys.ToList();
            }

            // Shuffle the list of items for the first column
            itemsForFirstColumn = itemsForFirstColumn.OrderBy(x => Guid.NewGuid()).ToList();

            // Initialize the first column with 4 random items
            firstColumnItems = itemsForFirstColumn.Take(4).ToList();

            // Initialize the second column with 7 possible answers (4 correct, 3 incorrect)
            secondColumnItems = new List<string>();
            correctAnswers = new List<string>();
            linkedQuestionsAnswers.Clear(); // Clear the dictionary

            foreach (var item in firstColumnItems)
            {
                if (matchDescriptionsToCallNumbers)
                {
                    // Use descriptions as questions
                    string callNumber = AreaBookData.First(kv => kv.Value == item).Key;
                    secondColumnItems.Add(callNumber);

                    // Store the correct answer (description)
                    correctAnswers.Add(item);
                }
                else
                {
                    // Use call numbers as questions
                    string description = AreaBookData[item];
                    secondColumnItems.Add(description);

                    // Store the correct answer (description)
                    correctAnswers.Add(description);
                }

                // Add to the linked dictionary
                linkedQuestionsAnswers.Add(item, string.Empty);
            }

            // Add 3 incorrect items (exclude those used for correct answers)
            var incorrectItems = matchDescriptionsToCallNumbers
                ? AreaBookData.Keys.Except(secondColumnItems).OrderBy(x => Guid.NewGuid()).Take(3)
                : AreaBookData.Values.Except(secondColumnItems).OrderBy(x => Guid.NewGuid()).Take(3);

            secondColumnItems.AddRange(incorrectItems);

            // Shuffle the second column items
            secondColumnItems = secondColumnItems.OrderBy(x => Guid.NewGuid()).ToList();
        }


        private void CreateLabelsForColumn(List<string> items, List<Panel> columns)
        {
            for (int i = 0; i < items.Count && i < columns.Count; i++)
            {
                // Create labels within the specified column panel
                Label label = new Label
                {
                    Text = items[i],
                    AutoSize = true,
                    ForeColor = Color.Black,
                    Enabled = false,
                    Dock = DockStyle.Top // Stacked vertically within the panel
                };
                columns[i].Tag = label.Text;
                columns[i].Controls.Add(label);
            }
        }
        private void ClearColumnPanels(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Panel)
                {
                    ClearColumnPanels((Panel)control); // Recursively clear nested panels
                }
                else
                {
                    panel.Controls.Remove(control);
                    control.Dispose(); // Dispose of the label control
                }
            }
        }
        private void ClearLabelsFromColumnPanels(List<Panel> columns)
        {
            foreach (var column in columns)
            {
                foreach (Control control in column.Controls)
                {
                    if (control is Label)
                    {
                        column.Controls.Remove(control);
                        control.Dispose(); // Dispose of the label control
                    }
                }
            }
        }
        private void MatchColumn_Control_Load(object sender, EventArgs e)
        {
            InitAreaBookData();
            InitColumnItems();


            CreateLabelsForColumn(firstColumnItems, new List<Panel> { pnlFColumn1, pnlFColumn2, pnlFColumn3, pnlFColumn4 });

            // Create labels for the second column panels
            CreateLabelsForColumn(secondColumnItems, new List<Panel> { pnlSColumn1, pnlSColumn2, pnlSColumn3, pnlSColumn4, pnlSColumn5, pnlSColumn6, pnlSColumn7 });
            AttachMouseClickEventHandlers(new List<Panel> { pnlFColumn1, pnlFColumn2, pnlFColumn3, pnlFColumn4 }, pnlFColumn_MouseClick);
            AttachMouseClickEventHandlers(new List<Panel> { pnlSColumn1, pnlSColumn2, pnlSColumn3, pnlSColumn4, pnlSColumn5, pnlSColumn6, pnlSColumn7 }, pnlSColumn_MouseClick);
        }
        private void AttachMouseClickEventHandlers(List<Panel> panels, MouseEventHandler handler)
        {
            foreach (var panel in panels)
            {
                if (panel != null)
                {
                    panel.MouseClick += handler;
                }
            }
        }

        private void Reset()
        {

            // Reinitialize the column items
            questionsWithLinesDrawn = 0;
            InitColumnItems();

            questionLineDrawn.Clear();
            linkedQuestionsAnswers.Clear();

            // Clear labels from all panels in the first column
            ClearLabelsFromColumnPanels(new List<Panel> { pnlFColumn1, pnlFColumn2, pnlFColumn3, pnlFColumn4 });

            // Clear labels from all panels in the second column
            ClearLabelsFromColumnPanels(new List<Panel> { pnlSColumn1, pnlSColumn2, pnlSColumn3, pnlSColumn4, pnlSColumn5, pnlSColumn6, pnlSColumn7 });

            // Recreate labels for the first and second column panels
            CreateLabelsForColumn(firstColumnItems, new List<Panel> { pnlFColumn1, pnlFColumn2, pnlFColumn3, pnlFColumn4 });
            CreateLabelsForColumn(secondColumnItems, new List<Panel> { pnlSColumn1, pnlSColumn2, pnlSColumn3, pnlSColumn4, pnlSColumn5, pnlSColumn6, pnlSColumn7 });
        }

        private void EnablePanels()
        {
            foreach (var panel in firstColumnPanels)
            {
                panel.Enabled = true;
            }

            foreach (var panel in secondColumnPanels)
            {
                panel.Enabled = true;
            }
        }

        private void ClearAllLines()
        {
            lines.Clear();
            pnlDraw.Invalidate(); // Trigger a repaint to remove the lines from the control
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearAllLines(); // Clear all lines
            Reset();
            EnablePanels();
        }

        private void pnlFColumn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel clickedPanel = sender as Panel;

                if (clickedPanel != null)
                {
                    if (selectedQuestion != null)
                    {
                        selectedQuestion.BackColor = Color.DimGray;
                    }

                    selectedQuestion = clickedPanel;
                    selectedQuestion.BackColor = Color.Green; // Set the selected question's color
                    ApplyGlowEffectToAnswers();
                }
            }
        }

        private void pnlSColumn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Check if an answer is selected
                Panel clickedPanel = sender as Panel;

                if (clickedPanel != null)
                {
                    // If an answer is already selected, unselect it
                    selectedAnswer = clickedPanel;

                    // Check if both question and answer are selected
                    if (selectedQuestion != null && selectedAnswer != null)
                    {
                        // Draw a line from the selected question to the selected answer
                        DrawLine(selectedQuestion, selectedAnswer);

                        ClearGlowEffectFromAnswers();

                        

                        // Update the dictionary to link the question to the answer
                        // For example, you can use the question's text as the key and the answer's text as the value
                        linkedQuestionsAnswers[selectedQuestion.Tag.ToString()] = selectedAnswer.Tag.ToString();

                        // Mark this question as having a line drawn
                        questionLineDrawn[selectedQuestion] = true;

                        // Disable both question and answer panels
                        selectedQuestion.Enabled = false;
                        selectedAnswer.Enabled = false;

                        // Unselect both question and answer
                        selectedAnswer.BackColor = Color.DimGray;
                        selectedQuestion.BackColor = Color.DimGray;
                        selectedQuestion = null;
                        selectedAnswer = null;

                        // questionsWithLinesDrawn++;
                        if (questionsWithLinesDrawn == 4)
                        {
                            CheckAnswers();
                            return;
                        }
                    }
                }
            }
        }
        private void CheckAnswers()
        {
            int correctCount = 0;
            int totalQuestions = firstColumnItems.Count;

            for (int i = 0; i < firstColumnItems.Count; i++)
            {
                string questionText = firstColumnItems[i];
                string selectedAnswerText = linkedQuestionsAnswers[questionText];
                string correctAnswerText;

                if (matchDescriptionsToCallNumbers)
                {
                    var questionAnswerPair = questionAnswerPairs.Find(pair => pair.Question == questionText);
                    correctAnswerText = (questionAnswerPair != null) ? questionAnswerPair.CorrectAnswer : null;
                }
                else
                {
                    var questionAnswerPair = questionAnswerPairs.Find(pair => pair.CorrectAnswer == questionText);
                    correctAnswerText = (questionAnswerPair != null) ? questionAnswerPair.Question : null;
                }

                if (correctAnswerText != null && selectedAnswerText == correctAnswerText)
                {
                    correctCount++;
                }
            }

            string message = $"You answered {correctCount} out of {totalQuestions} questions correctly.";
            MessageBox.Show(message, "Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }







        private void DrawLine(Panel startPanel, Panel endPanel)
        {
            using (Pen pen = new Pen(Color.Firebrick, 4))
            {
                // Smoothing the line drawn
                pnlDraw.CreateGraphics().SmoothingMode = SmoothingMode.AntiAlias;

                Point startPoint = new Point(startPanel.Right, startPanel.Top + startPanel.Height / 2);
                Point endPoint = new Point(endPanel.Left, endPanel.Top + endPanel.Height / 2);

                // Draw the line
                pnlDraw.CreateGraphics().DrawLine(pen, startPoint, endPoint);

                // Increment the counter
                questionsWithLinesDrawn++;

                // Store the line
                lines.Add(new Line(startPoint, endPoint));
            }
        }

        private void ApplyGlowEffectToAnswers()
        {
            foreach (var panel in secondColumnPanels)
            {
                // Apply the glow effect (e.g., change background color to a lighter color)
                panel.BackColor = Color.LightBlue;
            }
        }
        private void ClearGlowEffectFromAnswers()
        {
            foreach (var panel in secondColumnPanels)
            {
                // Clear the glow effect (e.g., reset the background color)
                panel.BackColor = Color.DimGray;
            }
        }

    }
}
