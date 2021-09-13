using BulletRPG.Characters;
using BulletRPG.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items.Powerups
{
    public class ChangeSpeed : Interactable
    {
        [Range(0, 5)]
        public float speedMultiplier;
        [Range(0, 30)]
        public float timeUntilReset;

        public override void Interact(Interactor agent)
        {
            agent.GetComponent<IMove>().SetSpeedMultiplier(speedMultiplier, timeUntilReset);
            Destroy(gameObject);
        }
    }
}
