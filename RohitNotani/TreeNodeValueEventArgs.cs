using System;

namespace RohitNotani
{
    /// <summary>
    /// Event args to store changes to TreeNode
    /// </summary>
    /// <typeparam name="T"> Type of TreeNode data</typeparam>
    public class TreeNodeChangedEventArgs<T> : EventArgs
    {
        public string EventType { get; private set; }
        public string NodeName { get; private set; }
        public T NodeValue { get; private set; }
        public TreeNodeChangedEventArgs(string eventType, string nodeName, T nodeValue)
        {
            this.EventType = eventType;
            this.NodeName = nodeName;
            this.NodeValue = nodeValue;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TreeNodeChangedEventHandler<T>(object sender, TreeNodeChangedEventArgs<T> e);
}
