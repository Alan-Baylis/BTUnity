using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class FakeNode : Node
    {
        public NodeState State { get; set; }

        public FakeNode(NodeState state)
        {
            State = state;
        }

        public override NodeState Evaluate()
        {
            this.CurrentState = this.State;
            return this.CurrentState;
        }
    }
}