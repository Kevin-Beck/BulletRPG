using BulletRPG.Characters;
using UnityEngine;

namespace BulletRPG.Gear
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        public virtual void Interact(Interactor agent) { }
        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }
}


