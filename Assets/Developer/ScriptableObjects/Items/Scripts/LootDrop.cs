using BulletRPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public class LootDrop : Interactable
    { 
        public ItemObject myItemReference;
        public int amount = 1;
        public override void Interact(Interactor agent)
        {
            if (agent.GetComponent<BasicInventoryManager>().AddToInventory(myItemReference, amount))
            {
                Destroy(gameObject);
            }
        }
    }
}
