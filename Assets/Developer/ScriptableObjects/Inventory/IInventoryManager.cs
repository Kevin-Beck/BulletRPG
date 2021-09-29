using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{    public interface IInventoryManager
    {
        public bool AddToInventory(ItemObject itemObject);
        public bool RemoveFromInventory(ItemObject itemObject);
        public List<ItemObject> GetInventoryItemObjectList();
        public bool Equip(GearObject gear);
        public bool Unequip(GearObject gear);
        public bool Drop(ItemObject itemObject);

        public int GetMaxCapacity();
        public void SetMaxCapacity(int maxCapacity);
    }
}
