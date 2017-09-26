using UnityEngine;

namespace BehaviorTree.Nodes
{
    public class MonoNode : MonoBehaviour
    {
        public Node node;

        [Header("Debug values")]
        [SerializeField]
        private Node.NodeState _currentState;

        public void Refresh()
        {
            this._currentState = this.node.CurrentState;
        }
    }
}