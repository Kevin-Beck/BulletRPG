using BulletRPG.Characters.Player;
using Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BulletRPG.Characters.NPC
{
    public class NPC : MonoBehaviour
    {
        public FiniteStateMachine behaviorStateMachine;
        [SerializeField] LookDirection lookDirection;


        private NPCHealth myHealth;
        private INPCMove myMove;        
        private NPCShoot myShoot;
        private NavMeshAgent navAgent;
        private Transform playerTransform;

        public Attribute[] attributes;
        CharacterStats baseStats;

        private void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
            myHealth = GetComponent<NPCHealth>();
            myMove = GetComponent<INPCMove>();
            myShoot = GetComponent<NPCShoot>();
            baseStats = GetComponentInChildren<CharacterStats>();
            playerTransform = Utilities.GetPlayerTransform();
        }
        private void Start()
        {
            behaviorStateMachine = new FiniteStateMachine();
            behaviorStateMachine.Add((int)BehaviorState.Idle, new NPCStateNode(behaviorStateMachine, BehaviorState.Idle, this));
            behaviorStateMachine.Add((int)BehaviorState.Move, new NPCStateNode(behaviorStateMachine, BehaviorState.Move, this));
            behaviorStateMachine.Add((int)BehaviorState.Attack, new NPCStateNode(behaviorStateMachine, BehaviorState.Attack, this));
            behaviorStateMachine.Add((int)BehaviorState.Damaged, new NPCStateNode(behaviorStateMachine, BehaviorState.Damaged, this));
            behaviorStateMachine.Add((int)BehaviorState.Die, new NPCStateNode(behaviorStateMachine, BehaviorState.Die, this));

            Init_IdleState();
            Init_AttackState();
            Init_DieState();
            Init_DamageState();
            Init_MoveState(myMove);

            behaviorStateMachine.SetCurrentState(behaviorStateMachine.GetState((int)BehaviorState.Move));
        }
        private void Update()
        {
            behaviorStateMachine.Update();
            if(lookDirection == LookDirection.Destination)
            {
                transform.LookAt(navAgent.destination);
            }else if(lookDirection == LookDirection.Player)
            {
                transform.LookAt(playerTransform);
            }
        }
        private void FixedUpdate()
        {
            behaviorStateMachine.FixedUpdate();
        }
        public void Die()
        {
            SetState(BehaviorState.Die);
        }
        // Helper function to set the state
        private void SetState(BehaviorState state)
        {
            behaviorStateMachine.SetCurrentState(behaviorStateMachine.GetState((int)state));
        }

        void Init_IdleState()
        {
            NPCStateNode state = (NPCStateNode)behaviorStateMachine.GetState((int)BehaviorState.Idle);

            // Add a text message to the OnEnter and OnExit delegates.
            state.OnEnterDelegate += delegate ()
            {
                Debug.Log("OnEnter - IDLE");
            };
            state.OnExitDelegate += delegate ()
            {
                Debug.Log("OnExit - IDLE");
            };

            state.OnUpdateDelegate += delegate ()
            {
                //Debug.Log("OnUpdate - IDLE");
                if (Input.GetKeyDown("c"))
                {
                    SetState(BehaviorState.Move);
                }
                else if (Input.GetKeyDown("d"))
                {
                    SetState(BehaviorState.Damaged);
                }
                else if (Input.GetKeyDown("a"))
                {
                    SetState(BehaviorState.Attack);
                }
            };
        }
        void Init_AttackState()
        {
            NPCStateNode state = (NPCStateNode)behaviorStateMachine.GetState((int)BehaviorState.Attack);

            state.OnEnterDelegate += delegate ()
            {

            };
            state.OnExitDelegate += delegate ()
            {
                Debug.Log("OnExit - ATTACK");
            };

            state.OnUpdateDelegate += delegate ()
            {
                //Debug.Log("OnUpdate - ATTACK");
                if (Input.GetKeyDown("c"))
                {
                    SetState(BehaviorState.Move);
                }
                else if (Input.GetKeyDown("d"))
                {
                    SetState(BehaviorState.Damaged);
                }
            };
        }
        void Init_DieState()
        {
            NPCStateNode state = (NPCStateNode)behaviorStateMachine.GetState((int)BehaviorState.Die);

            // Add a text message to the OnEnter and OnExit delegates.
            state.OnEnterDelegate += delegate ()
            {
                Debug.Log("OnEnter - DIE");
            };
            state.OnExitDelegate += delegate ()
            {
                Debug.Log("OnExit - DIE");
            };

            state.OnUpdateDelegate += delegate ()
            {
                //Debug.Log("OnUpdate - DIE");
            };
        }

        void Init_DamageState()
        {
            NPCStateNode state = (NPCStateNode)behaviorStateMachine.GetState((int)BehaviorState.Damaged);

            // Add a text message to the OnEnter and OnExit delegates.
            state.OnEnterDelegate += delegate ()
            {
                Debug.Log("OnEnter - DAMAGE");
            };
            state.OnExitDelegate += delegate ()
            {
                Debug.Log("OnExit - DAMAGE");
            };

            state.OnUpdateDelegate += delegate ()
            {
                
            };
        }
        void Init_MoveState(INPCMove movement)
        {
            NPCStateNode state = (NPCStateNode)behaviorStateMachine.GetState((int)BehaviorState.Move);

            // Add a text message to the OnEnter and OnExit delegates.
            state.OnEnterDelegate += delegate ()
            {
                Debug.Log("OnEnter - Move");
            };
            state.OnExitDelegate += delegate ()
            {
                myMove.Stop();
            };

            state.OnUpdateDelegate += delegate ()
            {
                myMove.Move();
            };
        }
    }
    public enum LookDirection
    {
        Destination,
        Player
    }
    public enum BehaviorState
    {
        Idle,
        Move,
        Attack,
        Damaged,
        Die,
    }
}

