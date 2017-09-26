using System;
using System.Collections.Generic;
using BehaviorTree.Nodes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BehaviorTree
{
    public class MathTree : MonoBehaviour
    {
        [SerializeField]
        public Color evaluatingColor;
        [SerializeField]
        public Color succeededColor;
        [SerializeField]
        public Color failedColor;

        private List<Node> _nodes;
        public SelectorNode rootNode;
        public ActionNode node2A;
        public InverterNode node2B;
        public ActionNode node2C;
        public ActionNode node3;
        public GameObject rootNodeBox;
        public GameObject node2ABox;
        public GameObject node2BBox;
        public GameObject node2CBox;
        public GameObject node3Box;
        public int targetValue = 20;
        private int _currentValue = 0;

        [SerializeField]
        private Text m_valueLabel;

        private void Awake()
        {
            foreach (Transform t in this.transform)
            {
                Node n = t.GetComponent<Node>();
                if (n == null) continue;
                this._nodes.Add(n);
            }
        }
        
        /* We instantiate our nodes from the bottom up, and assign the children in that order */
        void Start()
        {
            /** The deepest-level node is Node 3, which has no children. */
            node3 = new ActionNode(NotEqualToTarget);
            /** Next up, we create the level 2 nodes. */
            node2A = new ActionNode(AddTen);
            /** Node 2B is a selector which has node 3 as a child, so we'll pass node 3 to the constructor */
            node2B = new InverterNode(node3);
            node2C = new ActionNode(AddTen);
            /** Lastly, we have our root node. First, we prepare our list of children nodes to pass in */
            List<Node> rootChildren = new List<Node> {node2A, node2B, node2C};
            /** Then we create our root node object and pass in the list */
            rootNode = new SelectorNode(rootChildren);
//            this._nodeObjects.Add(new KeyValuePair<Node, GameObject>(rootNode, rootNodeBox));
//            this._nodeObjects.Add(new KeyValuePair<Node, GameObject>(node2A, node2ABox));
//            this._nodeObjects.Add(new KeyValuePair<Node, GameObject>(node2B, node2BBox));
//            this._nodeObjects.Add(new KeyValuePair<Node, GameObject>(node2C, node2CBox));
//            this._nodeObjects.Add(new KeyValuePair<Node, GameObject>(node3, node3Box));            
            m_valueLabel.text = _currentValue.ToString();
            rootNode.Evaluate();
            UpdateBoxes();
        }

        private void UpdateBoxes()
        {
/** Update root node box */
            foreach (KeyValuePair<Node, GameObject> node in this._nodeObjects)
            {
                switch (node.Key.CurrentState)
                {
                    case Node.NodeState.Succes:
                        SetSucceeded(node.Value);
                        break;
                    case Node.NodeState.Failure:
                        SetFailed(node.Value);
                        break;
                    case Node.NodeState.Running:
                        SetEvaluating(node.Value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private Node.NodeState NotEqualToTarget()
        {
            return _currentValue != targetValue ? Node.NodeState.Succes : Node.NodeState.Failure;
        }

        private Node.NodeState AddTen()
        {
            _currentValue += 10;
            m_valueLabel.text = _currentValue.ToString();
            return _currentValue == targetValue ? Node.NodeState.Succes : Node.NodeState.Failure;
        }


        private void SetEvaluating(GameObject box)
        {
            box.GetComponent<Renderer>().material.color = evaluatingColor;
        }

        private void SetSucceeded(GameObject box)
        {
            box.GetComponent<Renderer>().material.color = succeededColor;
        }

        private void SetFailed(GameObject box)
        {
            box.GetComponent<Renderer>().material.color = failedColor;
        }

        public void Reset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}