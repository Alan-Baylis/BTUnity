using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class FakeNode : Node
    {
        [SerializeField]
        private NodeState _state;

        public FakeNode(NodeState state)
        {
            _state = state;
        }

        public override NodeState Evaluate()
        {
            this.CurrentState = this._state;
            return this.CurrentState;
        }
    }
}