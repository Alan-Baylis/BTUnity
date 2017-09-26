using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class InverterNode : Node
    {
        public Node ChildNode { get; set; }

        public InverterNode(Node childNode)
        {
            this.ChildNode = childNode;
        }

/* Reports a success if the child fails and a failure if the child succeeds. Running will report as running */
        public override NodeState Evaluate()
        {
            switch (this.ChildNode.Evaluate())
            {
                case NodeState.Failure:
                    this.CurrentState = NodeState.Succes;
                    return this.CurrentState;
                case NodeState.Succes:
                    this.CurrentState = NodeState.Failure;
                    return this.CurrentState;
                case NodeState.Running:
                    this.CurrentState = NodeState.Running;
                    return this.CurrentState;
                default:
                    this.CurrentState = NodeState.Succes;
                    return this.CurrentState;
            }
        }
    }
}