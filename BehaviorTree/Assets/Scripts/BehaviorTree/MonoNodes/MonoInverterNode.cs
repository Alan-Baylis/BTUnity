using BehaviorTree.Nodes;
using UnityEngine;

namespace BehaviorTree.MonoNodes
{
    public class MonoInverterNode : MonoBehaviour
    {
        public InverterNode node;

        [SerializeField]
        private Node _nodeToInvert;

        private void Awake()
        {
            this.node = new InverterNode(_nodeToInvert);
        }
    }
}