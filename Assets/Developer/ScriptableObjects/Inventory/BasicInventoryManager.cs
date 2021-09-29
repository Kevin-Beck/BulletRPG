using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [RequireComponent(typeof(Collider))]
    public class BasicInventoryManager : MonoBehaviour
    {
        [SerializeField] InventoryObject inventory;

        private void Awake()
        {
            inventory.database = Resources.Load<ItemDatabaseObject>("ItemDatabase");
        }
        private void OnTriggerEnter(Collider other)
        {
            var gear = other.GetComponent<LootableItem>();
            if (gear)
            {
                if (gear.amount < 1)
                {
                    Debug.LogWarning($"Amount set to 0 when adding {gear.gearObject}");
                }
                if (inventory.AddItem(new Gear(gear.gearObject), gear.amount))
                {
                    Destroy(other.gameObject);
                }

            }
        }
        private void OnApplicationQuit()
        {
            inventory.Container.inventorySlots = new InventorySlot[21];
        }
    }
}
