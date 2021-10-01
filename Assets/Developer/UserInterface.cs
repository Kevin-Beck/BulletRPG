using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletRPG.Items;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BulletRPG.UI
{
    public abstract class UserInterface : MonoBehaviour
    {
        public DragDropElement dragDropIcon = new DragDropElement();

        public InventoryObject inventory;
        protected Dictionary<InventorySlotButton, InventorySlot> SlotMap = new Dictionary<InventorySlotButton, InventorySlot>();

        public abstract void CreateSlots();
        public abstract void CreateLinkToInventoryUpdateEvent();
        public void UpdateDisplay()
        {
            Debug.Log("UpdateDisplay Called");
            foreach (KeyValuePair<InventorySlotButton, InventorySlot> pair in SlotMap)
            {
                if (pair.Value.gear != null && pair.Value.gear.Id >= 0)
                {
                    var referencedGearObject = inventory.database.gearObjects[pair.Value.gear.Id];
                    pair.Key.SetToolTipEnabled(true);
                    pair.Key.SetIconSpriteAndColor(referencedGearObject.sprite, referencedGearObject.Color);
                    pair.Key.SetCounterText(pair.Value.amount.ToString("N0"));

                    string buffDescriptions = "";
                    foreach (GearBuff buff in pair.Value.gear.buffs)
                    {
                        buffDescriptions += buff.Stringify() + "\n";
                    }

                    pair.Key.SetToolTipTitleAndText(referencedGearObject.ItemName, $"{referencedGearObject.ItemDescription}\n\n{buffDescriptions}");
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
        protected void AddEvent(GameObject button, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = button.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }
        public void OnEnter(GameObject obj)
        {
            dragDropIcon.hoveringOverObject = obj;
        }
        public void OnExit(GameObject obj)
        {
            dragDropIcon.hoveringOverObject = null;
        }
        public void OnDragStart(GameObject obj)
        {
            dragDropIcon.fromButton = obj.GetComponent<InventorySlotButton>();
            if (dragDropIcon.fromButton)
            {
                // we have started to drag an inventory slot
                var gearInSlot = SlotMap[dragDropIcon.fromButton].gear;
                if (gearInSlot != null && gearInSlot.Id > -1)
                {
                    Debug.Log("Drag Started");
                    var dragMouseIcon = new GameObject();
                    var rect = dragMouseIcon.AddComponent<RectTransform>();
                    rect.sizeDelta = new Vector2(50, 50);
                    dragMouseIcon.transform.SetParent(transform);

                    var image = dragMouseIcon.AddComponent<Image>();
                    image.sprite = inventory.database.GetGearObject[gearInSlot.Id].sprite;
                    image.color = inventory.database.GetGearObject[gearInSlot.Id].Color;
                    image.raycastTarget = false;

                    dragDropIcon.icon = dragMouseIcon;
                }
                else
                {
                    Debug.Log("Nothing To Drag!");
                }
            }
            else
            {
                Debug.Log("No InventorySlotButtonFound");
            }
        }
        public void OnDragEnd(GameObject obj)
        {
            if(dragDropIcon.hoveringOverObject != null)
            {
                var endDragButton = dragDropIcon.hoveringOverObject.GetComponent<InventorySlotButton>();

                if (endDragButton != null && dragDropIcon.fromButton != null)
                {
                    Debug.Log("Ended Drag over an InventorySlotButton");
                    var fromParent = dragDropIcon.fromButton.inventoryGroupParent;
                    var toParent = endDragButton.inventoryGroupParent;
                    var fromSlot = SlotMap[dragDropIcon.fromButton];
                    var toSlot = SlotMap[endDragButton];
                    if (fromParent == toParent)
                    {
                        Debug.Log("Switching in same inventories");
                        inventory.MoveItem(fromSlot, toSlot);
                    }
                    else
                    {
                        Debug.Log("Switching two different inventories");
                        endDragButton.inventoryGroupParent.inventory.AddItem(fromSlot.gear, fromSlot.amount);
                        inventory.RemoveItem(fromSlot.gear, fromSlot.amount);
                    }
                }
            }
            else if(dragDropIcon.icon != null)
            {
                Debug.Log("Dropping Item");
                var fromSlot = SlotMap[dragDropIcon.fromButton];
                // TODO drop stacks of items, get the lootable element from the object and set the amount
                for (int i = 0; i < fromSlot.amount; i++)
                {
                    Instantiate(inventory.database.gearObjects[fromSlot.gear.Id].LootObject, Vector3.zero, Quaternion.identity);
                }
                inventory.RemoveItem(fromSlot.gear, fromSlot.amount);
            }
            else
            {
                Debug.Log("Default out of drag/drop");
            }

            Destroy(dragDropIcon.icon);
            dragDropIcon.icon = null;
            dragDropIcon.fromButton = null;
        }
        public void OnDrag(GameObject obj)
        {
            if (dragDropIcon.icon != null)
            {
                dragDropIcon.icon.GetComponent<RectTransform>().position = Input.mousePosition;
            }
        }
    }
}


