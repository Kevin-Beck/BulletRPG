using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public class GearEquipButton : MonoBehaviour
    {

        public Gear GearTiedToButton;
        IInventory inventory;
        EquipmentManager equipmentManager;

        void Start()
        {
            equipmentManager = GameObject.FindGameObjectWithTag("Player").GetComponent<EquipmentManager>();
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<IInventory>();
        }
        public void Equip()
        {
            Debug.Log("Sending Equip to Inventory with name: " + GearTiedToButton.ItemName);
            inventory.Equip(GearTiedToButton);
        }
        public void Unequip()
        {
            Debug.Log("Sending Unequip to Inventory with name: " + GearTiedToButton.ItemName);
            equipmentManager.Unequip(GearTiedToButton.Slot);            
        }
    }
}
