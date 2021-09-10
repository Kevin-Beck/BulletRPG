using BulletRPG.Characters;
using UnityEngine;

namespace BulletRPG.Items
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        public virtual void Interact(Interactor agent) { }
    }
}


