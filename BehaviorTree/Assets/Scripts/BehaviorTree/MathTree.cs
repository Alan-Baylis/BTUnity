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
        private EvaluateJob _evaluateJob;
        
        [SerializeField]
        public Color evaluatingColor;
        [SerializeField]
        public Color succeededColor;
        [SerializeField]
        public Color failedColor;

        [SerializeField]
        private List<MonoNode> _nodes;
        
        public int targetValue = 20;
        [SerializeField]
        private int _currentValue = 0;

        [SerializeField]
        private Text _valueLabel;

        private void Awake()
        {
            /* The deepest-level node is Node 3, which has no children. */
            var node3 = new ActionNode(NotEqualToTarget);
            /** Next up, we create the level 2 nodes. */
            var node2A = new ActionNode(AddTen);
            /** Node 2B is a selector which has node 3 as a child, so we'll pass node 3 to the constructor */
            //var node2B = new InverterNode(node3);
            var node2B = new RepeaterNode(node3, Node.NodeState.Failure, 500);
            
            var node2C = new ActionNode(AddTen);
            /** Lastly, we have our root node. First, we prepare our list of children nodes to pass in */
            List<Node> rootChildren = new List<Node> {node2A, node2B, node2C};
            /** Then we create our root node object and pass in the list */
            var rootNode = new SelectorNode(rootChildren);

            this._nodes[0].node = rootNode; 
            this._nodes[1].node = node2A; 
            this._nodes[2].node = node2B; 
            this._nodes[3].node = node3; 
            this._nodes[4].node = node2C; 
            this._evaluateJob.OnEvaluateDone += EvaluateJobOnOnEvaluateDone;
        }

        private void EvaluateJobOnOnEvaluateDone()
        {
            this.UpdateBoxes();
        }

        /* We instantiate our nodes from the bottom up, and assign the children in that order */
        private void Start()
        {
            _valueLabel.text = _currentValue.ToString();
            this.UpdateBoxes();
        }

        public void Evaluate()
        {
            this._evaluateJob.StartJob(this._nodes[0].node);
            this.UpdateBoxes();
        }
        
        private void UpdateBoxes()
        {
/** Update root node box */
            this._valueLabel.text = _currentValue.ToString();
            foreach (MonoNode monoNode in this._nodes)
            {
                monoNode.Refresh();
                switch (monoNode.node.CurrentState)
                {
                    case Node.NodeState.Succes:
                        SetSucceeded(monoNode.gameObject);
                        break;
                    case Node.NodeState.Failure:
                        SetFailed(monoNode.gameObject);
                        break;
                    case Node.NodeState.Running:
                        SetEvaluating(monoNode.gameObject);
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