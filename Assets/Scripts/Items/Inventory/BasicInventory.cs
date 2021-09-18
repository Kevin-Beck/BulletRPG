using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public class BasicInventory : MonoBehaviour, IInventory
    {
        [SerializeField] GameEvent PlayerInventoryChanged;
        [SerializeField] public int inventorySize;
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
            if (inventory.Contains(gear))
            {
                inventory.Remove(gear);
                equipmentManager.Equip(gear);
                PlayerInventoryChanged.Raise();
                Debug.Log("BasicInventory Equipped Object");
            }

        }

        public List<Gear> GetInventoryGearList()
        {
            return inventory;
        }
        public int GetMaxCapacity()
        {
            return inventorySize;
        }
    }
}
