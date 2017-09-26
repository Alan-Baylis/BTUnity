using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class SequenceNode : Node
    {
/** Chiildren nodes that belong to this sequence */
        [SerializeField]
        private readonly List<Node> _nodes;
        
/* If any child node returns a failure, the entire node fails.
When all nodes return a success, the node reports a success. */
        public override NodeState Evaluate()
        {
            bool anyChildRunning = false;
            foreach (Node node in _nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        this.CurrentState = NodeState.Failure;
                        return this.CurrentState;
                    case NodeState.Succes:
                        continue;
                    case NodeState.Running:
                        anyChildRunning = true;
                        continue;
                    default:
                        this.CurrentState = NodeState.Succes;
                        return this.CurrentState;
                }
            }
            this.CurrentState = anyChildRunning ? NodeState.Running : NodeState.Succes;
            return this.CurrentState;
        }
    }
}