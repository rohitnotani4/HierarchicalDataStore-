namespace RohitNotani
{
    /// <summary>
    /// Interface to add event mechanism 
    /// </summary>
    interface ITreeNodeEventHandler<T>
    {       
        /// <summary>
        /// Event for any changes to Tree
        /// </summary>
        event TreeNodeChangedEventHandler<T> NodeChanged;

        /// <summary>
        /// Function which provides registeration to a TreeNode. 
        /// This will also add the event to all direct and indirect childs.
        /// </summary>
        /// <param name="listner">The listener which will execute when event is fired</param>
        void RegisterForTreeNodeChanged(TreeNodeChangedEventHandler<T> listner);
    }
}
