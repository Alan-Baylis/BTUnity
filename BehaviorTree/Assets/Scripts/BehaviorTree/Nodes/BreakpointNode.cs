using System;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class BreakpointNode : Node
    {
        public Action Action { get; set; }

        public NodeState StateToSet { get; set; }

        public BreakpointNode(Action action, NodeState stateToSet)
        {
            this.Action = action;
            this.StateToSet = stateToSet;
        }       

        public override NodeState Evaluate()
        {
            if (this.Action == null)
                Debug.Log("Breakpoint Node hit!");
            else
                this.Action();
            this.CurrentState = this.StateToSet;
            return this.CurrentState;
        }
    }
}