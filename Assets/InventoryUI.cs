using Ricimi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletRPG.Items
{
    public class InventoryUI : MonoBehaviour
    {
        IInventory inventory;
        EquipmentManager equipmentManager;
        [SerializeField] private Transform equipmentContainer;
        [SerializeField] private TextMeshProUGUI equipmentTextTemplate;
        [SerializeField] private Transform equipmentSlotTemplate;

        [SerializeField] private Transform inventoryContainer;
        [SerializeField] private Transform inventoryIconButton;
        [SerializeField] private Transform inventoryBackground;
        [SerializeField] private Sprite notAvailableIcon;
        private int padding = 8;

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<IInventory>();
            equipmentManager = player.GetComponent<EquipmentManager>();
            UpdateInventoryUI();
        }

        public void UpdateInventoryUI()
        {
            foreach (Transform child in inventoryContainer.GetComponentsInChildren<Transform>())
            {
                if (child == inventoryContainer || child == inventoryIconButton) continue;
                Destroy(child.gameObject);
            }
            foreach(Transform child in equipmentContainer.GetComponentsInChildren<Transform>())
            {
                if( child == equipmentContainer || child == equipmentSlotTemplate || child == equipmentTextTemplate) continue;
                Destroy(child.gameObject);
            }
            // Equipments
            int count = 0;
            foreach (EquipmentSlot slot in equipmentManager.myEquipmentSlots)
            {
                Debug.Log("Creating EquipSlots");
                TextMeshProUGUI equipText = Instantiate(equipmentTextTemplate, equipmentContainer);
                equipText.gameObject.SetActive(true);
                equipText.text = slot.GearSlot.ToString();
                equipText.GetComponent<RectTransform>().anchoredPosition = new Vector2(10, count * -110);

                RectTransform equipSlot = Instantiate(equipmentSlotTemplate, equipmentContainer).GetComponent<RectTransform>();
                equipSlot.gameObject.SetActive(true);
                equipSlot.anchoredPosition = new Vector2(300, count * -110 + -50);

                Gear gear = equipmentManager.GetGearInSlot(slot.GearSlot);
                if (gear != null)
                {
                    equipSlot.GetComponentInChildren<GearEquipButton>().GearTiedToButton = gear;
                    SetSlotColorAndImage(equipSlot, gear.Sprite, gear.Color);
                }
                else
                {
                    equipSlot.GetComponentInChildren<GearEquipButton>().GearTiedToButton = null;
                    SetSlotColorAndImage(equipSlot, null, Color.clear);
                }

                count++;
            }

            // Inventory
            int x = 0;
            int y = 0;
            List<Gear> currentGear = inventory.GetInventoryGearList();
            Debug.Log(currentGear.Count);
            for(int i = 0; i < 12; i++)
            {                
                if(i < inventory.GetMaxCapacity())
                {
                    Debug.Log("In creationLoop");

                    RectTransform itemSlot = Instantiate(inventoryIconButton, inventoryContainer).GetComponent<RectTransform>();
                    itemSlot.gameObject.SetActive(true);
                    itemSlot.anchoredPosition = new Vector2(50 + x * 100 + x * padding + padding, -50 + -1 * y * 100 - y * padding - padding);
                    x++;
                    if (x > 2)
                    {
                        x = 0;
                        y++;
                    }
                    if (currentGear.Count > i && currentGear[i] != null)
                    {
                        Debug.Log("Making gear");
                        Gear gear = currentGear[i];
                        Debug.Log("Gear Name: " + gear.ItemName);
                        itemSlot.GetComponentInChildren<GearEquipButton>().GearTiedToButton = gear;
                        SetSlotColorAndImage(itemSlot, gear.Sprite, gear.Color);
                      //  EnableToolTip(itemSlot, gear.Slot, gear.name, gear.ItemDescription);
                    }
                    else
                    {
                        SetSlotColorAndImage(itemSlot, null, Color.clear);
                       // DisableToolTip(itemSlot);
                    }
                }
                else
                {
                    RectTransform itemSlot = Instantiate(inventoryIconButton, inventoryContainer).GetComponent<RectTransform>();
                    itemSlot.gameObject.SetActive(true);
                    itemSlot.anchoredPosition = new Vector2(50 + x * 100 + x * padding + padding, -50 + -1 * y * 100 - y * padding - padding);
                    x++;
                    if (x > 2)
                    {
                        x = 0;
                        y++;
                    }
                    Debug.Log("Making Empty Inventory Spots");
                    SetSlotColorAndImage(itemSlot, notAvailableIcon, Color.black);
                    itemSlot.GetComponent<CanvasGroup>().enabled = false;
                    itemSlot.GetComponent<GearEquipButton>().enabled = false;
                    itemSlot.GetComponent<CleanButton>().enabled = false;
                    //DisableToolTip(itemSlot);
                }
            }
        }

        public void Equip(Gear gear)
        {
            inventory.Equip(gear);
        }
        private void EnableToolTip(Transform slot, GearSlots gearSlot, string itemName, string itemDescription)
        {
            var tooltip = Utilities.RecursiveFindChild(slot, "Tooltip");
            if (tooltip == null)
            {
                Debug.LogWarning("no tooltip");
            }
            tooltip.gameObject.SetActive(true);
            Utilities.RecursiveFindChild(tooltip, "Title").GetComponent<TextMeshProUGUI>().text = itemName;
            Utilities.RecursiveFindChild(tooltip, "Text").GetComponent<TextMeshProUGUI>().text = $"{gearSlot}\n\n{itemDescription}";
        }
        private void DisableToolTip(Transform slot)
        {
            Utilities.RecursiveFindChild(slot, "Tooltip").gameObject.SetActive(false);
        }
        private void SetSlotColorAndImage(Transform slot, Sprite sprite, Color color)
        {
            var icon = Utilities.RecursiveFindChild(slot, "Icon");
            var image = icon.GetComponent<Image>();
            image.sprite = sprite;
            image.color = color;
        }


    }
}
