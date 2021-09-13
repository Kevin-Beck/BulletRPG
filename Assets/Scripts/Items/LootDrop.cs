using BulletRPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public class LootDrop : Interactable
    { 
        public Gear myGear;
        public override void Interact(Interactor agent)
        {
            agent.GetComponent<IInventory>().Add(myGear);
            Destroy(gameObject);
        }
    }
}
