using System;
using System.Collections.Generic;
using System.Linq;

namespace RohitNotani
{
    /// <summary>
    /// TreeNode (An n-ary tree)
    /// </summary>
    /// <typeparam name="T">Type of value to be stored in TreeNode</typeparam>
    public class TreeNode<T> : ITreeNodeEventHandler<T>
    {
        #region TreeNode Properties
        /// <summary>
        /// The Name of Node. Ex : root
        /// </summary>
        public string NodeName { get; private set; }

        /// <summary>
        /// The Value of Node.
        /// </summary>
        public T NodeValue { get; private set; }

        /// <summary>
        /// The direct parent of this Node
        /// </summary>
        public TreeNode<T> Parent { get; private set; }

        /// <summary>
        /// The direct childrens of this Node
        /// </summary>
        public LinkedList<TreeNode<T>> Children { get; private set; }

        /// <summary>
        /// Mapping which stores name of Node and its corresponding object 
        /// </summary>
        public Dictionary<string, TreeNode<T>> ChildrenSet { get; private set; }

        /// <summary>
        /// Constructor for TreeNode
        /// </summary>
        /// <param name="name">Name for node</param>
        /// <param name="value">Value for node</param>
        public TreeNode(string name, T value)
        {
            this.NodeName = name;
            this.NodeValue = value;
            this.Children = new LinkedList<TreeNode<T>>();
            this.ChildrenSet = new Dictionary<string, TreeNode<T>>();
        }
        #endregion

        #region Main TreeNode Functions
        /// <summary>
        /// Adds direct child to this TreeNode
        /// </summary>
        /// <param name="name">Name of the child node</param>
        /// <param name="value">Value to the child node</param>
        /// <returns>The created node</returns>
        public TreeNode<T> AddChild(string name, T value)
        {
            var child = new TreeNode<T>(name, value);
            child.Parent = this;            
            this.Children.AddLast(child);
            this.ChildrenSet.Add(name, child);
            if (child.Parent.NodeChanged != null)
            {
                child.NodeChanged += child.Parent.NodeChanged;
            }
            OnNodeChanged(new TreeNodeChangedEventArgs<T>("Node Added", child.NodeName, value));
            return child;
        }

        /// <summary>
        /// Removes the current TreeNode and all its childrens (direct and indirect)
        /// </summary>
        public void DeleteNode()
        {
            var nodeToDelete = this;
            var allNodesToDelete = nodeToDelete.GetSelfAndAllDescendants();
            foreach (var currNode in allNodesToDelete)
            {
                currNode.Parent?.RemoveChild(currNode);
                currNode.ClearNode();
            }
        }

        /// <summary>
        /// Updates the value of the this TreeNode
        /// </summary>
        /// <param name="newValue">The new value</param>
        public void UpdateNodeValue(T newValue)
        {
            this.NodeValue = newValue;
            OnNodeChanged(new TreeNodeChangedEventArgs<T>("Node Updated", NodeName, NodeValue));
        }

        /// <summary>
        /// Gets all self and all direct, indirect childs of a TreeNode
        /// </summary>
        /// <returns></returns>
        public LinkedList<TreeNode<T>> GetSelfAndAllDescendants()
        {
            LinkedList<TreeNode<T>> allChildrens = new LinkedList<TreeNode<T>>();
            Queue<TreeNode<T>> bfsQueue = new Queue<TreeNode<T>>();
            allChildrens.AddLast(this);
            bfsQueue.Enqueue(this);
            while (bfsQueue.Any())
            {
                var currNode = bfsQueue.Dequeue();
                foreach (var childNode in currNode.Children)
                {
                    bfsQueue.Enqueue(childNode);
                    allChildrens.AddLast(childNode);
                }                 
            }
            return allChildrens;
        }

        /// <summary>
        /// Print the current tree in level order fashion
        /// Takes this node as root.
        /// </summary>
        public void PrintTreeInLevelOrder()
        {
            Console.WriteLine("\nLevel Order Traversal of current Tree : ");
            int levelNo = 0;
            Queue<TreeNode<T>> bfsQueue = new Queue<TreeNode<T>>();
            bfsQueue.Enqueue(this);
            int count = 1, newCount = 1;
            while (bfsQueue.Any())
            {
                count = newCount;
                newCount = 0;
                Console.WriteLine("\nLevel " + levelNo + ": ");
                levelNo++;
                while (count > 0)
                {
                    var currNode = bfsQueue.Dequeue();
                    count--;
                    if (currNode != null)
                    {
                        Console.Write("Node name: " + currNode.NodeName + " Node value: " + currNode.NodeValue + " ");
                        foreach (var childNode in currNode.Children)
                        {
                            bfsQueue.Enqueue(childNode);

                        }
                        newCount += currNode.Children.Count;
                    }
                }
            }
            Console.WriteLine();
        }

        #endregion

        #region TreeNode Helper Functions

        /// <summary>
        /// Removes child from the children list
        /// </summary>
        /// <param name="child">Child to remove</param>
        private void RemoveChild(TreeNode<T> child)
        {
            if (this.Children != null)
            {
                this.Children.Remove(child);
                this.ChildrenSet.Remove(child.NodeName);
            }
        }

        /// <summary>
        /// Checks if the node is direct child of current node 
        /// </summary>
        /// <param name="name"></param>
        /// <returns> True if the passed node is direct child, else false</returns>
        public bool IsChildrenPresent(string name)
        {
            return this.ChildrenSet.ContainsKey(name);
        }

        /// <summary>
        /// Gets the TreeNode corresponding to given name
        /// </summary>
        /// <param name="name">Name of Node</param>
        /// <returns>TreeNode, if name is valid, else null</returns>
        public TreeNode<T> GetTreeNode(string name)
        {
            TreeNode<T> result;
            this.ChildrenSet.TryGetValue(name, out result);
            return result;
        }

        /// <summary>
        /// Clears all the properties of current node
        /// </summary>
        private void ClearNode()
        {
            OnNodeChanged(new TreeNodeChangedEventArgs<T>("Node Removed", NodeName, NodeValue));
            this.NodeName = null;
            this.NodeValue = default(T);
            this.Children = null;
            this.ChildrenSet = null;
            this.Parent = null;            
        }

        #endregion

        #region Event Handling
        /// <inheritdoc/>
        public event TreeNodeChangedEventHandler<T> NodeChanged;

        /// <inherticdoc/>
        public void RegisterForTreeNodeChanged(TreeNodeChangedEventHandler<T> listener)
        {
            var allNodes = GetSelfAndAllDescendants();
            foreach(var currNode in allNodes)
            {
                this.NodeChanged += listener;
            }
        }

        protected virtual void OnNodeChanged(TreeNodeChangedEventArgs<T> eventArgs)
        {
            NodeChanged?.Invoke(this, eventArgs);
        }
        #endregion
    }
}
