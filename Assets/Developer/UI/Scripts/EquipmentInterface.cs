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
                        equipments += gearType.ToString() + "\n";
                        var slot = Instantiate(equipmentSlotPrefab, contentPanel);
                        var text = Utilities.RecursiveFindChild(slot.transform, "SlotName");
                        text.GetComponent<TextMeshProUGUI>().text = gearType.ToString();
                        types.Add(gearType);
                    }
                }
                InventorySlotButton[] buttons = GetComponentsInChildren<InventorySlotButton>();
                for (int i = 0; i < buttons.Length; i++)
                {
                    Debug.Log(i);
                    inventory.Container.inventorySlots[i].AllowThisGear(types[i]);
                    SlotMap.Add(buttons[i], inventory.Container.inventorySlots[i]);                    
                    var buttonGameObject = buttons[i].gameObject;
                    AddEvent(buttonGameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonGameObject); });
                    AddEvent(buttonGameObject, EventTriggerType.PointerExit, delegate { OnExit(buttonGameObject); });
                    AddEvent(buttonGameObject, EventTriggerType.BeginDrag, delegate { OnDragStart(buttonGameObject); });
                    AddEvent(buttonGameObject, EventTriggerType.EndDrag, delegate { OnDragEnd(buttonGameObject); });
                    AddEvent(buttonGameObject, EventTriggerType.Drag, delegate { OnDrag(buttonGameObject); });
                }
                Debug.Log("Found Equipment: \n" + equipments);
            }
        }        
    }
}

