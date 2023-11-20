/*
 * Student Name: Angelo Traverso
 * Student Number: ST10081927
 * Project: Programming 3B POE
 * Project Title: Finding Call Numbers
 */

// ----------------- Usings ----------------- //
using DeweyDecimal_Latest.Forms;
using DeweyDecimal_Latest.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TressSampleApplication.Classes;
using static TressSampleApplication.Classes.RedBlackTree;
// ----------------- End Usings ----------------- //

namespace DeweyDecimal_Latest
{
    public class FileWorker
    {
        #region Declarations

        /// <summary>
        ///     Event handler to notify control that a life has been lost,and a heart should be removed
        /// </summary>
        public event EventHandler LifeLost;

        /// <summary>
        ///     Event handler to notify the control that the game has been reset in order to reset timer and other UI components
        /// </summary>
        public event EventHandler gameReset;

        /// <summary>
        ///     Event handler to notify the control that the game has been fully completed
        ///     This is done to updated the users personal best score
        /// </summary>
        public event EventHandler GameCompleted;

        /// <summary>
        ///     Event Handler to notify the control that the user wants to leave the game and return to the menu
        /// </summary>
        public event EventHandler exitToMenu;

        /// <summary>
        ///     Holds the total number of completed game the user has completed
        /// </summary>
        public int gamesPlayed = 0;

        /// <summary>
        ///     NEED TO STORE THIS IN RESOURCES
        /// </summary>
        private string file_path = Properties.Resources.dewey_file_path;

        /// <summary>
        ///     Instance of RedBlackTree class
        /// </summary>
        RedBlackTree deweyTree = new RedBlackTree();

        /// <summary>
        ///     Holds the node containing the correct answer
        /// </summary>
        private Node correctAnswerNode;

        /// <summary>
        ///     Holds a list of options to display to the user
        /// </summary>
        private List<Node> options;

        /// <summary>
        ///     Holds a list of labels
        /// </summary>
        private List<Label> optionLabels;

        /// <summary>
        ///     Holds a list of panels from the control in order to alter its properties
        /// </summary>
        private Panel[] optionPanels;

        /// <summary>
        ///     Label to change question text
        /// </summary>
        private Label lblQuestion;

        /// <summary>
        ///     Holds the current level the user is on
        /// </summary>
        private int currrentLevel = 1;

        /// <summary>
        ///     Holds the first question of each the game
        /// </summary>
        private string firstQuestion;

        /// <summary>
        ///     Holds the correct answer node found using .Find()
        /// </summary>
        private Node answerFound;

        /// <summary>
        ///     Instance of SoundPlayer
        /// </summary>
        private SoundPlayer soundPlayer = new SoundPlayer();

        /// <summary>
        ///     Holds the number of lives the user has left
        /// </summary>
        public int livesLeft = 3;

        /// <summary>
        ///     Instance of FindingCallNumberTreeControl
        /// </summary>
        private FindingCallNumberTreeControl control;

        /// <summary>
        ///     Custom blue hex for panels
        /// </summary>
        string blue_hex = "#0C98E8";

        /// <summary>
        ///     Custom orange hex for panels
        /// </summary>
        string orange_hex = "#FF8C05";

        /// <summary>
        ///     Custom orange hex for panels
        /// </summary>
        string purple_hex = "#FF3FEC";
        #endregion

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="control"></param>
        public FileWorker(FindingCallNumberTreeControl control)
        {
            this.control = control;
        }

