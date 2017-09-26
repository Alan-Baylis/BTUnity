using BehaviorTree.Nodes;
using UnityEngine;

namespace BehaviorTree.MonoNodes
{
    public class MonoBreakpointNode : MonoBehaviour
    {
        public BreakpointNode node;

        [SerializeField]
        private Node.NodeState _nodeState;

        private void Awake()
        {
            this.node = new BreakpointNode(null, this._nodeState);
        }
    }
}