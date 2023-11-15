using CSharpTree;
using CsvHelper;
using DeweyDecimal_Latest.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimal_Latest
{
    public class FileWorker
    {
        public class DeweyTreeNode : TreeNode<string>
        {
            public DeweyTreeNode(string data) : base(data)
            {
            }
        }
        private DeweyTreeNode root;
        private string filePath = @"Resources\\DeweyDecimalValues.csv";
        private Random random = new Random();
        private List<DeweyModel> deweyData;

        public FileWorker()
        {
            // Initialize the root node
            root = new DeweyTreeNode("Dewey Decimal Classification");
            ReadFromFile();
           // LoadDeweyData();
            //ReadFromFileCSV();
        }

        public void ReadFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] values = line.Split(';');

                            if (values.Length >= 3)
                            {
                                string deweyClass = values[0].Trim();
                                string caption = values[1].Trim();
                                string levelStr = values[2].Trim();

                                if (int.TryParse(levelStr, out int level))
                                {
                                    // Find or create the parent node based on the level
                                    var parentNode = root.FindTreeNode(node => node.Level == level - 1);

                                    // Add the current node to the tree
                                    var currentNode = parentNode.AddChild($"{deweyClass} - {caption}");
                                }
                                else
                                {
                                    Console.WriteLine($"Skipped line. Error parsing level. Invalid format: {line}");
                                }
                            }
                        }
                    }
                }

                // Print the Dewey Decimal Tree to the console with indentation
                PrintDeweyTree(root, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the CSV file: {ex.Message}");
            }
        }

        private void LoadDeweyData()
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    deweyData = csv.GetRecords<DeweyModel>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Dewey Decimal data: {ex.Message}");
            }
        }
        private void PrintDeweyTree(TreeNode<string> node, int indent)
        {
            Console.WriteLine($"{new string(' ', indent * 2)}{node.Data}");

            foreach (var child in node.Children)
            {
                PrintDeweyTree(child, indent + 1);
            }
        }

        public void FindingCallNumbersTask()
        {
            Console.WriteLine("Finding Call Numbers Task");
            Console.WriteLine("Press any key to start the quiz...");
            Console.ReadKey();

            // Start the quiz
            RunQuiz(root);
        }

        private void RunQuiz(TreeNode<string> currentNode)
        {
            // Get a random third-level entry
            var thirdLevelNode = GetRandomThirdLevelNode(currentNode);

            // Display the quiz question
            Console.WriteLine($"Quiz Question: {thirdLevelNode.Data}");

            // Display the options
            var options = GetQuizOptions(thirdLevelNode);
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            // Get user input (simulate user selecting an option)
            Console.Write("Select an option (1-4): ");
            int userChoice = Convert.ToInt32(Console.ReadLine());

            // Check user's choice
            if (userChoice >= 1 && userChoice <= 4)
            {
                // Get the selected option
                var selectedOption = options[userChoice - 1];

                // Check if the selected option is correct
                if (selectedOption == thirdLevelNode.Parent?.Data)
                {
                    Console.WriteLine("Correct! Moving to the next level...");
                    // Recursively call the quiz for the next level
                    RunQuiz(thirdLevelNode);
                }
                else
                {
                    Console.WriteLine($"Incorrect! The correct answer is: {thirdLevelNode.Parent?.Data}");
                    // Implement logic to handle incorrect answers and move to the next question
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select a number between 1 and 4.");
            }
        }

        private DeweyTreeNode GetRandomThirdLevelNode(TreeNode<string> rootNode)
        {
            // Filter third-level nodes
            var thirdLevelNodes = new List<DeweyTreeNode>();
            foreach (var child in rootNode.Children)
            {
                foreach (var grandchild in child.Children)
                {
                    if (grandchild is DeweyTreeNode deweyTreeNode && deweyTreeNode.Level == 2)
                    {
                        thirdLevelNodes.Add(deweyTreeNode);
                    }
                }
            }

            // Randomly select a third-level node
            int randomIndex = random.Next(thirdLevelNodes.Count);
            return thirdLevelNodes[randomIndex];
        }
        private List<string> GetQuizOptions(TreeNode<string> node)
        {
            // Get the parent node (second level) and its siblings
            var secondLevelNodes = new List<DeweyTreeNode>();
            if (node.Parent != null && node.Parent.Parent != null)
            {
                foreach (var sibling in node.Parent.Parent.Children)
                {
                    if (sibling is DeweyTreeNode deweyTreeNode)
                    {
                        secondLevelNodes.Add(deweyTreeNode);
                    }
                }
            }

            // Shuffle the second level nodes
            Shuffle(secondLevelNodes);

            // Prepare options
            var options = new List<string>();
            foreach (var optionNode in secondLevelNodes)
            {
                var firstChild = optionNode.Children.FirstOrDefault();
                options.Add($"{optionNode.Data} - {firstChild?.Data}"); // Assuming the description is the first child
            }

            return options;
        }

        private void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    public class CsvDeweyModel
    {
        public int Class { get; set; }
        public string Caption { get; set; }
        public int Summary { get; set; }
    }
}