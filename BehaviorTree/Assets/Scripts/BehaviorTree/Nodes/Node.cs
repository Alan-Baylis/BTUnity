using System;
using UnityEngine;

namespace BehaviorTree.Nodes
{
    [System.Serializable]
    public abstract class Node : MonoBehaviour
    {
        public enum NodeState
        {
            Succes,
            Failure,
            Running
        }

        public NodeState CurrentState { get; protected set; }

        /* Delegate that returns the state of the node.*/
        //public delegate NodeState NodeReturn();

        // Implementing classes use this method to evaluate the desired set of conditions
        public abstract NodeState Evaluate();
    }
}