using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletRPG.Items;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BulletRPG.UI
{
    public class DisplayInventory : MonoBehaviour
    {
        public MouseItem mouseItem = new MouseItem();

        public InventoryObject inventory;
        Dictionary<InventorySlotUIHelper, InventorySlot> slotDictionary = new Dictionary<InventorySlotUIHelper, InventorySlot>();
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
                slotDictionary.Add(buttons[i], inventory.Container.inventorySlots[i]);
                var buttonGameObject = buttons[i].gameObject;
                AddEvent(buttonGameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonGameObject); });
                AddEvent(buttonGameObject, EventTriggerType.PointerExit, delegate { OnExit(buttonGameObject); });
                AddEvent(buttonGameObject, EventTriggerType.BeginDrag, delegate { OnDragStart(buttonGameObject); });
                AddEvent(buttonGameObject, EventTriggerType.EndDrag, delegate { OnDragEnd(buttonGameObject); });
                AddEvent(buttonGameObject, EventTriggerType.Drag, delegate { OnDrag(buttonGameObject); });

            }
            UpdateDisplay();
        }
        public void UpdateDisplay()
        {
            foreach(KeyValuePair<InventorySlotUIHelper, InventorySlot> pair in slotDictionary)
            {
                if(pair.Value.ID >= 0)
                {
                    var referencedGearObject = inventory.database.gearObjects[pair.Value.ID];
                    pair.Key.SetToolTipEnabled(true);
                    pair.Key.SetIconSpriteAndColor(referencedGearObject.sprite, referencedGearObject.Color);
                    pair.Key.SetCounterText(pair.Value.amount.ToString("N0"));

                    string buffDescriptions = "";
                    foreach(GearBuff buff in pair.Value.gear.buffs)
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
        private void AddEvent(GameObject button, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = button.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }
        public void OnEnter(GameObject obj)
        {
            mouseItem.hoverObject = obj;
            if (slotDictionary.ContainsKey(obj.GetComponent<InventorySlotUIHelper>()))
            {
                mouseItem.hoverInventorySlot = slotDictionary[obj.GetComponent<InventorySlotUIHelper>()];
            }
        }
        public void OnExit(GameObject obj)
        {
            mouseItem.hoverObject = null;
            mouseItem.hoverInventorySlot = null;
        }
        public void OnDragStart(GameObject obj)
        {
            Debug.Log("Drag Started");
            var newMouse = new GameObject();
            var rect = newMouse.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(50, 50);
            newMouse.transform.SetParent(transform);

            var inventorySlot = slotDictionary[obj.GetComponent<InventorySlotUIHelper>()];
            if(inventorySlot.ID >=0)
            {
                var image = newMouse.AddComponent<Image>();
                image.sprite = inventory.database.GetGearObject[inventorySlot.ID].sprite;
                image.color = inventory.database.GetGearObject[inventorySlot.ID].Color;
                image.raycastTarget = false;
            }

            mouseItem.obj = newMouse;
            mouseItem.fromSlot = slotDictionary[obj.GetComponent<InventorySlotUIHelper>()];
        }
        public void OnDragEnd(GameObject obj)
        {
            if (mouseItem.hoverObject)
            {
                Debug.Log("Switching");
                inventory.MoveItem(mouseItem.fromSlot, mouseItem.hoverInventorySlot);
            }
            else
            {
                inventory.RemoveItem(slotDictionary[obj.GetComponent<InventorySlotUIHelper>()].gear);
            }
            Destroy(mouseItem.obj);
            mouseItem.fromSlot = null;
        }
        public void OnDrag(GameObject obj)
        {
            if(mouseItem.obj != null)
            {
                mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
            }
        }
    }

    public class MouseItem
    {
        public GameObject obj;
        public InventorySlot fromSlot;
        public InventorySlot hoverInventorySlot;
        public GameObject hoverObject;
    }
}

