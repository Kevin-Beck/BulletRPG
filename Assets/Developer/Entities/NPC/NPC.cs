using BulletRPG.Characters.Player;
using Patterns;
using UnityEngine;
using UnityEngine.AI;

namespace BulletRPG.Characters.NPC
{
    public class NPC : MonoBehaviour
    {
        public FiniteStateMachine behaviorStateMachine;
        [SerializeField] LookDirection lookDirection;
        Animator animator;

        private NPCHealth myHealth;
        private INPCMove myMove;        
        private INPCShoot myShoot;
        private NavMeshAgent navAgent;
        private Transform playerTransform;

        public Attribute[] attributes;
        CharacterStats baseStats;

        [Header("StateMachine Settings")]
        public float walkTime;
        public float shotsPerShoot;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
            myHealth = GetComponent<NPCHealth>();
            myMove = GetComponent<INPCMove>();
            myShoot = GetComponent<INPCShoot>();
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
            InvokeRepeating("TriggerAttack", walkTime, walkTime);
        }
        private void Update()
        {
            if (myHealth.IsDead)
            {
                return;
            }
            ProcessLookDirection();
            behaviorStateMachine.Update();
        }
        private void FixedUpdate()
        {
            behaviorStateMachine.FixedUpdate();
        }
        private void ProcessLookDirection()
        {
            if (lookDirection == LookDirection.Destination)
            {
                transform.LookAt(navAgent.destination);
            }
            else if (lookDirection == LookDirection.Player)
            {
                transform.LookAt(playerTransform);
            }
        }
        private void SetState(BehaviorState state)
        {
            behaviorStateMachine.SetCurrentState(behaviorStateMachine.GetState((int)state));
        }
        public void Die()
        {
            SetState(BehaviorState.Die);
        }

        private void TriggerAttack()
        {
            SetState(BehaviorState.Attack);
        }
        private void TriggerMove()
        {
            SetState(BehaviorState.Move);
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

            };
        }
        void Init_AttackState()
        {
            NPCStateNode state = (NPCStateNode)behaviorStateMachine.GetState((int)BehaviorState.Attack);

            state.OnEnterDelegate += delegate ()
            {
                Debug.Log("Entering Attack");
                myShoot.StartAttackAnimation();
            };
            state.OnExitDelegate += delegate ()
            {
                Debug.Log("Exiting Attack");
            };
            state.OnUpdateDelegate += delegate ()
            {
                
            };
        }
        public void AttackFinished()
        {
            animator.SetBool("shoot", false);
            TriggerMove();
        }
        void Init_DieState()
        {
            NPCStateNode state = (NPCStateNode)behaviorStateMachine.GetState((int)BehaviorState.Die);

            state.OnEnterDelegate += delegate ()
            {
            };
            state.OnExitDelegate += delegate ()
            {
            };
            state.OnUpdateDelegate += delegate ()
            {                
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
                Debug.Log("Entering Moving");
                animator.SetBool("moving", true);
            };
            state.OnExitDelegate += delegate ()
            {
                Debug.Log("Exiting Moving");
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

