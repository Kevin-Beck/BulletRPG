using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BulletRPG.UI
{    public class StaticInterface : UserInterface
    {
        void Awake()
        {
            CreateSlots();
            CreateLinkToInventoryUpdateEvent();
        }
        private void Start()
        {
            UpdateDisplay();
        }
        public override void CreateLinkToInventoryUpdateEvent() {
            inventory.InventoryChanged += UpdateDisplay;
        }
        public override void CreateSlots()
        {
            InventorySlotButton[] buttons = GetComponentsInChildren<InventorySlotButton>();
            for (int i = 0; i < buttons.Length; i++)
            {
                SlotMap.Add(buttons[i], inventory.Container.inventorySlots[i]);
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

