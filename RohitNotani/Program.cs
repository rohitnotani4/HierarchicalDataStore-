namespace RohitNotani
{
    /// <summary>
    /// Main class for our Console Application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of our Application
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {
            var myNodeListener = new Listener<string>();
            var myDataStore = new HierarchicalDataStore<string>();

            // Task 1: Add a node - root with a string data “nothing”  
            myDataStore.CreateNode("/root", "nothing");

            // Task 2: Attach a listener which prints all events to root. ​[Bonus]  
            myDataStore.AddListener("/root", myNodeListener.OnNodeChanged);
            
            // Task 3: Add two child nodes to root - child1 with a string data “childdata 1” &  child2 with a string data “childdata 2”   
            myDataStore.CreateNode("/root/child1", "childdata 1");
            myDataStore.CreateNode("/root/child2", "childdata 2");
            
            // Task 4: Add one child node to child1 - subchild1 with a string data “subchild1”. 
            myDataStore.CreateNode("/root/child1/subchild1", "subchild1_data");
            
            // Task 5: Get and print the data for all the nodes.
            // I am printing entire Tree Level-wise for this Task 
            myDataStore.PrintAllNodesFromRoot();

            // Task 6: List all the child nodes for root
            // Assumption : I am listing only direct childs (since all childs tasks is done above)
            myDataStore.ListDirectChilds("/root");

            // Task 7: Delete the node child2. 
            // Assumption : I am doning a cascade delete i.e. It will delete child2 and all its childrens as well
            myDataStore.DeleteNode("/root/child1");
            myDataStore.PrintAllNodesFromRoot();
        }        
    }
}
