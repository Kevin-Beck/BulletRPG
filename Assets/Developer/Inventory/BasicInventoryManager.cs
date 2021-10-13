using BulletRPG.Gear.Weapons.RangedWeapons;
using UnityEngine;

namespace BulletRPG.Gear
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
            var lootableItem = other.GetComponent<LootableItem>();
            if (lootableItem)
            {
                if (lootableItem.amount < 1)
                {
                    Debug.LogWarning($"Amount set to 0 when adding {lootableItem.itemObject}");
                }
                if(lootableItem.setItem.Id == -1)
                {
                    // roll a new gear TODO 
                    // This should be able to just return a specific type of any object
                    // item.GetObject() can maybe return us any type of object based on the itemObject
                    // a base function that is overwritten by the sub-classes
                    var gearObject = lootableItem.itemObject as GearObject;
                    if (gearObject != null && gearObject.gearSlot == GearSlot.Wand)
                    {
                        if (inventory.AddItem(new RangedWeapon((RangedWeaponObject)lootableItem.itemObject), 1))
                        {
                            Destroy(other.gameObject);
                        }
                    }
                    else if (inventory.AddItem(new Item(lootableItem.itemObject), lootableItem.amount))
                    {
                        Destroy(other.gameObject);
                    }
                }
                else
                {
                    // gear is set already
                    if (inventory.AddItem(lootableItem.setItem, lootableItem.amount))
                    {
                        Destroy(other.gameObject);
                    }
                }
            }
        }
        private void OnApplicationQuit()
        {
            inventory.Clear();
        }
    }
}
