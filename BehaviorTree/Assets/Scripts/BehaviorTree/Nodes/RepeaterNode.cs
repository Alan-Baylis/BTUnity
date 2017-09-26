using System.Diagnostics;
using System.Threading;
using Debug = UnityEngine.Debug;

namespace BehaviorTree.Nodes
{
    public class RepeaterNode : Node
    {
        public NodeState WaitForState { get; set; }

        public int NumberOfRepeats { get; set; }

        public Node ChildNode { get; set; }


        public RepeaterNode(Node childNode, NodeState waitForState, int numberOfRepeats)
        {
            this.ChildNode = childNode;
            this.WaitForState = waitForState;
            this.NumberOfRepeats = numberOfRepeats;
        }

        // TODO: Multithread?
        public override  NodeState Evaluate()
        {
            int counter = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (counter != this.NumberOfRepeats)
            {
                NodeState result = this.ChildNode.Evaluate();
                if (result == this.WaitForState)
                {
                    this.CurrentState = result;
                    return this.CurrentState;
                }
                Thread.Sleep(10);
                counter++;                
            }
            sw.Stop();
            Debug.Log(string.Format("Repeater node ({0}repeats) took {1}ms", this.NumberOfRepeats, sw.ElapsedMilliseconds));
            this.CurrentState = NodeState.Failure;
            return this.CurrentState;
        }
    }
}