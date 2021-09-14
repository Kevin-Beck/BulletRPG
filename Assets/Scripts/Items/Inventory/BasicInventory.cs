using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public class BasicInventory : MonoBehaviour, IInventory
    {
        [SerializeField] int inventorySize;
        [SerializeField] List<Gear> inventory;
        [SerializeField] EquipmentManager equipmentManager;
        private void Awake()
        {
            equipmentManager = GetComponent<EquipmentManager>();
        }
        public bool Add(Gear gear)
        {
            if(inventorySize > inventory.Count)
            {
                inventory.Add(gear);
                return true;
            }
            return false;
        }

        public void Remove(Gear gear)
        {
            if (inventory.Contains(gear))
            {
                inventory.Remove(gear);
            }
        }

        public void EquipRandom()
        {
            if(inventory.Count > 0)
            {
                int random = Random.Range(0, inventory.Count);
                equipmentManager.Equip(inventory[random]);
                inventory.RemoveAt(random);
            }
            else
            {
                Debug.Log("Nothing in inventory");
            }
        }
    }
}
