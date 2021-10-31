using BulletRPG.Gear;
using UnityEngine;

namespace BulletRPG.Characters
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IHealth))]
    [RequireComponent(typeof(INPCMove))]
    public class Interactor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.gameObject.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(this);
            }
        }
    }
}

