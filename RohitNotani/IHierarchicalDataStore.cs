using System.Collections.Generic;

namespace RohitNotani
{
    /// <summary>
    /// Interface which defines a HierarchicalDataStore
    /// </summary>
    /// <typeparam name="T"> Type of value to be stored in HierarchicalDataStore</typeparam>
    interface IHierarchicalDataStore<T>
    {
        /// <summary>
        /// Creates a node with a path and data
        /// </summary>
        /// <param name="path">Path for node</param>
        /// <param name="data">Node Data</param>
        void CreateNode(string path, T data);

        /// <summary>
        /// Updates data of a node
        /// </summary>
        /// <param name="path"> Path for Node</param>
        /// <param name="data"> New Data</param>
        void UpdateNode(string path, T data);

        /// <summary>
        ///  Deletes a node 
        /// </summary>
        /// <param name="path"> Path for Node to be Deleted</param>
        void DeleteNode(string path);

        /// <summary>
        /// Gets data from a node 
        /// </summary>
        /// <param name="path">Path for Node</param>
        /// <returns></returns>
        T GetNodeData(string path);

        /// <summary>
        /// Gets list of all direct child nodes for a given node
        /// </summary>
        /// <param name="path">Path for Node</param>
        /// <returns>List of all child TreeNodes</returns>
        LinkedList<TreeNode<T>> ListDirectChilds(string path);
    }
}
