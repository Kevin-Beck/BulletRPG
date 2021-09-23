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
        void Awake()
        {
            inventorySlotButtons = GetComponentsInChildren<InventorySlotButton>();
            for(int i = 0; i < inventorySlotButtons.Length; i++)
            {
                inventorySlotButtons[i].SetItemDatabaseObject(inventory.database);
            }
        }
        private void Start()
        {
            UpdateDisplay();
        }
        public void UpdateDisplay()
        {
            for(int i = 0; i < inventory.InventoryMaxSize; i++)
            {
                if(i < inventory.Container.Items.Count)
                {
                    inventorySlotButtons[i].SetInventorySlot(inventory.Container.Items[i]);
                }
                else
                {
                    inventorySlotButtons[i].SetInventorySlot(null);
                }
            }
        }
    }
}

