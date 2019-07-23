using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RohitNotani
{
    /// <summary>
    /// A DataStore where the data has a hierarchy along with data
    /// </summary>
    /// <typeparam name="T"> Type of data to be stored</typeparam>
    public class HierarchicalDataStore<T> : IHierarchicalDataStore<T>
    {
        private readonly TreeNode<T> _parentOfRoot;

        /// <summary>
        /// Default Constructor for HierarchicalDataStore
        /// </summary>
        public HierarchicalDataStore()
        {
            this._parentOfRoot = new TreeNode<T>("/", default(T));
        }

        #region MAIN APIs

        /// <inheritdoc/>
        public void CreateNode(string path, T data)
        {
            var allSubPaths = GetAllSubPaths(path);
            createNodeRecursive(allSubPaths, 0, data, _parentOfRoot);
        }

        /// <inheritdoc/>
        public void UpdateNode(string path, T newValue)
        {
            var nodeForPath = GetNodeForPath(path);
            nodeForPath.UpdateNodeValue(newValue);
        }

        /// <inheritdoc/>
        public void DeleteNode(string path)
        {
            var nodeForPath = GetNodeForPath(path);
            nodeForPath.DeleteNode();
        }

        /// <inheritdoc/>
        public T GetNodeData(string path)
        {
            var nodeForPath = GetNodeForPath(path);
            return nodeForPath.NodeValue;
        }

        /// <inheritdoc/>
        public LinkedList<TreeNode<T>> ListDirectChilds(string path)
        {
            var nodeForPath = GetNodeForPath(path);
            Console.Write("\nList of direct childs of "+ nodeForPath.NodeName + " ");
            foreach (var currNode in nodeForPath.Children)
            {
                Console.Write(currNode.NodeName + " ");
            }
            return nodeForPath.Children;
        }

        /// <summary>
        /// Print the current tree in level order with root as first node
        /// </summary>
        public void PrintAllNodesFromRoot()
        {
            _parentOfRoot.Children.First().PrintTreeInLevelOrder();
        }

        /// <summary>
        /// Adds a Listener of <see cref="TreeNodeChangedEventHandler{T}"/> to the node for given path
        /// </summary>
        /// <param name="path"> Path for Node</param>
        /// <param name="handler">Listener to add</param>
        public void AddListener(string path, TreeNodeChangedEventHandler<T> handler)
        {
            var nodeForPath = GetNodeForPath(path);
            nodeForPath.RegisterForTreeNodeChanged(handler);
        }

        #endregion

        #region Helper Functions
        /// <summary>
        /// Gets the corresponding TreeNode for given path
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>TreeNode, if path is valid, else throws exception</returns>
        private TreeNode<T> GetNodeForPath(string path)
        {
            var allSubPaths = GetAllSubPaths(path);
            return FindNodeInStore(allSubPaths, 0, _parentOfRoot);
        }

        /// <summary>
        /// Breaks the passed path and returns an array of all subPaths
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>An array of all subpaths</returns>
        private string[] GetAllSubPaths(string path)
        {
            var allSubPaths = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return allSubPaths;
        }

        /// <summary>
        /// Creates the TreeNode for given path. Also creates any intermediate nodes which doesn't exists.
        /// For Ex: if current tree is -> root->child1 and we want to create a node root->child2->subchild1
        /// then this function will create child2 first  and then subchild1
        /// </summary>
        /// <param name="subPaths"> An array of all subpaths </param>
        /// <param name="index"> Current Index </param>
        /// <param name="data"> Data for Node</param>
        /// <param name="parent"> Parent of the node at current index</param>
        private void createNodeRecursive(string[] subPaths, int index, T data, TreeNode<T> parent)
        {
            if (index < subPaths.Length)
            {
                bool present = parent.IsChildrenPresent(subPaths[index]);
                TreeNode<T> child;
                if (!present)
                {
                    child = parent.AddChild(subPaths[index], data);
                }
                else
                {
                    child = parent.GetTreeNode(subPaths[index]);
                }
                parent = child;
                createNodeRecursive(subPaths, index + 1, data, parent);
            }
        }

        /// <summary>
        /// Finds if the path passed is valid by breaking it into subpaths and ensuring all nodes exists
        /// </summary>
        /// <param name="subPaths"> An array of all subpaths </param>
        /// <param name="index"> Current Index </param>
        /// <param name="parent"> Parent of the node at current index</param>
        /// <returns></returns>
        private TreeNode<T> FindNodeInStore(string[] subPaths, int index, TreeNode<T> parent)
        {
            if (index < subPaths.Length)
            {
                bool present = parent.IsChildrenPresent(subPaths[index]);
                if (!present)
                {
                    throw new Exception("One of the SubPaths in the Path provided doesn't exist. Please provide correct path");
                }
                parent = parent.GetTreeNode(subPaths[index]);
                return FindNodeInStore(subPaths, index + 1, parent);
            }
            if (index == subPaths.Length)
            {
                return parent;
            }
            return null;
        }

        #endregion
    }
}