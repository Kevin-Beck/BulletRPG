using BulletRPG.Characters;
using UnityEngine;

namespace BulletRPG.Gear.Powerups
{
    public class ChangeSpeed : Interactable
    {
        [Range(0, 5)]
        public float speedMultiplier;
        [Range(0, 30)]
        public float timeUntilReset;

        public override void Interact(Interactor agent)
        {
            agent.GetComponent<INPCMove>().SetSpeedMultiplier(speedMultiplier, timeUntilReset);
            Destroy(gameObject);
        }
    }
}
