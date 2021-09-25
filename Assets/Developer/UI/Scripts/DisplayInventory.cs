using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletRPG.Items;
namespace BulletRPG.UI
{
    public class DisplayInventory : MonoBehaviour
    {
        public InventoryObject inventory;
        Dictionary<InventorySlotUIHelper, InventorySlot> itemsDisplayed = new Dictionary<InventorySlotUIHelper, InventorySlot>();
        // Start is called before the first frame update
        void Awake()
        {
            inventory.PlayerInventoryChanged += UpdateDisplay;
        }
        private void Start()
        {
            InventorySlotUIHelper[] buttons = GetComponentsInChildren<InventorySlotUIHelper>();
            for (int i = 0; i < buttons.Length; i++)
            {
                itemsDisplayed.Add(buttons[i], inventory.Container.Items[i]);
            }
            UpdateDisplay();
        }
        public void UpdateDisplay()
        {
            foreach(KeyValuePair<InventorySlotUIHelper, InventorySlot> pair in itemsDisplayed)
            {
                if(pair.Value.ID >= 0)
                {
                    var referencedObject = inventory.database.GetItem[pair.Value.item.Id];
                    pair.Key.SetToolTipEnabled(true);
                    pair.Key.SetIconSpriteAndColor(referencedObject.sprite, referencedObject.Color);
                    pair.Key.SetCounterText(pair.Value.amount.ToString("N0"));
                    pair.Key.SetToolTipTitleAndText(referencedObject.ItemName, referencedObject.ItemDescription);
                    Debug.Log("KeyPair Description:" + pair.Value.item.Description);
                    Debug.Log("Ref SO  Description:" + referencedObject.ItemDescription);

                }
                else
                {
                    pair.Key.SetIconSpriteAndColor(null, Color.clear);
                    pair.Key.SetCounterText("");
                    pair.Key.SetToolTipTitleAndText("", "");
                    pair.Key.SetToolTipEnabled(false);
                }
            }
        }
    }
}

