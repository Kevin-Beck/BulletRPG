using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public class BasicInventoryManager : MonoBehaviour
    {
        [SerializeField] InventoryObject inventory;
        [SerializeField] int MaxInventorySize;
        public GameEvent inventoryChangedEvent;

        public bool AddToInventory(ItemObject item, int amount)
        {
            if (inventory.Container.Count >= MaxInventorySize )
                return false;
            
            if(amount < 1)
            {
                Debug.LogWarning($"Amount set to 0 when adding {item.ItemName}");
            }
            inventory.AddItem(item, amount);
            Debug.Log($"Added {item.ItemName}x{amount}");
            inventoryChangedEvent.Raise();
            return true;
        }
    }
}
