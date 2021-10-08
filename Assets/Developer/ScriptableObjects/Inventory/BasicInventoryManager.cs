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
            inventory.database = Resources.Load<GearDatabaseObject>("ItemDatabase");
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
                if(gear.setGear.Id == -1)
                {
                    // roll a new gear
                    if (inventory.AddItem(new Gear(gear.gearObject), gear.amount))
                    {
                        Destroy(other.gameObject);
                    }
                }
                else
                {
                    // gear is set already
                    if(inventory.AddItem(gear.setGear, gear.amount))
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
