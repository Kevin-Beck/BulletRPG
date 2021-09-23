using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [RequireComponent(typeof(Collider))]
    public class BasicInventoryManager : MonoBehaviour
    {
        [SerializeField] InventoryObject inventory;

        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<LootableItem>();
            if (item)
            {
                if (item.amount < 1)
                {
                    Debug.LogWarning($"Amount set to 0 when adding {item.itemObject}");
                }
                if (inventory.AddItem(new Item(item.itemObject), item.amount))
                {
                    Destroy(other.gameObject);
                }

            }
        }
    }
}
