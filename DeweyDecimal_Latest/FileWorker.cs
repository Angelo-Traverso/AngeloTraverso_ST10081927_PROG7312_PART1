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

namespace DeweyDecimal_Latest
{
    public class FileWorker
    {
        public event EventHandler LifeLost;
        public event EventHandler gameReset;
        public int gamesPlayed = 0;
        private string file_path = @"DDResources\\DeweyDecimalValues.csv";
        RedBlackTree deweyTree = new RedBlackTree();
        private Node correctAnswerNode;
        private List<Node> options;
        private List<Label> optionLabels; // Store the labels for later comparison
        private Panel[] optionPanels; // Declare optionPanels as a class-level field
        private Label lblQuestion; // Dec
        private int currrentLevel = 1;
        private string firstQuestion;
        private Node answerFound;
        private SoundPlayer soundPlayer = new SoundPlayer();
        public int livesLeft = 3;

        public void ReadFromFile()
        {
            using (StreamReader reader = new StreamReader(file_path))
            {
                // Skip the header line
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

                        deweyTree.Insert(deweyModel);
                    }
                }
                deweyTree.DisplayTree();
            }
        }

        private void OnLifeLost()
        {
            LifeLost?.Invoke(this, EventArgs.Empty);
        }

        private void OnGameReset()
        { 
            gameReset?.Invoke(this, EventArgs.Empty);
        }

        public void ResetGame()
        {
            // Invoking Listener so that actions can be performed when the game is reset
            OnGameReset();

            // Reset the current level to 1
            currrentLevel = 1;

            livesLeft = 3;

            // Clear the labels and options
            lblQuestion.Text = string.Empty;

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

            // Generate a new question for the first level
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
                // Remove leading and trailing spaces
                parts[i] = parts[i].Trim();

                // Remove surrounding quotes if present
                if (parts[i].StartsWith("\"") && parts[i].EndsWith("\""))
                {
                    parts[i] = parts[i].Substring(1, parts[i].Length - 2);
                }
            }

            return parts;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        public void DisplayQuestion(Panel[] optionPanels, Label lblQuestion)
        {

            this.optionPanels = optionPanels;
            this.lblQuestion = lblQuestion;
            // Get a random third-level node
            string firstLevel = "100";
            correctAnswerNode = GetRandomThirdLevelEntry(out firstLevel);


            // Get all first-level options
            var topLevelOptions = GetTopLevelOptions();
            Shuffle(topLevelOptions);

            // Select the correct first level
            string correctFirstLevel = correctAnswerNode.DeweyData.ClassNumber.Substring(0, 1) + "00";

            // Filter first-level options to get only the nodes with the correct first level
            List<Node> correctFirstLevelOptions = topLevelOptions
                .Where(node => node.DeweyData.ClassNumber.StartsWith(correctFirstLevel))
                .ToList();

            // Shuffle and take three random incorrect options
            List<Node> incorrectOptions = topLevelOptions
                .Except(correctFirstLevelOptions)
                .OrderBy(_ => Guid.NewGuid())
                .Take(3)
                .ToList();

            // Combine correct and incorrect options
            options = new List<Node>(correctFirstLevelOptions);
            options.AddRange(incorrectOptions);

            // Randomising the combined options
            Shuffle(options);

            // Initialize the list to store labels
            optionLabels = new List<Label>();

            // Display the question
            lblQuestion.Text = $"{correctAnswerNode.DeweyData.ClassNumber} - {correctAnswerNode.DeweyData.Description}";
            //firstQuestionAnswer = correctAnswerNode.DeweyData.Description;
            firstQuestion = correctAnswerNode.DeweyData.ClassNumber;
            correctAnswerNode = GetAnswerNode(correctAnswerNode.DeweyData);

            // Display the options in each panel
            for (int i = 0; i < optionPanels.Length; i++)
            {
                Panel panel = optionPanels[i];
                panel.Click += Panel_Click;
                panel.MouseEnter += Panel_MouseEnter;
                panel.MouseLeave += Panel_MouseLeave;
                panel.BringToFront();
                panel.Tag = options[i];

                // Create a new label for each option
                Label lblOption = new Label
                {
                    AutoSize = false,
                    Width = 160,
                    Height = 55,
                    Text = $"{options[i].DeweyData.ClassNumber} - {options[i].DeweyData.Description}",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 10, FontStyle.Regular),
                    //callNumberLabel.Font = new Font("Arial", 8, FontStyle.Regular);
                    Tag = options[i], // Set the Tag property with the node
                    Enabled = false // Enable the label initially
                };

                // Add the label to the panel
                panel.Controls.Add(lblOption);

                // Add the label to the list
                optionLabels.Add(lblOption);
            }
        }
        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            // Change the cursor to a grabbing hand when the mouse enters the panel
            (sender as Panel).Cursor = Cursors.Hand;
        }

        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            // Change the cursor back to the default when the mouse leaves the panel
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
            // Handle the click event for all panels
            Panel clickedPanel = sender as Panel;

            Console.WriteLine("Clicked!");
            _ = PlaySound("Wink.mp3");
            // Check if the panel's tag is a Node
            if (clickedPanel.Tag is Node selectedNode)
            {
                // Compare the selected option with the correct answer
                // Display a message indicating if the answer is correct

                if (currrentLevel == 1)
                {
                    bool isCorrect = ValidateUserAnswer(selectedNode);
                    if (isCorrect)
                    {
                        currrentLevel++;
                        MessageBox.Show("Well done! Moving to round 2...", "Correct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateOptionsForLevel2();
                        return;
                    }
                    else
                    {
                        HandleLives();
                        MessageBox.Show("Wrong answer! Try again...");
                        // NEED TO ADD INCORRECT ANSWER GAMIFICATION
                    }
                }
                
                if (currrentLevel == 2)
                {
                    bool isCorrect = ValidateUserAnswer2ndLevel(selectedNode);
                    if (isCorrect)
                    {
                        currrentLevel++;
                        MessageBox.Show("That was too easy!\nTaking you to the last round...", "Correct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateOptionsForLevel3(selectedNode);
                        return;
                    }
                    else
                    {
                        HandleLives();
                        MessageBox.Show("Whoops, wrong answer...", "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (currrentLevel == 3)
                {
                    bool isCorrect = ValidateUserAnswer3rdLevel(selectedNode);
                    if (isCorrect)
                    {
                        MessageBox.Show("It looks like you know your dewey decimal classifications!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gamesPlayed += 1;
                        ResetGame();
                        return;
                        // Call reset game here
                    }
                    else
                    {
                        HandleLives();
                        MessageBox.Show("No more lives left...");
                    }
                }
            }
        }
        private void HandleLives()
        {
            if (livesLeft > 0)
            {
                livesLeft -= 1;
                OnLifeLost();
            }

            if (livesLeft == 0)
            {
                _ = PlaySound("violinLose.mp3");
                ResetGame();
            }
        }

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

        private bool ValidateUserAnswer2ndLevel(Node selectedOption)
        {
            return selectedOption.DeweyData.ClassNumber == answerFound.DeweyData.ClassNumber;
        }

        private bool ValidateUserAnswer3rdLevel(Node selectedOption)
        {
            return selectedOption.DeweyData.ClassNumber == answerFound.DeweyData.ClassNumber;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        private void CollectThirdLevelNodes(Node current, List<Node> result)
        {
            if (current != null)
            {
                // Traverse right subtree
                CollectThirdLevelNodes(current.right, result);

                // Check if the current node is at the third level
                if (current.DeweyData.Level == 3)
                {
                    result.Add(current);
                }

                // Traverse left subtree
                CollectThirdLevelNodes(current.left, result);
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        private List<Node> GetTopLevelOptions()
        {
            Node rootNode = deweyTree.GetRoot();
            List<Node> topLevelOptions = new List<Node>();

            // Use breadth-first traversal to find top-level options
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
        private void CollectAllNodes(Node current, List<Node> result)
        {
            if (current != null)
            {
                // Traverse right subtree
                CollectAllNodes(current.right, result);

                // Add the current node to the result
                result.Add(current);

                // Traverse left subtree
                CollectAllNodes(current.left, result);
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        private Node GetAnswerNode(DeweyModel deweyModel)
        {
            var tempClassNum = deweyModel.ClassNumber.Substring(0, 1) + "00";
            var answer = deweyTree.Find(tempClassNum);

            return answer;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        private Node GetRandomThirdLevelEntry(out string correspondingFirstLevel)
        {
            var rootNode = deweyTree.GetRoot();
            List<Node> thirdLevelNodes = new List<Node>();
            CollectThirdLevelNodes(rootNode, thirdLevelNodes);

            Random random = new Random();
            int randomIndex = random.Next(thirdLevelNodes.Count);
            var randomThirdLevelNode = thirdLevelNodes[randomIndex];
            correspondingFirstLevel = randomThirdLevelNode.DeweyData.ClassNumber.Substring(0, 1) + "00"; // Extract the corresponding first level

            return randomThirdLevelNode;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        // Implement a method to shuffle a list
        private void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
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
        private void UpdateOptionsForLevel2()
        {
            // Get all second-level options
            var secondLevelOptions = GetSecondLevelOptions();

            // Select the correct second level
            string correctSecondLevel = correctAnswerNode.DeweyData.ClassNumber.Substring(0, 2) + "0";

            // Filter second-level options to get only the nodes with round numbers
            // I changed EndWith over here
            List<Node> correctSecondLevelOptions = secondLevelOptions
                .Where(node => node.DeweyData.ClassNumber.EndsWith("0"))
                .ToList();

            // Ensure that the correct answer is included
            if (!correctSecondLevelOptions.Any(node =>
                node.DeweyData.ClassNumber == correctAnswerNode.DeweyData.ClassNumber.Substring(0, 2) + "0"))
            {
                correctSecondLevelOptions.Add(correctAnswerNode);
            }

            // Shuffle all options (correct and incorrect)
            List<Node> allOptions = new List<Node>(correctSecondLevelOptions);
            allOptions.AddRange(secondLevelOptions.Except(correctSecondLevelOptions));
            Shuffle(allOptions);

            // Take three options from the shuffled list
            options = allOptions.Take(3).ToList();
            var substring = firstQuestion.Substring(0, 2) + "0";
            answerFound = deweyTree.Find(substring);
            // Remember to make this order random
            options.Add(answerFound);

            Shuffle(options);


            for (int i = 0; i < optionLabels.Count; i++)
            {
                optionLabels[i].Text = $"{options[i].DeweyData.ClassNumber} - {options[i].DeweyData.Description}";
            }

            for (int i = 0; i < optionPanels.Length; i++)
            {
                optionPanels[i].Tag = options[i];
                // Update the label text with the new descriptions
                (optionPanels[i].Controls[0] as Label).Text = $"{options[i].DeweyData.ClassNumber} - {options[i].DeweyData.Description}";
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //
        private List<Node> GetSecondLevelOptions()
        {
            Node rootNode = deweyTree.GetRoot();
            List<Node> secondLevelOptions = new List<Node>();

            // Use breadth-first traversal to find second-level options
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(rootNode);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                // Check if the current node is at the second level
                if (current != null && current.DeweyData.Level == 2)
                {
                    secondLevelOptions.Add(current);
                }

                // Enqueue the child nodes
                if (current != null)
                {
                    queue.Enqueue(current.left);
                    queue.Enqueue(current.right);
                }
            }

            return secondLevelOptions;
        }

        private void UpdateOptionsForLevel3(Node selectedNode)
        {
            // Get all third-level options
            var thirdLevelOptions = GetThirdLevelOptions();

            // Select the correct third level
            string correctThirdLevel = selectedNode.DeweyData.ClassNumber.Substring(0, 3);

            // Filter third-level options to get only the nodes with the correct third level
            List<Node> correctThirdLevelOptions = thirdLevelOptions
                .Where(node => node.DeweyData.ClassNumber.StartsWith(correctThirdLevel))
                .ToList();

            // Ensure that the correct answer is included
            if (!correctThirdLevelOptions.Any(node => node.DeweyData.ClassNumber == selectedNode.DeweyData.ClassNumber))
            {
                correctThirdLevelOptions.Add(selectedNode);
            }

            // Shuffle all options (correct and incorrect)
            List<Node> allOptions = new List<Node>(correctThirdLevelOptions);
            allOptions.AddRange(thirdLevelOptions.Except(correctThirdLevelOptions));
            Shuffle(allOptions);

            // Take three options from the shuffled list
            options = allOptions.Take(3).ToList();
            answerFound = deweyTree.Find(firstQuestion);
            options.Add(answerFound);
            Shuffle(options);

            for (int i = 0; i < optionLabels.Count; i++)
            {
                optionLabels[i].Text = $"{options[i].DeweyData.ClassNumber} - {options[i].DeweyData.Description}";
            }

            for (int i = 0; i < optionPanels.Length; i++)
            {
                optionPanels[i].Tag = options[i];
                // Update the label text with the new descriptions
                (optionPanels[i].Controls[0] as Label).Text = $"{options[i].DeweyData.ClassNumber} - {options[i].DeweyData.Description}";
            }
        }

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