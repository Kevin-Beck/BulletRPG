using BulletRPG.Gear;
using BulletRPG.Gear.Weapons.RangedWeapons;
using BulletRPG.Items;
using Ricimi;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BulletRPG.UI.Inventory
{
    public class InventorySlotButton : MonoBehaviour
    {
        [System.NonSerialized]
        public InventoryUserInterface inventoryGroupParent;

        public ItemSettings itemSettings;

        private InventorySlot slot;

        private Image icon;
        private Image border;
        private Image background;
        private TextMeshProUGUI counter;
        private TextMeshProUGUI tooltiptitle;
        private TextMeshProUGUI tooltiptext;
        private GameObject ToolTip;

        private string tipText;
        private string tipTitle;

        private void Awake()
        {
            ToolTip = GameObject.FindGameObjectWithTag("ToolTip");
            GetComponent<Tooltip>().tooltip = ToolTip;

            icon = Utilities.RecursiveFindChild(transform, "Icon").GetComponent<Image>();
            counter = Utilities.RecursiveFindChild(transform, "Counter").GetComponent<TextMeshProUGUI>();
            border = Utilities.RecursiveFindChild(transform, "Border").GetComponent<Image>();
            background = Utilities.RecursiveFindChild(transform, "Background").GetComponent<Image>();
            tooltiptitle = Utilities.RecursiveFindChild(ToolTip.transform, "ToolTipTitle").GetComponent<TextMeshProUGUI>();
            tooltiptext = Utilities.RecursiveFindChild(ToolTip.transform, "ToolTipText").GetComponent<TextMeshProUGUI>();
            inventoryGroupParent = GetComponentInParent<InventoryUserInterface>();
            Utilities.AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { ShowToolTip(); });
            Utilities.AddEvent(gameObject, EventTriggerType.PointerExit, delegate { RemoveToolTip(); });
        }
        private void Start()
        {
            UpdateSlotUI(slot.item.Id < 0 ? null : slot.item);
        }
        protected void AddEvent(GameObject button, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = button.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }
        private void ShowToolTip()
        {
            if(slot.amount == 0)
            {
                return;
            }
            tooltiptitle.text = tipTitle;
            tooltiptext.text = tipText;
            var myRect = GetComponent<RectTransform>();
            ToolTip.GetComponent<RectTransform>().position = new Vector2(myRect.position.x - myRect.sizeDelta.x, GetComponent<RectTransform>().position.y);
            ToolTip.SetActive(true);
            
        }
        private void RemoveToolTip()
        {
            tooltiptitle.text = "";
            tooltiptext.text = "";
            ToolTip.SetActive(false);
        }
        private void SetCounterText(string count)
        {
            counter.text = count;
        }
        private void SetIconSpriteAndColor(Sprite sprite, Color color)
        {            
            icon.sprite = sprite;
            icon.color = color;
        }
        private void SetBackgroundColor(Color color)
        {
            background.color = color;
        }
        private void SetBorderColor(Color color)
        {
            border.color = color;
        }
        public void SetSlot(InventorySlot slotToRepresent)
        {
            slot = slotToRepresent;
            slot.AllowThisGearType(GearSlot.Default);
        }
        public void UpdateSlotUI(Item item)
        {
            if (slot == null || item == null)
            {
                SetCounterText("");
                SetIconSpriteAndColor(null, Color.clear);
                SetBorderColor(Color.clear);
                SetBackgroundColor(Color.clear);
                tipText = "";
                tipTitle = "";
            }
            else
            {
                SetCounterText(slot.amount == 1 || slot.amount == 0 ? "" : slot.amount.ToString());
                if (item.itemType == ItemType.Gear)
                {
                    var sprite = inventoryGroupParent.inventory.database.GetItemObject[item.Id].sprite;
                    SetIconSpriteAndColor(sprite, Color.black);

                    var rangeWeapon = (RangedWeapon)item;
                    if(rangeWeapon != null)
                    {
                        SetBorderColor(itemSettings.damageTypeColors.DamageColorMap[rangeWeapon.damage.type]);
                        var rarity = ((Gear.Gear)item).rarity;
                        SetBackgroundColor(itemSettings.rarityColors.RarityColorMap[rarity]);
                    }
                }

                tipTitle = slot.item.name;
                tipText = slot.item.description;                
            }
        }
    }
}

