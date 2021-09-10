using BulletRPG.Items;
using UnityEngine;

namespace BulletRPG.Characters
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Interactor : MonoBehaviour
    {
        private IMove move;

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

