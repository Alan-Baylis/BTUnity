namespace BehaviorTree.Nodes
{
    public class ActionNode : Node
    {
/* Method signature for the action. */
        public delegate NodeState ActionNodeDelegate();

/* The delegate that is called to evaluate this node */
        private readonly ActionNodeDelegate _action;

/* Because this node contains no logic itself, the logic must be passed in in the form of
* a delegate. As the signature states, the action needs to return a NodeState enum */
        public ActionNode(ActionNodeDelegate action)
        {
            this._action = action;
        }

/* Evaluates the node using the passed in delegate and
* reports the resulting state as appropriate */
        public override NodeState Evaluate()
        {
            switch (this._action())
            {
                case NodeState.Succes:
                    this.CurrentState = NodeState.Succes;
                    return this.CurrentState;
                case NodeState.Failure:
                    this.CurrentState = NodeState.Failure;
                    return this.CurrentState;
                case NodeState.Running:
                    this.CurrentState = NodeState.Running;
                    return this.CurrentState;
                default:
                    this.CurrentState = NodeState.Failure;
                    return this.CurrentState;
            }
        }
    }
}