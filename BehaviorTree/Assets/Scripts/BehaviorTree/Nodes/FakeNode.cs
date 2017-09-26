namespace BehaviorTree.Nodes
{
    public class FakeNode : Node
    {
        private readonly NodeState _state;

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