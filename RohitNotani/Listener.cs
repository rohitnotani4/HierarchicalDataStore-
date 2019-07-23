using System;

namespace RohitNotani
{
    /// <summary>
    /// Listener class which handles the events fired by <see cref="TreeNode{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of value for TreeNode</typeparam>
    public class Listener<T>
    {
        /// <summary>
        /// Event Handler
        /// </summary>
        /// <param name="sender"> TreeNode object which fired the event</param>
        /// <param name="e"> TreeNodeChangedEventArgs </param>
        public void OnNodeChanged(object sender, TreeNodeChangedEventArgs<T> e)
        {
            Console.WriteLine("\nEvent : \"" + e.EventType + "\" Node Name \"" + e.NodeName + "\" Node Value \"" + e.NodeValue + "\"");
        }
    }
}
