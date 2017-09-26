using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class RepeaterNode : Node
    {
        private NodeState WaitForState { get; set; }

        private int NumberOfRepeats { get; set; }

        public Node Node { get; set; }


        public RepeaterNode(NodeState waitForState, int numberOfRepeats, Node node)
        {
            WaitForState = waitForState;
            NumberOfRepeats = numberOfRepeats;
            this.Node = node;
        }

        // TODO: Multithread?
        public override  NodeState Evaluate()
        {
            int counter = 0;
            while (counter != this.NumberOfRepeats)
            {
                NodeState result = this.Node.Evaluate();
                if (result == this.WaitForState)
                {
                    this.CurrentState = result;
                    return this.CurrentState;
                }
                counter++;
            }
            this.CurrentState = NodeState.Failure;
            return this.CurrentState;
        }
    }
}