using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletRPG.Items;
namespace BulletRPG.UI
{
    public class DisplayInventory : MonoBehaviour
    {
        public InventoryObject inventory;
        public InventorySlotButton[] inventorySlotButtons;

        Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
        // Start is called before the first frame update
        void Start()
        {
            inventorySlotButtons = GetComponentsInChildren<InventorySlotButton>();
            UpdateDisplay();
        }
        public void UpdateDisplay()
        {
            for(int i = 0; i < inventory.Container.Count; i++)
            {
                inventorySlotButtons[i].SetInventorySlot(inventory.Container[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

