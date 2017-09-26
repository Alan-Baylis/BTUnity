using System;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class BreakpointNode : Node
    {
        private readonly Action _action;
        private readonly NodeState _state;

        public BreakpointNode(NodeState state, Action action)
        {
            this._state = state;
            if (action == null)
                this._action = () => { Debug.Log("Breakpoint Node hit!"); };
            else
                this._action = action;
        }

        public override NodeState Evaluate()
        {
            this._action();
            this.CurrentState = this._state;
            return this.CurrentState;
        }
    }
}