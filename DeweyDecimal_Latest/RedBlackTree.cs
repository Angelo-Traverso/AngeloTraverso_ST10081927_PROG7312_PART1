using DeweyDecimal_Latest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TressSampleApplication.Classes
{
    /// <summary>
    /// Enumeration Defining Colors
    /// </summary>
    enum Color
    {
        Red,
        Black
    }
    internal class RedBlackTree
    {
        //--------------------------------------------------------------------------------------------------//
        ///Class defining a Node - Be careful of defining a clASs within  Class!
        /// <summary>
        /// Object of type Node contains 4 properties
        /// Colour
        /// Left
        /// Right
        /// Parent
        /// Data
        /// </summary>
        public class Node
        {
            public Color colour;
            public Node left;
            public Node right;
            public Node parent;
            public int data;

            public DeweyModel DeweyData { get; set; }

            public Node(DeweyModel deweyData)
            {
                this.DeweyData = deweyData;
            }
            public int CompareTo(Node other)
            {
                return this.DeweyData.ClassNumber.CompareTo(other.DeweyData.ClassNumber);
            }
            public Node(int data) { this.data = data; }
            public Node(Color colour) { this.colour = colour; }
            public Node(int data, Color colour) { this.data = data; this.colour = colour; }
        }
        //--------------------------------------------------------------------------------------------------//


        /// <summary>
        /// Root node of the tree (both reference & pointer)
        /// </summary>
        public Node root;
        public Node GetRoot()
        {
            return root;
        }

        //-------------------------------------------------------------------------------------------------//
        ///Constructor
        /// <summary>
        /// New instance of a Red-Black tree object
        /// </summary>
        public RedBlackTree()
        {
        }

        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Left Rotate
        /// </summary>
        /// <param name="X"></param>
        /// <returns>void</returns>
        private void LeftRotate(Node X)
        {
            if (X.parent != null)
            {
                Node Y = X.right; // set Y
                X.right = Y.left; // turn Y's left subtree into X's right subtree
                if (Y.left != null)
                {
                    Y.left.parent = X;
                }
                if (Y != null)
                {
                    Y.parent = X.parent; // link X's parent to Y
                }
                if (X.parent == null)
                {
                    root = Y;
                }
                else if (X == X.parent.left)
                {
                    X.parent.left = Y;
                }
                else
                {
                    X.parent.right = Y;
                }
                Y.left = X; // put X on Y's left
                if (X != null)
                {
                    X.parent = Y;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Rotate Right
        /// </summary>
        /// <param name="Y"></param>
        /// <returns>void</returns>
        private void RightRotate(Node Y)
        {
            // right rotate is simply mirror code from left rotate
            Node X = Y.left;
            Y.left = X.right;
            if (X.right != null)
            {
                X.right.parent = Y;
            }
            if (X != null)
            {
                X.parent = Y.parent;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            if (Y == Y.parent.right)
            {
                Y.parent.right = X;
            }
            if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }

            X.right = Y;//put Y on X's right
            if (Y != null)
            {
                Y.parent = X;
            }
        }

        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Display Tree
        /// </summary>
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Nothing in the tree!");
            }
            else
            {
                //InOrderDisplay(root, 0);
            }
        }


        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Find item in the tree
        /// </summary>
        /// <param name="key"></param>
        public Node Find(string classNumber)
        {
            bool isFound = false;
            Node temp = root;
            Node item = null;
            while (!isFound)
            {
                if (temp == null)
                {
                    break;
                }

                string tempClassNumber = temp.DeweyData.ClassNumber.ToString();

                int comparisonResult = classNumber.CompareTo(tempClassNumber);

                if (comparisonResult < 0)
                {
                    temp = temp.left;
                }
                else if (comparisonResult > 0)
                {
                    temp = temp.right;
                }
                else
                {
                    isFound = true;
                    item = temp;
                }
            }
            if (isFound)
            {
                // Console.WriteLine("{0} was found", classNumber);
                return temp;
            }
            else
            {
                //Console.WriteLine("{0} not found", classNumber);
                return null;
            }
        }

        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Insert a new object into the RB Tree
        /// </summary>
        /// <param name="item"></param>
        public void Insert(DeweyModel deweyModel)
        {
            Node newItem = new Node(deweyModel);
            if (root == null)
            {
                root = newItem;
                root.colour = Color.Black;
                return;
            }
            Node Y = null;
            Node X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.CompareTo(X) < 0)
                {
                    X = X.left;
                }
                else
                {
                    X = X.right;
                }
            }
            newItem.parent = Y;
            if (Y == null)
            {
                root = newItem;
            }
            else if (newItem.CompareTo(Y) < 0)
            {
                Y.left = newItem;
            }
            else
            {
                Y.right = newItem;
            }
            newItem.left = null;
            newItem.right = null;
            newItem.colour = Color.Red;
            InsertFixUp(newItem);
        }

        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        private void InOrderDisplay(Node current, int depth)
        {
            if (current != null)
            {
                InOrderDisplay(current.right, depth + 1); // Print right subtree with increased depth

                // Add indents based on the depth
                Console.Write(new string(' ', depth * 2));
                Console.WriteLine($"{current.DeweyData.ClassNumber}, {current.DeweyData.Description}, {current.colour}");

                InOrderDisplay(current.left, depth + 1); // Print left subtree with increased depth
            }
        }
        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void InsertFixUp(Node item)
        {
            while (item != null && item != root && item.parent != null && item.parent.colour == Color.Red)
            {
                Node parent = item.parent;
                Node grandparent = parent.parent;

                if (grandparent == null)
                {
                    break; // Exit the loop if the grandparent is null
                }

                if (parent == grandparent.left)
                {
                    Node uncle = grandparent.right;

                    if (uncle != null && uncle.colour == Color.Red)
                    {
                        parent.colour = Color.Black;
                        uncle.colour = Color.Black;
                        grandparent.colour = Color.Red;
                        item = grandparent;
                    }
                    else
                    {
                        if (item == parent.right)
                        {
                            item = parent;
                            LeftRotate(item);
                        }

                        parent.colour = Color.Black;
                        grandparent.colour = Color.Red;
                        RightRotate(grandparent);
                    }
                }
                else
                {
                    Node uncle = grandparent.left;

                    if (uncle != null && uncle.colour == Color.Red)
                    {
                        parent.colour = Color.Black;
                        uncle.colour = Color.Black;
                        grandparent.colour = Color.Red;
                        item = grandparent;
                    }
                    else
                    {
                        if (item == parent.left)
                        {
                            item = parent;
                            RightRotate(item);
                        }

                        parent.colour = Color.Black;
                        grandparent.colour = Color.Red;
                        LeftRotate(grandparent);
                    }
                }
            }

            if (root != null)
            {
                root.colour = Color.Black;
            }
        }


        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Deletes a specified value from the tree
        /// </summary>
        /// <param name="item"></param>
        public void Delete(int key)
        {
            // first find the node in the tree to delete and assign to item pointer/reference
            Node item = Find(key.ToString()); // Convert key to string
            Node X = null;
            Node Y = null;

            if (item == null)
            {
                Console.WriteLine("Nothing to delete!");
                return;
            }
            if (item.left == null || item.right == null)
            {
                Y = item;
            }
            else
            {
                Y = TreeSuccessor(item);
            }
            if (Y.left != null)
            {
                X = Y.left;
            }
            else
            {
                X = Y.right;
            }
            if (X != null)
            {
                X.parent = Y;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            else if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }
            else
            {
                Y.parent.left = X;
            }
            if (Y != item)
            {
                item.DeweyData = Y.DeweyData; // Copy DeweyData
            }
            if (Y.colour == Color.Black)
            {
                DeleteFixUp(X);
            }
        }

        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// Checks the tree for any violations after deletion and performs a fix
        /// </summary>
        /// <param name="X"></param>
        private void DeleteFixUp(Node X)
        {

            while (X != null && X != root && X.colour == Color.Black)
            {
                if (X == X.parent.left)
                {
                    Node W = X.parent.right;
                    if (W.colour == Color.Red)
                    {
                        W.colour = Color.Black; //case 1
                        X.parent.colour = Color.Red; //case 1
                        LeftRotate(X.parent); //case 1
                        W = X.parent.right; //case 1
                    }
                    if (W.left.colour == Color.Black && W.right.colour == Color.Black)
                    {
                        W.colour = Color.Red; //case 2
                        X = X.parent; //case 2
                    }
                    else if (W.right.colour == Color.Black)
                    {
                        W.left.colour = Color.Black; //case 3
                        W.colour = Color.Red; //case 3
                        RightRotate(W); //case 3
                        W = X.parent.right; //case 3
                    }
                    W.colour = X.parent.colour; //case 4
                    X.parent.colour = Color.Black; //case 4
                    W.right.colour = Color.Black; //case 4
                    LeftRotate(X.parent); //case 4
                    X = root; //case 4
                }
                else //mirror code from above with "right" & "left" exchanged
                {
                    Node W = X.parent.left;
                    if (W.colour == Color.Red)
                    {
                        W.colour = Color.Black;
                        X.parent.colour = Color.Red;
                        RightRotate(X.parent);
                        W = X.parent.left;
                    }
                    if (W.right.colour == Color.Black && W.left.colour == Color.Black)
                    {
                        W.colour = Color.Black;
                        X = X.parent;
                    }
                    else if (W.left.colour == Color.Black)
                    {
                        W.right.colour = Color.Black;
                        W.colour = Color.Red;
                        LeftRotate(W);
                        W = X.parent.left;
                    }
                    W.colour = X.parent.colour;
                    X.parent.colour = Color.Black;
                    W.left.colour = Color.Black;
                    RightRotate(X.parent);
                    X = root;
                }
            }
            if (X != null)
                X.colour = Color.Black;
        }

        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// 
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        private Node Minimum(Node X)
        {
            while (X.left.left != null)
            {
                X = X.left;
            }
            if (X.left.right != null)
            {
                X = X.left.right;
            }
            return X;
        }

        //-------------------------------------------------------------------------------------------------//
        /// <summary>
        /// 
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        private Node TreeSuccessor(Node X)
        {
            if (X.left != null)
            {
                return Minimum(X);
            }
            else
            {
                Node Y = X.parent;
                while (Y != null && X == Y.right)
                {
                    X = Y;
                    Y = Y.parent;
                }
                return Y;
            }
        }

    }
}
//-----------------------------------...ooo000 END OF FILE 000ooo...-----------------------------------//
