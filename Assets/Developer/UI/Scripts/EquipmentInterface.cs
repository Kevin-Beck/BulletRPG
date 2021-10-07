using BulletRPG.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BulletRPG.UI
{
    public class EquipmentInterface : UserInterface
    {
        GameObject player;
        public GameObject equipmentSlotPrefab;
        Transform contentPanel;
        private void Awake()
        {
            contentPanel = Utilities.RecursiveFindChild(transform, "Content");
            CreateLinkToInventoryUpdateEvent();
            CreateSlots();
        }
        private void Start()
        {
            UpdateDisplay();
        }
        public override void CreateLinkToInventoryUpdateEvent()
        {
            inventory.InventoryChanged += UpdateDisplay;
        }

        public override void CreateSlots()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            string equipments = "";
            if (player != null)
            {
                List<GearType> types = new List<GearType>();
                foreach (GearType gearType in (GearType[])Enum.GetValues(typeof(GearType)))
                {
                    var equipmentPoint = Utilities.RecursiveFindChild(player.transform, gearType.ToString());
                    if (equipmentPoint != null)
                    {
                        equipments += gearType.ToString() + " ";
                        var slot = Instantiate(equipmentSlotPrefab, contentPanel);
                        var text = Utilities.RecursiveFindChild(slot.transform, "SlotName");
                        text.GetComponent<TextMeshProUGUI>().text = gearType.ToString();
                        types.Add(gearType);
                    }
                }
                InventorySlotButton[] buttons = GetComponentsInChildren<InventorySlotButton>();
                for (int i = 0; i < buttons.Length; i++)
                {
                    inventory.Container.inventorySlots[i].AllowThisGear(types[i]);
                    SlotMap.Add(buttons[i], inventory.Container.inventorySlots[i]);                    
                    var buttonGameObject = buttons[i].gameObject;
                    Utilities.AddEvent(buttonGameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonGameObject); });
                    Utilities.AddEvent(buttonGameObject, EventTriggerType.PointerExit, delegate { OnExit(buttonGameObject); });
                    Utilities.AddEvent(buttonGameObject, EventTriggerType.BeginDrag, delegate { OnDragStart(buttonGameObject); });
                    Utilities.AddEvent(buttonGameObject, EventTriggerType.EndDrag, delegate { OnDragEnd(buttonGameObject); });
                    Utilities.AddEvent(buttonGameObject, EventTriggerType.Drag, delegate { OnDrag(buttonGameObject); });
                }
                Debug.Log("Equipment Interface Found Equipment Slots: (" + equipments.Trim() + ") in the character model");
            }
        }        
    }
}

