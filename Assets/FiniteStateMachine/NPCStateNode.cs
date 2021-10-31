using BulletRPG.Characters.NPC;
using UnityEngine;

namespace Patterns
{
    public class NPCStateNode : Node
    {
        protected NPC myNPC;
        private BehaviorState myState;

        public BehaviorState StateType { get { return myState; } }
        public NPCStateNode(FiniteStateMachine fsm, BehaviorState state, NPC npc) : base(fsm)
        {
            myNPC = npc;
            myState = state;
        }
        public delegate void StateDelegate();

        public StateDelegate OnEnterDelegate { get; set; } = null;
        public StateDelegate OnExitDelegate { get; set; } = null;
        public StateDelegate OnUpdateDelegate { get; set; } = null;
        public StateDelegate OnFixedUpdateDelegate { get; set; } = null;

        public override void Enter()
        {
            OnEnterDelegate?.Invoke();
        }

        public override void Exit()
        {
            OnExitDelegate?.Invoke();
        }

        public override void Update()
        {
            OnUpdateDelegate?.Invoke();
        }

        public override void FixedUpdate()
        {
            OnFixedUpdateDelegate?.Invoke();
        }
    }
}

