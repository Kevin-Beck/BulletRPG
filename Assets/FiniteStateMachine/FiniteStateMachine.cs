using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    /// <summary>
    /// https://faramira.com/finite-state-machine-using-csharp-delegates-in-unity/
    /// </summary>
    public class FiniteStateMachine
    {
        protected Dictionary<int, Node> m_states = new Dictionary<int, Node>();
        protected Node m_currentState;
        public FiniteStateMachine()
        {

        }
        public void Update()
        {
            if(m_currentState != null)
            {
                m_currentState.Update();
            }
        }
        public void FixedUpdate()
        {
            if(m_currentState != null)
            {
                m_currentState.FixedUpdate();
            }
        }
        public void Add(int key, Node state)
        {
            m_states.Add(key, state);
        }
        public Node GetState(int key)
        {
            return m_states[key];
        }
        public void SetCurrentState(Node state)
        {
            if(m_currentState != null)
            {
                m_currentState.Exit();
            }

            m_currentState = state;

            if(m_currentState != null)
            {
                m_currentState.Enter();
            }
        }
    }
    public class Node
    {
        protected FiniteStateMachine m_finiteStateMachine;
        public Node(FiniteStateMachine finiteStateMachine)
        {
            m_finiteStateMachine = finiteStateMachine;
        }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Enter() { }
        public virtual void Exit() { }
    }
}

