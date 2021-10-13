using System.Collections.Generic;
using UnityEngine;
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
                button.UpdateSlotUI(slot.item?.Id < 0 ? null : inventory.database.itemObjects[slot.item.Id]);
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
                var gearInSlot = SlotMap[dragDropIcon.fromButton].item;
                if (gearInSlot != null && gearInSlot.Id > -1)
                {
                    Debug.Log("Drag Started");
                    var dragMouseIcon = new GameObject();
                    var rect = dragMouseIcon.AddComponent<RectTransform>();
                    rect.sizeDelta = new Vector2(75, 75);
                    dragMouseIcon.transform.SetParent(transform.root);

                    var image = dragMouseIcon.AddComponent<Image>();
                    image.sprite = inventory.database.GetItemObject[gearInSlot.Id].sprite;
                    image.color = inventory.database.GetItemObject[gearInSlot.Id].color;
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

                    var fromGear = fromSlot.item as Gear.Gear;
                    var toGear = toSlot.item as Gear.Gear;
                    if (toSlot.CanPlaceInSlot(fromGear))
                    {
                        if(fromSlot.CanPlaceInSlot(toGear) || toSlot.item.Id < 0)
                        {
                            if (fromParent == toParent)
                            {
                                Debug.Log("Switching in same inventory");
                                inventory.MoveItem(fromSlot, toSlot);
                            }
                            else
                            {
                                Debug.Log("Switching two different inventories");
                                var tempItem = fromSlot.item;
                                var tempAmount = fromSlot.amount;
                                fromParent.inventory.SetInventorySlot(fromSlot, toSlot.item, toSlot.amount);
                                toParent.inventory.SetInventorySlot(toSlot, tempItem, tempAmount);
                            }
                        }
                        else
                        {
                            Debug.Log($"Sending slot cannot hold gearType: {toGear.gearSlot}");
                        }
                    }
                    else
                    {
                        Debug.Log($"Receiving slot cannot hold gearType: {fromGear.gearSlot}");
                    }
                }
            }
            else if(dragDropIcon.icon != null)
            {
                Debug.Log("Dropping Item");
                var fromSlot = SlotMap[dragDropIcon.fromButton];
                var droppedObject = Instantiate(inventory.database.itemObjects[fromSlot.item.Id].lootObject, Utilities.MouseOnPlane(), Quaternion.identity);
                var lootableData = droppedObject.GetComponent<LootableItem>();
                lootableData.amount = fromSlot.amount;
                lootableData.setItem = fromSlot.item;

                inventory.RemoveItem(fromSlot.item, fromSlot.amount);
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


