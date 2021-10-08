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
            player = GameObject.FindGameObjectWithTag("Player");
            string equipments = "";
            if (player != null)
            {
                // Equipment slots are currently Retrieved from the character model by name by looping through all types of gear
                // With this system in place each slot is only able to take a single type of object
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

                // After inventory buttons have been made, initialize each slot with the allowed gear for that slot                
                InventorySlotButton[] buttons = GetComponentsInChildren<InventorySlotButton>();
                for (int i = 0; i < buttons.Length; i++)
                {                   
                    // basic way to map each slot to a specific type of slot, this is where allowable items for each slot are set
                    inventory.GetSlots[i].InitializeAllowedGear(new GearType[] { types[i] });

                    LinkButtonToInventorySlot(buttons[i], inventory.GetSlots[i]);

                    // Add Drag and drop functionality to buttons by adding events to the game object
                    var buttonGameObject = buttons[i].gameObject;
                    Utilities.AddEvent(buttonGameObject, EventTriggerType.PointerEnter, delegate { OnEnter(buttonGameObject); });
                    Utilities.AddEvent(buttonGameObject, EventTriggerType.PointerExit, delegate { OnExit(buttonGameObject); });
                    Utilities.AddEvent(buttonGameObject, EventTriggerType.BeginDrag, delegate { OnDragStart(buttonGameObject); });
                    Utilities.AddEvent(buttonGameObject, EventTriggerType.EndDrag, delegate { OnDragEnd(buttonGameObject); });
                    Utilities.AddEvent(buttonGameObject, EventTriggerType.Drag, delegate { OnDrag(buttonGameObject); });
                }
                Debug.Log("Equipment Interface Found Equipment Slots: (" + equipments.Trim() + ") in the character model");
            }
            else
            {
                Debug.LogWarning("Player not found by EquipmentInterface");
            }
        }        
    }
}

