using System.Collections.Generic;
using UnityEngine;
using BulletRPG.Items;
using UnityEngine.UI;
using BulletRPG.Gear;

namespace BulletRPG.UI.Inventory
{
    public abstract class InventoryUserInterface : MonoBehaviour
    {
        public DragDropElement dragDropIcon = DragDropElement.Instance;

        public InventoryObject inventory;
        protected Dictionary<InventorySlotButton, InventorySlot> SlotMap = new Dictionary<InventorySlotButton, InventorySlot>();

        public abstract void CreateSlots();

        public void LinkButtonToInventorySlot(InventorySlotButton button, InventorySlot slot)
        {
            slot.OnAfterUpdate += delegate {
                button.UpdateSlotUI(slot.gear?.Id < 0 ? null : inventory.database.gearObjects[slot.gear.Id]);
            };
            button.SetSlot(slot);
            SlotMap.Add(button, slot);
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
                    rect.sizeDelta = new Vector2(75, 75);
                    dragMouseIcon.transform.SetParent(transform.root);

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
                    var fromParent = dragDropIcon.fromButton.inventoryGroupParent;
                    var toParent = endDragButton.inventoryGroupParent;
                    var fromSlot = SlotMap[dragDropIcon.fromButton];
                    var toSlot = toParent.SlotMap[endDragButton];

                    if (toSlot.CanPlaceInSlot(fromSlot.gear))
                    {
                        if(fromSlot.CanPlaceInSlot(toSlot.gear) || toSlot.gear.Id < 0)
                        {
                            if (fromParent == toParent)
                            {
                                Debug.Log("Switching in same inventory");
                                inventory.MoveItem(fromSlot, toSlot);
                            }
                            else
                            {
                                Debug.Log("Switching two different inventories");
                                var tempGear = fromSlot.gear;
                                var tempAmount = fromSlot.amount;
                                fromParent.inventory.SetInventorySlot(fromSlot, toSlot.gear, toSlot.amount);
                                toParent.inventory.SetInventorySlot(toSlot, tempGear, tempAmount);
                            }
                        }
                        else
                        {
                            Debug.Log($"Sending slot cannot hold gearType: {toSlot.gear.gearType}");
                        }
                    }
                    else
                    {
                        Debug.Log($"Receiving slot cannot hold gearType: {fromSlot.gear.gearType}");
                    }
                }
            }
            else if(dragDropIcon.icon != null)
            {
                Debug.Log("Dropping Item");
                var fromSlot = SlotMap[dragDropIcon.fromButton];
                var droppedObject = Instantiate(inventory.database.gearObjects[fromSlot.gear.Id].LootObject, Utilities.MouseOnPlane(), Quaternion.identity);
                var lootableData = droppedObject.GetComponent<LootableItem>();
                lootableData.amount = fromSlot.amount;
                lootableData.setGear = fromSlot.gear;

                inventory.RemoveItem(fromSlot.gear, fromSlot.amount);
            }

            Destroy(dragDropIcon.icon);
            dragDropIcon.icon = null;
            dragDropIcon.fromButton = null;
        }
        public void SplitStack(GameObject button)
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                var uiSlotObject = button.GetComponent<InventorySlotButton>();
                inventory.SplitStack(SlotMap[uiSlotObject]);
            }
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