        #region Invokes
        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Invoking (Triggering) the GameCompleted event
        /// </summary>
        private void OnGameCompleted()
        {
            GameCompleted?.Invoke(this, EventArgs.Empty);
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Invoking (Triggering) the exitToMenu event
        /// </summary>
        private void OnUserWantsToExit()
        {
            exitToMenu.Invoke(this, EventArgs.Empty);
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Invoking (Triggering) the LifeLost event
        /// </summary>
        private void OnLifeLost()
        {
            LifeLost?.Invoke(this, EventArgs.Empty);
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Invoking (Triggering) the gameReset event
        /// </summary>
        private void OnGameReset()
        {
            gameReset?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Reads from the provided .csv file
        ///     https://www.oclc.org/research/activities/browser/desc.html
        /// </summary>
        public void ReadFromFile()
        {
            using (StreamReader reader = new StreamReader(file_path))
            {
                // Skipping the header line
                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = SplitCsvLine(line);

                    if (parts.Length == 3)
                    {
                        DeweyModel deweyModel = new DeweyModel
                        {
                            ClassNumber = parts[0].Trim(),
                            Description = parts[1].Trim(),
                            Level = int.Parse(parts[2].Trim())
                        };

                        // Inserting node of type deweyModel
                        deweyTree.Insert(deweyModel);
                    }
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Method resets game with random values
        /// </summary>
        public void ResetGame()
        {
            // Invoking Listener
            OnGameReset();

            currrentLevel = 1;

            livesLeft = 3;

            lblQuestion.Text = string.Empty;

            // Removing events from panels on reset
            foreach (var panel in optionPanels)
            {
                panel.Click -= Panel_Click;
                panel.MouseEnter -= Panel_MouseEnter;
                panel.MouseLeave -= Panel_MouseLeave;
                panel.Controls.Clear();
                panel.Tag = null;
            }

            optionLabels.Clear();
            options.Clear();

            // Generating a new question for game
            DisplayQuestion(optionPanels, lblQuestion);
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Splits the csv lines at each ';' and ensures '""' is counted as a whole string
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string[] SplitCsvLine(string line)
        {
            // Split the line, considering quoted strings and semicolons
            string[] parts = line.Split(';');

            for (int i = 0; i < parts.Length; i++)
            {
                // Removing leading and trailing spaces
                parts[i] = parts[i].Trim();

                // Removing surrounding quotes if present
                if (parts[i].StartsWith("\"") && parts[i].EndsWith("\""))
                {
                    parts[i] = parts[i].Substring(1, parts[i].Length - 2);
                }
            }

            return parts;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     
        /// </summary>
        /// <param name="optionPanels"></param>
        /// <param name="lblQuestion"></param>
        public void DisplayQuestion(Panel[] optionPanels, Label lblQuestion)
        {

            this.optionPanels = optionPanels;
            this.lblQuestion = lblQuestion;

            // Getting a random third-level node
            string firstLevel = "100";
            correctAnswerNode = GetRandomThirdLevelEntry(out firstLevel);

            // Getting all first-level options
            var topLevelOptions = GetTopLevelOptions();
            Shuffle(topLevelOptions);

            // Selecting the correct first level
            string correctFirstLevel = correctAnswerNode.DeweyData.ClassNumber.Substring(0, 1) + "00";

            // Filtering the first-level options to only get correct first-nodes
            List<Node> correctFirstLevelOptions = topLevelOptions
                .Where(node => node.DeweyData.ClassNumber.StartsWith(correctFirstLevel))
                .ToList();

            // Shuffling and take three random incorrect options
            List<Node> incorrectOptions = topLevelOptions
                .Except(correctFirstLevelOptions)
                .OrderBy(_ => Guid.NewGuid())
                .Take(3)
                .ToList();

            // Combining correct and incorrect options
            options = new List<Node>(correctFirstLevelOptions);
            options.AddRange(incorrectOptions);

            // Sorting the combined options by ClassNumber
            options.Sort((x, y) => x.DeweyData.ClassNumber.CompareTo(y.DeweyData.ClassNumber));

            // Initialize the list to store labels
            optionLabels = new List<Label>();

            lblQuestion.Text = $"{correctAnswerNode.DeweyData.Description}";

            firstQuestion = correctAnswerNode.DeweyData.ClassNumber;
            correctAnswerNode = GetAnswerNode(correctAnswerNode.DeweyData);

            var blue = ColorTranslator.FromHtml(blue_hex);

            // Display the options in each panel
            for (int i = 0; i < optionPanels.Length; i++)
            {
                Panel panel = optionPanels[i];
                panel.Click += Panel_Click;
                panel.MouseEnter += Panel_MouseEnter;
                panel.MouseLeave += Panel_MouseLeave;
                panel.BringToFront();
                panel.Tag = options[i];
                panel.BackColor = blue;

                // Create a new label for each panel
                Label lblOption = new Label
                {
                    AutoSize = false,
                    Width = 160,
                    Height = 55,
                    Text = $"{options[i].DeweyData.ClassNumber} - {options[i].DeweyData.Description}",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    ForeColor = SystemColors.ControlLightLight,
                    UseMnemonic = false,
                    Tag = options[i],
                    Enabled = false
                };

                // Adding the label to the panel
                panel.Controls.Add(lblOption);

                // Adding the label to the list
                optionLabels.Add(lblOption);
            }
        }

        #region Panel Events

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Method to alter the mouse curser if the user enters the panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            (sender as Panel).Cursor = Cursors.Hand;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Method to alter the mouse curser if the user leaves the panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            (sender as Panel).Cursor = Cursors.Default;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Click events for all option panels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Click(object sender, EventArgs e)
        {
            // Getting clicked panel
            Panel clickedPanel = sender as Panel;


            // Is panels tag a node?
            if (clickedPanel.Tag is Node selectedNode)
            {
                if (currrentLevel == 1)
                {
                    ProcessLevel1Click(selectedNode);
                }
                else if (currrentLevel == 2)
                {
                    ProcessLevel2Click(selectedNode);
                }
                else if (currrentLevel == 3)
                {
                    ProcessLevel3Click(selectedNode);
                }
            }
        }

        #endregion

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Processes user answer for level 1
        /// </summary>
        /// <param name="selectedNode"></param>
        private void ProcessLevel1Click(Node selectedNode)
        {
            // Whether the user selected answer is correct or not
            bool isCorrect = ValidateUserAnswer(selectedNode);

            // If the answer is correct, then play the sound, and display new options for level 2
            if (isCorrect)
            {
                _ = PlaySound("Wink.mp3");
                currrentLevel++;
                control.SetLabelCorrect();
                control.UpdateLabelText("Well done! Moving to round 2...");
                UpdateOptionsForLevel2();
                return;
            }

            HandleLives();
            
            // if user has no more lives left, play a sound, and show game over splash screen.
            if (livesLeft == 0)
            {
                _ = PlaySound("violinLose.mp3");
                using (var gameOverForm = new GameOverSplash())
                {
                    gameOverForm.ShowDialog();

                    if (gameOverForm.PlayAgain)
                    {
                        gamesPlayed += 1;
                        ResetGame();
                        return;
                    }
                    else
                    {
                        var mainForm = new FindingCallNumberTreeControl();
                        mainForm.StopMusic();
                        return;
                    }
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Processes user answer for level 2
        /// </summary>
        /// <param name="selectedNode"></param>
        private void ProcessLevel2Click(Node selectedNode)
        {
            // Whether the user selected answer is correct or not
            bool isCorrect = ValidateUserAnswer2ndLevel(selectedNode);

            // If the answer is correct, then play the sound, and display new options for level 3
            if (isCorrect)
            {
                _ = PlaySound("Wink.mp3");
                currrentLevel++;
                control.SetLabelCorrect();
                control.UpdateLabelText("That was too easy! Taking you to the last round...");
                UpdateOptionsForLevel3(selectedNode);
                return;
            }

            HandleLives();

            // if user has no more lives left, play a sound, and show game over splash screen.
            if (livesLeft == 0)
            {
                _ = PlaySound("violinLose.mp3");
                
                using (var gameOverForm = new GameOverSplash())
                {
                    gameOverForm.ShowDialog();

                    // Show game of dialog
                    if (gameOverForm.PlayAgain)
                    {
                        gamesPlayed += 1;
                        ResetGame();
                        return;
                    }
                    else
                    {
                        var mainForm = new FindingCallNumberTreeControl();
                        mainForm.StopMusic();
                        return;
                    }
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Processes user answer for level 3
        /// </summary>
        /// <param name="selectedNode"></param>
        private void ProcessLevel3Click(Node selectedNode)
        {
            // Whether the user selected answer is correct or not
            bool isCorrect = ValidateUserAnswer3rdLevel(selectedNode);

            if (isCorrect)
            {
                _ = PlaySound("Success.mp3");
                OnGameCompleted();

                using (var gameWinForm = new WinForm(livesLeft))
                {
                    gameWinForm.ShowDialog();

                    if (gameWinForm.PlayAgain)
                    {
                        ResetGame();
                        return;
                    }
                    else 
                    { 
                        var mainForm = new FindingCallNumberTreeControl(); 
                        mainForm.StopMusic();
                        return; 
                    }
                }
            }
            
            HandleLives();

            // Game is over, play sound and show game over dialog
            if (livesLeft == 0)
            {
                _ = PlaySound("violinLose.mp3");
                using (var gameOverForm = new GameOverSplash())
                {
                    gameOverForm.ShowDialog();

                    if (gameOverForm.PlayAgain)
                    {
                        gamesPlayed += 1;
                        ResetGame();
                        return;
                    }
                    else 
                    { 
                        return; 
                    }
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Handles what to do with user's lives when they are needed to be taken away or added back
        /// </summary>
        private void HandleLives()
        {
            if (livesLeft > 0)
            {
                _ = PlaySound("wrongChoice.mp3");
                control.SetLabelError();
                control.UpdateLabelText("Wrong answer...");
                livesLeft -= 1;
                OnLifeLost();
            }

            // OnUserWantsToExit();
            return;
        }

        #region Validate User Answers
        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Checks user selected answer against the correct answer node in the tree
        /// </summary>
        /// <param name="selectedOption"></param>
        /// <returns></returns>
        private bool ValidateUserAnswer(Node selectedOption)
        {
            return selectedOption.DeweyData.ClassNumber == correctAnswerNode.DeweyData.ClassNumber;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Checks user selected answer against the correct answer node in the tree
        /// </summary>
        /// <param name="selectedOption"></param>
        /// <returns></returns>
        private bool ValidateUserAnswer2ndLevel(Node selectedOption)
        {
            return selectedOption.DeweyData.ClassNumber == answerFound.DeweyData.ClassNumber;
        }
        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Checks user selected answer against the correct answer node in the tree
        /// </summary>
        /// <param name="selectedOption"></param>
        /// <returns></returns>
        private bool ValidateUserAnswer3rdLevel(Node selectedOption)
        {
            return selectedOption.DeweyData.ClassNumber == answerFound.DeweyData.ClassNumber;
        }
        #endregion

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Retrieve third level nodes and adds them to a list<node>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="result"></param>
        private void CollectThirdLevelNodes(Node current, List<Node> result)
        {
            if (current != null)
            {
                // Traverse right subtree
                CollectThirdLevelNodes(current.right, result);

                if (current.DeweyData.Level == 3)
                {
                    result.Add(current);
                }

                CollectThirdLevelNodes(current.left, result);
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Method to retrieve the first levell options
        ///     ChatGpt()
        /// </summary>
        /// <returns></returns>
        private List<Node> GetTopLevelOptions()
        {
            Node rootNode = deweyTree.GetRoot();
            List<Node> topLevelOptions = new List<Node>();

            // Using breadth-first traversal to find top-level options
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(rootNode);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                // Check if the current node is at the first level
                if (current != null && current.DeweyData.Level == 1)
                {
                    topLevelOptions.Add(current);
                }

                // Enqueue the child nodes
                if (current != null)
                {
                    queue.Enqueue(current.left);
                    queue.Enqueue(current.right);
                }
            }

            return topLevelOptions;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Collects all tree nodes
        /// </summary>
        /// <param name="current"></param>
        /// <param name="result"></param>
        private void CollectAllNodes(Node current, List<Node> result)
        {
            // Ensuring the current node is not null
            if (current != null)
            {
                // Traversing right subtree
                CollectAllNodes(current.right, result);

                // Adding the current node to the result
                result.Add(current);

                // Traversing left subtree
                CollectAllNodes(current.left, result);
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Returns the answer node
        /// </summary>
        /// <param name="deweyModel"></param>
        /// <returns></returns>
        private Node GetAnswerNode(DeweyModel deweyModel)
        {
            var tempClassNum = deweyModel.ClassNumber.Substring(0, 1) + "00";

            // Searching the tree for the correct answer node
            var answer = deweyTree.Find(tempClassNum);

            return answer;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Returns a random third level entry
        /// </summary>
        /// <param name="correspondingFirstLevel"></param>
        /// <returns></returns>
        private Node GetRandomThirdLevelEntry(out string correspondingFirstLevel)
        {
            // Retrieving tree root
            var rootNode = deweyTree.GetRoot();

            var thirdLevelNodes = new List<Node>();
            CollectThirdLevelNodes(rootNode, thirdLevelNodes);

            // Getting random nodes
            var random = new Random();
            int randomIndex = random.Next(thirdLevelNodes.Count);

            var randomThirdLevelNode = thirdLevelNodes[randomIndex];
            correspondingFirstLevel = randomThirdLevelNode.DeweyData.ClassNumber.Substring(0, 1) + "00";

            return randomThirdLevelNode;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Shuffle method in order to randomize a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        private void Shuffle<T>(List<T> list)
        {
            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Method to play a sound, url path is passed so that the method is dynamic
        /// </summary>
        /// <param name="url"></param>
        public async Task PlaySound(string url)
        {
            await Task.Run(() =>
            {
                soundPlayer.PlaySound($"Media\\{url}");
            });
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Displays new options to the user for level 2
        /// </summary>
        private void UpdateOptionsForLevel2()
        {
            // Getting all second-level options
            var secondLevelOptions = GetSecondLevelOptions();

            // Selecting the correct second level
            string correctSecondLevel = correctAnswerNode.DeweyData.ClassNumber.Substring(0, 2) + "0";

            // Filtering second-level options to get only the nodes with round numbers
            List<Node> correctSecondLevelOptions = secondLevelOptions
                .Where(node => node.DeweyData.ClassNumber.EndsWith("0"))
                .ToList();

            // Ensuring that the correct answer is included
            if (!correctSecondLevelOptions.Any(node =>
                node.DeweyData.ClassNumber == correctAnswerNode.DeweyData.ClassNumber.Substring(0, 2) + "0"))
            {
                correctSecondLevelOptions.Add(correctAnswerNode);
            }

            // Shuffling all the options both incorrect and correct
            List<Node> allOptions = new List<Node>(correctSecondLevelOptions);
            allOptions.AddRange(secondLevelOptions.Except(correctSecondLevelOptions));
            Shuffle(allOptions);

            // Take three options from the shuffled list
            options = allOptions.Take(3).ToList();
            var substring = firstQuestion.Substring(0, 2) + "0";
            answerFound = deweyTree.Find(substring);
            // Remember to make this order random
            options.Add(answerFound);

            // Sorting the list of options into ascending order by call number
            options.Sort((x, y) => x.DeweyData.ClassNumber.CompareTo(y.DeweyData.ClassNumber));

            // Updating labels
            for (int i = 0; i < optionLabels.Count; i++)
            {
                if (optionLabels[i] != null && options[i] != null && options[i].DeweyData != null)
                {
                    optionLabels[i].Text = $"{options[i].DeweyData.ClassNumber} - {options[i].DeweyData.Description}";
                }
                else
                {
                    Console.WriteLine("An error ");
                }
            }

            // Instantiating a new color
            var orange = ColorTranslator.FromHtml(orange_hex);

            // Changing panel color for level 2
            for (int i = 0; i < optionPanels.Length; i++)
            {
                optionPanels[i].Tag = options[i];
                optionPanels[i].BackColor = orange;
                (optionPanels[i].Controls[0] as Label).Text = $"{options[i].DeweyData.ClassNumber} - {options[i].DeweyData.Description}";
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Returns a list of  second level options to display to the user
        /// </summary>
        /// <returns></returns>
        private List<Node> GetSecondLevelOptions()
        {
            // Getting root
            Node rootNode = deweyTree.GetRoot();
            List<Node> secondLevelOptions = new List<Node>();

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(rootNode);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                // Checking if the current node is at the second level
                if (current != null && current.DeweyData.Level == 2)
                {
                    secondLevelOptions.Add(current);
                }

                // Enqueueing the child nodes
                if (current != null)
                {
                    queue.Enqueue(current.left);
                    queue.Enqueue(current.right);
                }
            }

            return secondLevelOptions;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///      Displays new options to the user for level 3
        /// </summary>
        /// <param name="selectedNode"></param>
        private void UpdateOptionsForLevel3(Node selectedNode)
        {
            // Get all third-level options
            var thirdLevelOptions = GetThirdLevelOptions();

            // Select the correct third level
            string correctThirdLevel = selectedNode.DeweyData.ClassNumber.Substring(0, 3);

            // Filtering the third-level options in order to get only the nodes with the correct third level
            List<Node> correctThirdLevelOptions = thirdLevelOptions
                .Where(node => node.DeweyData.ClassNumber.StartsWith(correctThirdLevel))
                .ToList();

            // Ensuring that the correct answer is included
            if (!correctThirdLevelOptions.Any(node => node.DeweyData.ClassNumber == selectedNode.DeweyData.ClassNumber))
            {
                correctThirdLevelOptions.Add(selectedNode);
            }

            // Shuffling all the options for both correct and incorrect
            List<Node> allOptions = new List<Node>(correctThirdLevelOptions);
            allOptions.AddRange(thirdLevelOptions.Except(correctThirdLevelOptions));
            //Shuffle(allOptions);

            // Taking three options from the shuffled list
            options = allOptions.Take(3).ToList();
            answerFound = deweyTree.Find(firstQuestion);
            options.Add(answerFound);
            //Shuffle(options);
            // Sorting options by call number
            options.Sort((x, y) => x.DeweyData.ClassNumber.CompareTo(y.DeweyData.ClassNumber));

            // Updating labels
            for (int i = 0; i < optionLabels.Count; i++)
            {
                optionLabels[i].Text = $"{options[i].DeweyData.ClassNumber} - {options[i].DeweyData.Description}";
            }

            // Instantiating a new color
            var purple = ColorTranslator.FromHtml(purple_hex);

            // Updating panels and their color for level 3
            for (int i = 0; i < optionPanels.Length; i++)
            {
                optionPanels[i].Tag = options[i];
                optionPanels[i].BackColor = purple;
                (optionPanels[i].Controls[0] as Label).Text = $"{options[i].DeweyData.ClassNumber} - {options[i].DeweyData.Description}";
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Returns a list of ndoes from the third level in order to displaying third levek options to the user
        /// </summary>
        /// <returns></returns>
        private List<Node> GetThirdLevelOptions()
        {
            Node rootNode = deweyTree.GetRoot();
            List<Node> thirdLevelOptions = new List<Node>();

            // Use breadth-first traversal to find third-level options
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(rootNode);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                // Check if the current node is at the third level
                if (current != null && current.DeweyData.Level == 3)
                {
                    thirdLevelOptions.Add(current);
                }

                // Enqueue the child nodes
                if (current != null)
                {
                    queue.Enqueue(current.left);
                    queue.Enqueue(current.right);
                }
            }
            return thirdLevelOptions;
        }
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //