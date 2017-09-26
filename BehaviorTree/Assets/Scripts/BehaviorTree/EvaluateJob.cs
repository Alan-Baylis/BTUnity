using System;
using System.Diagnostics;
using System.Threading;
using BehaviorTree.Nodes;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace BehaviorTree
{
    public class EvaluateJob : MonoBehaviour
    {
        [SerializeField]
        private Node _rootNode;

        private readonly object _lockObject = new object();

        [SerializeField]
        private bool _isRunning;

        [SerializeField]
        private bool _isStarted;

        public delegate void EvaluateDone();

        public event EvaluateDone OnEvaluateDone;

        private Thread _workThread;

        private void Update()
        {
            if (this._isRunning || !this._isStarted) return;
            lock (this._lockObject)
            {
                this._isStarted = false;
            }
            if (this.OnEvaluateDone != null)
                this.OnEvaluateDone.Invoke();
        }

        public void StartJob(Node rootnode)
        {
            lock (this._lockObject)
            {
                if (this._isRunning)
                    throw new Exception("Started evaluation while it was still in progress");
                this._isRunning = true;
                this._isStarted = true;
            }
            this._rootNode = rootnode;
            _workThread = new Thread(DoWork);
            _workThread.Start();
        }

        private void DoWork()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            this._rootNode.Evaluate();
            sw.Stop();
            Debug.Log(String.Format("Evaluate job took : {0}ms", sw.ElapsedMilliseconds));
            lock (this._lockObject)
            {
                this._isRunning = false;
            }
        }

        private void OnDestroy()
        {
            lock (this._lockObject)
            {
                if (this._isRunning)
                    this._workThread.Abort();
            }
        }
    }
}