using System;
using System.Collections;
using UnityEngine;

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
            while (counter != this.NumberOfRepeats)
            {
                NodeState result = this.ChildNode.Evaluate();
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