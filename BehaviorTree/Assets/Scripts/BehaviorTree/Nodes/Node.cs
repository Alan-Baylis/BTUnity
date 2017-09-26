using System;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    [System.Serializable]
    public abstract class Node
    {
        public enum NodeState
        {
            Succes,
            Failure,
            Running
        }

        [SerializeField]
        private NodeState _currentState;

        public NodeState CurrentState
        {
            get { return this._currentState; }
            protected set { this._currentState = value; }
        }

        /* Delegate that returns the state of the node.*/
        //public delegate NodeState NodeReturn();

        // Implementing classes use this method to evaluate the desired set of conditions
        public abstract NodeState Evaluate();
    }
}