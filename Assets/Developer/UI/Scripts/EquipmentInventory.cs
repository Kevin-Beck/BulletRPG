using BulletRPG.Gear;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BulletRPG.UI.Inventory
{
    public class EquipmentInventory : InventoryUserInterface
    {
        GameObject player;
        public GameObject equipmentSlotPrefab;
        Transform contentPanel;
        private void Awake()
        {
            // Link the content panel to create the UI slots.
            contentPanel = Utilities.RecursiveFindChild(transform, "Content");
            if(contentPanel == null)
            {
                Debug.LogWarning("EquipmentInterface was unable to locate its Content Panel");
            }
            CreateSlots();
        }

        public override void CreateSlots()
        {
            for (int i = 0; i < inventory.GetSlots.Length; i++)
            {
                var slot = Instantiate(equipmentSlotPrefab, contentPanel);
                var text = Utilities.RecursiveFindChild(slot.transform, "SlotName");
                GearSlot gearSlot = inventory.GetSlots[i].AllowedGearTypes.ElementAt(0);
                text.GetComponent<TextMeshProUGUI>().text = gearSlot.ToString();
            }

            // After inventory buttons have been made, initialize each slot with the allowed gear for that slot                
            InventorySlotButton[] buttons = GetComponentsInChildren<InventorySlotButton>();
            for (int i = 0; i < buttons.Length; i++)
            {
                LinkButtonToInventorySlot(buttons[i], inventory.GetSlots[i]);

                // Add Drag and drop functionality to buttons by adding events to the game object
                var buttonGameObject = buttons[i].gameObject;
                Utilities.AddEvent(buttonGameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonGameObject); });
                Utilities.AddEvent(buttonGameObject, EventTriggerType.PointerExit, delegate { OnExit(buttonGameObject); });
                Utilities.AddEvent(buttonGameObject, EventTriggerType.BeginDrag, delegate { OnDragStart(buttonGameObject); });
                Utilities.AddEvent(buttonGameObject, EventTriggerType.EndDrag, delegate { OnDragEnd(buttonGameObject); });
                Utilities.AddEvent(buttonGameObject, EventTriggerType.Drag, delegate { OnDrag(buttonGameObject); });
            }
        }
    }
}

