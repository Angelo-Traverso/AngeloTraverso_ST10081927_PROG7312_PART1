/*
 *  Full Name: Angelo Traverso
 *  Student Number: ST10081927
 *  Subject: Programming 3B
 *  Code: PROG7312
 */

// ------------ Using ------------ //

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using DeweyDecimal_Latest.Models;
using TimerLib;


// ------------ End Using ------------ //

namespace DeweyDecimal_Latest
{
    public partial class MatchColumn_Control : UserControl
    {
        // --------------------------- Declarations --------------------------- //
        /// <summary>
        ///  Instance of PanelHelper class
        ///  Class holds helper methods for panels in this game
        /// </summary>
        PanelHelper panelHelper = new PanelHelper();

        /// <summary>
        ///     Instance of Sorting Game, in order to re-use some methods
        /// </summary>
        SortingGame sortingGame = new SortingGame();
        /// <summary>
        ///     List to hold all labels created
        /// </summary>
        private List<Label> panelLabels = new List<Label>();
        /// <summary>
        ///     Holds selected question panel
        /// </summary>
        private Panel selectedQuestion = null;

        /// <summary>
        ///     Holds selected answer panel
        /// </summary>
        private Panel selectedAnswer = null;

        /// <summary>
        ///     Instance of deweyTimer Library
        /// </summary>
        private Class1 deweyTimer;

        /// <summary>
        ///     Holds the fastest time the user has completed the game successfully
        /// </summary>
        private TimeSpan fastestTime = new TimeSpan(0, 0, 35);
     
        /// <summary>
        ///     Holds the number of answers made by the user
        /// </summary>
        int answerCounter = 0;

        /// <summary>
        ///     Holds all of the first column panels
        /// </summary>
        private List<Panel> firstColumnPanels;

        /// <summary>
        ///     Holds all of the second column panels
        /// </summary>
        private List<Panel> secondColumnPanels;

        /// <summary>
        ///     Holds QuestionAnswerPairs that user has chosen
        /// </summary>
        private List<QuestionAnswerPair> questionAnswerPairs;

        /// <summary>
        ///     Holds the number of question with a line drawn
        /// </summary>
        private int questionsWithLinesDrawn = 0;

        /// <summary>
        ///     Holds the lines drawn
        /// </summary>
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
        ///     Keeps track of the elapsed time for th eusers' game
        /// </summary>
        private int elapsedTimeInSeconds = 0;

        /// <summary>
        ///     Holds the correct answers
        /// </summary>
        private List<string> correctAnswers;

        /// <summary>
        ///     Holds scenario
        /// </summary>
        private bool matchDescriptionsToCallNumbers;
        
        /// <summary>
        ///     Holds the panels with lines drawn from them
        /// </summary>
        private Dictionary<Panel, bool> questionLineDrawn = new Dictionary<Panel, bool>();

        /// <summary>
        ///     Holds questions and their related answers
        /// </summary>
        private Dictionary<string, string> linkedQuestionsAnswers = new Dictionary<string, string>();

        // --------------------------- End Declarations --------------------------- //

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Default Constructor
        /// </summary>
        public MatchColumn_Control()
        {
            InitializeComponent();

            lblFastestTime.Text = "Personal Best: " + fastestTime;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Starts a new, fresh game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            btnNewGame.Visible = false;
            btnNewGame.Enabled = false;
            ClearAllLines();
            Reset();
            panelHelper.EnablePanels(firstColumnPanels, secondColumnPanels);
        }

        /// <summary>
        ///     All respective functions are called to start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;

            SetAnswerPanelColor();

            InitializeTimer();

            InitializeQuesitonAnswerPairs();

            panelHelper.AttachMouseClickEventHandlers(firstColumnPanels, pnlFColumn_MouseClick);
            panelHelper.AttachMouseClickEventHandlers(secondColumnPanels, pnlSColumn_MouseClick);

            SetLineDrawnState();

            InitAreaBookData();

            InitColumnItems();

            InitAreaBookData();

            InitColumnItems();

            panelHelper.ResetQuestionPanelColor(firstColumnPanels);

            panelHelper.CreateLabelsForColumn(firstColumnItems, new List<Panel> { pnlFColumn1, pnlFColumn2, pnlFColumn3, pnlFColumn4 }, panelLabels);
            panelHelper.CreateLabelsForColumn(secondColumnItems, new List<Panel> { pnlSColumn1, pnlSColumn2, pnlSColumn3, pnlSColumn4, pnlSColumn5, pnlSColumn6, pnlSColumn7 }, panelLabels);
        }

