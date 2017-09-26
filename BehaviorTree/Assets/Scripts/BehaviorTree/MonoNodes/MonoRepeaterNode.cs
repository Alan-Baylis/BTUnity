using BehaviorTree.Nodes;
using UnityEngine;

namespace BehaviorTree.MonoNodes
{
    public class MonoRepeaterNode : MonoBehaviour
    {
        public RepeaterNode node;

        [SerializeField]
        private Node.NodeState _waitForState;
        
        [Tooltip("Use -1 for infinite")]
        [SerializeField]
        private int _numberOfRepeats;
        
        [SerializeField]
        private Node _node;

        private void Awake()
        {
            this.node = new RepeaterNode();
        }
    }
}