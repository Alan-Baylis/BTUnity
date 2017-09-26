using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class SelectorNode : Node
    {
/** The child nodes for this selector */
        [SerializeField]
        protected List<Node> nodes;

/* If any of the children reports a success,
the selector will immediately report a success upwards.
If all children fail, it will report a failure instead.*/
        public override NodeState Evaluate()
        {
            foreach (Node node in nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Succes:
                        this.CurrentState = NodeState.Succes;
                        return this.CurrentState;
                    case NodeState.Running:
                        this.CurrentState = NodeState.Running;
                        return this.CurrentState;
                    default:
                        continue;
                }
            }
            this.CurrentState = NodeState.Failure;
            return this.CurrentState;
        }
    }
}