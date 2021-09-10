using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BulletRPG.Characters.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Move : MonoBehaviour, IMove
    {
        private PlayerInputActions playerInputActions;
        public LayerMask playerLookLayer;
        [HideInInspector] public NavMeshAgent navMeshAgent;
        private Animator animator;
        public string animationBlendTreeParameterName;
        public string animationForwardAxisName;
        public string animationRightAxisName;


        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updateRotation = false;
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
        }
        private void Start()
        {
            animator.SetBool(animationBlendTreeParameterName, true);
        }

        private void Update()
        {
            // Look Direction
            Ray ray = Camera.main.ScreenPointToRay(playerInputActions.Player.MousePosition.ReadValue<Vector2>());
            if (Physics.Raycast(ray, out RaycastHit hit, 100, playerLookLayer))
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
            else
            {
#if UNITY_EDITOR
                if (!(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y))
                    Debug.LogWarning("Raycast for player look direction not hitting anything on PlayerLookLayer");
#endif
            }

            // Get input from player, create a vector3
            Vector2 inputDirection = playerInputActions.Player.Movement.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;

            // Set destination
            navMeshAgent.destination = transform.position + moveDirection * 2f;

            if (animator != null)
            {
                // Adjust vector based on current rotation to create correct animation
                moveDirection = transform.InverseTransformDirection(moveDirection);
                animator.SetFloat(animationRightAxisName, moveDirection.x);
                animator.SetFloat(animationForwardAxisName, moveDirection.z);
            }
            else
            {
                Debug.LogWarning($"Animator is null on {name}");
            }
        }
        //TODO Below is untested
        public void SetSpeedMultiplier(float speedMultiplier, float revertAfterSeconds)
        {
            var currentSpeed = navMeshAgent.speed;
            var newSpeed = navMeshAgent.speed * speedMultiplier;
            navMeshAgent.speed = newSpeed;
            StartCoroutine(SetSpeed(currentSpeed, revertAfterSeconds));
        }

        IEnumerator SetSpeed(float speedValue, float timeDelay)
        {
            yield return new WaitForSeconds(timeDelay);
            navMeshAgent.speed = speedValue;
        }
    }
}
