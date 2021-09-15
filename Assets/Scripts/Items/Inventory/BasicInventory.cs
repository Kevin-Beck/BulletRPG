using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public class BasicInventory : MonoBehaviour, IInventory
    {
        [SerializeField] GameEvent PlayerInventoryChanged;
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
                PlayerInventoryChanged.Raise();
                return true;
            }
            return false;
        }

        public void Remove(Gear gear)
        {
            if (inventory.Contains(gear))
            {
                inventory.Remove(gear);
                PlayerInventoryChanged.Raise();
            }
        }

        public void Equip(Gear gear)
        {
            if(inventory.Count > 0)
            {
                equipmentManager.Equip(gear);
                inventory.Remove(gear);
                PlayerInventoryChanged.Raise();
            }
            else
            {
                Debug.Log("Nothing in inventory");
            }
        }

        public List<Gear> GetInventoryGearList()
        {
            return inventory;
        }
    }
}
