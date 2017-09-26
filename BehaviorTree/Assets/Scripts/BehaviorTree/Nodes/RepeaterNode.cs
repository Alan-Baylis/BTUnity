using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class RepeaterNode : Node
    {
        [SerializeField]
        private NodeState _waitForState;

        [Tooltip("Use -1 for infinite")]
        [SerializeField]
        private int _numberOfRepeats;

        public Node Node { get; private set; }

        /// <param name="waitForState">Evaluate until the node returns this state.</param>
        /// <param name="numberOfRepeats">Use -1 for infinite repeats</param>
        /// <param name="node"></param>
        public RepeaterNode(NodeState waitForState, int numberOfRepeats, Node node)
        {
            this._waitForState = waitForState;
            this._numberOfRepeats = numberOfRepeats;
            this.Node = node;
        }

        // TODO: Multithread?
        public override  NodeState Evaluate()
        {
            int counter = 0;
            while (counter != this._numberOfRepeats)
            {
                NodeState result = this.Node.Evaluate();
                if (result == this._waitForState)
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