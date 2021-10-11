using BulletRPG.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BulletRPG.UI.Inventory
{    public class StaticInventory : InventoryUserInterface
    {
        void Awake()
        {
            CreateSlots();
        }

        public override void CreateSlots()
        {
            InventorySlotButton[] buttons = GetComponentsInChildren<InventorySlotButton>();
            for (int i = 0; i < buttons.Length; i++)
            {
                LinkButtonToInventorySlot(buttons[i], inventory.GetSlots[i]);

                // Button Config for drag and drop functionality
                var buttonGameObject = buttons[i].gameObject;
                Utilities.AddEvent(buttonGameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonGameObject); });
                Utilities.AddEvent(buttonGameObject, EventTriggerType.PointerExit, delegate { OnExit(buttonGameObject); });
                Utilities.AddEvent(buttonGameObject, EventTriggerType.BeginDrag, delegate { OnDragStart(buttonGameObject); });
                Utilities.AddEvent(buttonGameObject, EventTriggerType.EndDrag, delegate { OnDragEnd(buttonGameObject); });
                Utilities.AddEvent(buttonGameObject, EventTriggerType.Drag, delegate { OnDrag(buttonGameObject); });
                Utilities.AddEvent(buttonGameObject, EventTriggerType.PointerClick, delegate { SplitStack(buttonGameObject); });
            }
        }
    }
}