        /// <summary>
        ///     When user decides to exit, an appropriate message is given
        ///     So that the user is can make sure they want to leave and lose progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            var response = MessageBox.Show("You are about to leave your game.\nAll progress will be lost. Are you sure you want to leave?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (response == DialogResult.No)
            {
                // Early return to remove the need for redundant itterations
                return;
            }
            else
            {
                var parent = this.FindForm();
                var menu = new StartMenu();
                menu.Show();
                parent.Close();
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Resets game state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearAllLines();
            Reset();
            panelHelper.EnablePanels(firstColumnPanels, secondColumnPanels);
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     First panel mouse click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlFColumn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel clickedPanel = sender as Panel;

                if (clickedPanel != null)
                {
                    ResetSelectedQuestionBackground();

                    SetSelectedQuestion(clickedPanel);

                    panelHelper.ReadyToSelectEffect(secondColumnPanels);

                    SetLabelsInSecondColumnEnabledAndColor();
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Second panel mouse click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlSColumn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                // Early return to remove redundant iterations
                return;
            }

            Panel clickedPanel = sender as Panel;

            if (clickedPanel == null)
            {
                return;
            }

            selectedAnswer = clickedPanel;

            if (selectedQuestion == null || selectedAnswer == null)
            {
                return;
            }

            // Draw a line from the selected question to the selected answer
            DrawLine(selectedQuestion, selectedAnswer, pnlDraw);

            panelHelper.ClearGlowEffectFromAnswers(secondColumnPanels);

            _ = sortingGame.PlaySound("Wink.mp3");

            // Updating the dictionary to link the question to the answer
            linkedQuestionsAnswers[selectedQuestion.Tag.ToString()] = selectedAnswer.Tag.ToString();

            // Mark this question as having a line drawn
            questionLineDrawn[selectedQuestion] = true;

            // Disable both question and answer panels
            DisableQuestionAndAnswer();

            // Unselect both question and answer
            panelHelper.SetQuestionBackground(selectedQuestion);

            selectedQuestion = null;
            selectedAnswer = null;

            answerCounter++;

            DisplayAnswersGiven();

            AllAnswersComplete();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Sets all panels line drawn to false on start
        /// </summary>
        private void SetLineDrawnState()
        {
            foreach (var panel in firstColumnPanels)
            {
                questionLineDrawn[panel] = false;
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///    Initialize the questionAnswerPairs list with questions and correct answers
        /// </summary>
        private void InitializeQuesitonAnswerPairs()
        {
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
        }
        
        // ----------------------------------------------------------------------------------------------------------- //
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

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Updates UI Timer
        /// </summary>
        private void UpdateUITimer()
        {
            if (lblTimer.InvokeRequired)
            {
                // We're on a different thread, so invoke the method to update the UI
                lblTimer.Invoke(new Action(UpdateUITimer));
            }
            else
            {
                // We're on the UI thread, so update the lblTimer control directly
                TimeSpan elapsedTime = TimeSpan.FromSeconds(elapsedTimeInSeconds);
                lblTimer.Text = elapsedTime.ToString(@"hh\:mm\:ss");
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Initializing game timer
        ///     Attatching Tick event as well
        /// </summary>
        private void InitializeTimer()
        {
            deweyTimer = new Class1(1000);
            deweyTimer.Elapsed += matchColumnTimer_Tick;
            deweyTimer.Start();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Initializes the colum items on start or reset
        /// </summary>
        private void InitColumnItems()
        {
            // Defines items for first quesiton column
            List<string> itemsForFirstColumn;

            // Randomly chooses the matching game scenario - either call numbers as questions or descriptions
            matchDescriptionsToCallNumbers = new Random().Next(2) == 0;

            if (matchDescriptionsToCallNumbers)
            {
                // Descriptions to call numbers
                itemsForFirstColumn = AreaBookData.Values.ToList();
            }
            else
            {
                // Call numbers to descriptions
                itemsForFirstColumn = AreaBookData.Keys.ToList();
            }

            // Shuffles the list of items for the first column
            itemsForFirstColumn = itemsForFirstColumn.OrderBy(x => Guid.NewGuid()).ToList();

            // Initialize the first column with 4 random items
            firstColumnItems = itemsForFirstColumn.Take(4).ToList();

            // Initializing the second column with 7 possible answers (4 correct, 3 incorrect)
            secondColumnItems = new List<string>();

            // Defines the correct answers
            correctAnswers = new List<string>();
            
            linkedQuestionsAnswers.Clear();

            foreach (var item in firstColumnItems)
            {
                if (matchDescriptionsToCallNumbers)
                {
                    string callNumber = AreaBookData.First(kv => kv.Value == item).Key;
                    secondColumnItems.Add(callNumber);

                    // Storing the correct answer
                    correctAnswers.Add(item);
                }
                else
                {
                    // Using call numbers as questions
                    string description = AreaBookData[item];
                    secondColumnItems.Add(description);

                    // Storing correct answer
                    correctAnswers.Add(description);
                }

                // Add to the linked dictionary
                linkedQuestionsAnswers.Add(item, string.Empty);
            }

            // Adding 3 incorrect items
            var incorrectItems = matchDescriptionsToCallNumbers
                ? AreaBookData.Keys.Except(secondColumnItems).OrderBy(x => Guid.NewGuid()).Take(3)
                : AreaBookData.Values.Except(secondColumnItems).OrderBy(x => Guid.NewGuid()).Take(3);

            secondColumnItems.AddRange(incorrectItems);

            // Shuffle the second column items
            secondColumnItems = secondColumnItems.OrderBy(x => Guid.NewGuid()).ToList();
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Customizes the drawing behaviour of the control, drawing lines between panels
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Calling the base class OnPaint method
            base.OnPaint(e);

            // Creating a Pen for drawing lines
            using (Pen pen = new Pen(Color.Firebrick, 4))
            {
                // Setting anti-aliasing mode for smoother lines
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                foreach (var line in lines)
                {
                    // Iterating through the list of lines and draw each line using the specified pen
                    e.Graphics.DrawLine(pen, line.StartPoint, line.EndPoint);
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Resets game to original state
        /// </summary>
        private void Reset()
        {
            answerCounter = 0;

            // Timer
            elapsedTimeInSeconds = 0;
            lblTimer.Text = "00:00:00";
            deweyTimer.Stop();

            //Lines
            questionsWithLinesDrawn = 0;

            // Columns
            InitColumnItems();

            // Colors
            panelHelper.ResetQuestionPanelColor(firstColumnPanels);
            panelHelper.ResetAnswerPanelColor(secondColumnPanels);

            // Lists
            questionLineDrawn.Clear();
            linkedQuestionsAnswers.Clear();
            panelHelper.ClearLabelsFromColumnPanels(new List<Panel> { pnlFColumn1, pnlFColumn2, pnlFColumn3, pnlFColumn4 });
            panelHelper.ClearLabelsFromColumnPanels(new List<Panel> { pnlSColumn1, pnlSColumn2, pnlSColumn3, pnlSColumn4, pnlSColumn5, pnlSColumn6, pnlSColumn7 });

            // Labels
            panelHelper.CreateLabelsForColumn(firstColumnItems, new List<Panel> { pnlFColumn1, pnlFColumn2, pnlFColumn3, pnlFColumn4 }, panelLabels);
            panelHelper.CreateLabelsForColumn(secondColumnItems, new List<Panel> { pnlSColumn1, pnlSColumn2, pnlSColumn3, pnlSColumn4, pnlSColumn5, pnlSColumn6, pnlSColumn7 }, panelLabels);

            InitializeTimer();
        }

       
        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Sets the answer panel colors
        /// </summary>
        private void SetAnswerPanelColor()
        {
            for (int i = 1; i <= 7; i++)
            {
                string pnlName = "pnlFColumn" + i.ToString();

                // Finding the panel by its name
                Panel panel = Controls.Find(pnlName, true).FirstOrDefault() as Panel;

                if (panel != null)
                {
                    panel.BackColor = ColorTranslator.FromHtml("#ff943f");
                }
            }

        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Clears all lines drawn
        /// </summary>
        private void ClearAllLines()
        {
            lines.Clear();
            pnlDraw.Invalidate(); // Trigger a repaint to remove the lines from the control
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Resets the selected questions color
        /// </summary>
        private void ResetSelectedQuestionBackground()
        {
            if (selectedQuestion != null)
            {
                selectedQuestion.BackColor = ColorTranslator.FromHtml("#3fa9ff");
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Sets the selected question color
        /// </summary>
        /// <param name="clickedPanel"></param>
        private void SetSelectedQuestion(Panel clickedPanel)
        {
            selectedQuestion = clickedPanel;
            selectedQuestion.BackColor = ColorTranslator.FromHtml("#90cdff");
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///  Sets answer panels color when question is clicked
        /// </summary>
        private void SetLabelsInSecondColumnEnabledAndColor()
        {
            foreach (var panel in secondColumnPanels)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is Label label)
                    {
                        //label.Enabled = true;
                        label.ForeColor = Color.White;
                    }
                }
            }
        }
       
        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Draws line from question panel to answer panel
        /// </summary>
        /// <param name="startPanel"></param>
        /// <param name="endPanel"></param>
        public void DrawLine(Panel startPanel, Panel endPanel, Panel pnlDraw)
        {
            using (Graphics g = pnlDraw.CreateGraphics())
            using (LinearGradientBrush brush = new LinearGradientBrush(startPanel.Location, endPanel.Location, Color.White, Color.Black))
            using (Pen pen = new Pen(Color.White, 4))
            {
                // Smoothing the line drawn
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Point startPoint = new Point(startPanel.Right, startPanel.Top + startPanel.Height / 2);
                Point endPoint = new Point(endPanel.Left, endPanel.Top + endPanel.Height / 2);

                // Draw the line
                g.DrawLine(pen, startPoint, endPoint);

                // Calculating arrowhead points
                float arrowSize = 20;
                float angle = (float)Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X);
                PointF arrow1 = new PointF(endPoint.X - arrowSize * (float)Math.Cos(angle - Math.PI / 6), endPoint.Y - arrowSize * (float)Math.Sin(angle - Math.PI / 6));
                PointF arrow2 = new PointF(endPoint.X - arrowSize * (float)Math.Cos(angle + Math.PI / 6), endPoint.Y - arrowSize * (float)Math.Sin(angle + Math.PI / 6));

                // Draw the arrowhead
                g.DrawLine(pen, endPoint, arrow1);
                g.DrawLine(pen, endPoint, arrow2);

                // Increment the counter
                this.questionsWithLinesDrawn++;

                // Storing the line
                lines.Add(new Line(startPoint, endPoint));
            }
        }
        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Check measure to see if user has answered all questions
        /// </summary>
        private void AllAnswersComplete() 
        {
            if (questionsWithLinesDrawn == 4)
            {
                CheckAnswers();
                btnNewGame.Enabled = true;
                btnNewGame.Visible = true;
                return;
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Displays the number of answers completed by the user
        /// </summary>
        private void DisplayAnswersGiven()
        {
            lblAnswerCounter.Text = answerCounter + "/4";
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Disables the question and answer panels after they are chosen
        /// </summary>
        private void DisableQuestionAndAnswer()
        {
            selectedQuestion.Enabled = false;
            selectedAnswer.Enabled = false;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Validates users' answers
        /// </summary>
        private void CheckAnswers()
        {
            var correctCount = 0;
            var totalQuestions = firstColumnItems.Count;
            
            deweyTimer.Stop();
            

            for (int i = 0; i < firstColumnItems.Count; i++)
            {
                var questionText = firstColumnItems[i];
                var selectedAnswerText = linkedQuestionsAnswers[questionText];
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


            SetPersonalBest(correctCount);

            DisplayResultsMessage(correctCount, totalQuestions);
            
            deweyTimer.Stop();

            lblAnswerCounter.Text = "/4";
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Sets the users' personal best score only if they have successfully managed to get all questions correct
        /// </summary>
        /// <param name="correctCount"></param>
        private void SetPersonalBest(int correctCount)
        {
            if ((correctCount == 4) && (TimeSpan.FromSeconds(elapsedTimeInSeconds) < fastestTime))
            {
                fastestTime = TimeSpan.FromSeconds(elapsedTimeInSeconds);
            }

            lblFastestTime.Text = "Personal Best: " + fastestTime.ToString();

        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Displays an appropriate results message after completion
        /// </summary>
        /// <param name="corrCount"></param>
        /// <param name="totalQuestions"></param>
        private void DisplayResultsMessage(int corrCount, int totalQuestions)
        {
           var message = $"You answered {corrCount} out of {totalQuestions} questions correctly.";
            MessageBox.Show(message, "Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Executes on every timer tick execution
        ///     It updates the lblTimer to display the current time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void matchColumnTimer_Tick(object sender, EventArgs e)
        {
            elapsedTimeInSeconds++;

            if (!IsHandleCreated)
            {
                // Stoping the timer once the form is closed
                matchColumnTimer.Stop();

                // Early return to remove redundant itterations
                return;
            }

            if (!lblTimer.IsDisposed)
            {
                if (lblTimer.InvokeRequired)
                {
                    lblTimer.Invoke(new Action(() => lblTimer.Text = TimeSpan.FromSeconds(elapsedTimeInSeconds).ToString()));
                }
                else
                {
                    lblTimer.Text = TimeSpan.FromSeconds(elapsedTimeInSeconds).ToString();
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     On Mouse Over event for new game button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGame_MouseHover(object sender, EventArgs e)
        {
            panelHelper.CreateToolTip(btnNewGame, "New Game");
        }
    }
}// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //
