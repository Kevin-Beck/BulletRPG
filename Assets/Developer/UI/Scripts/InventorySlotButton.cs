using BulletRPG.Gear;
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
        private InventorySlot slot;

        private Image icon;
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
            tooltiptitle = Utilities.RecursiveFindChild(ToolTip.transform, "ToolTipTitle").GetComponent<TextMeshProUGUI>();
            tooltiptext = Utilities.RecursiveFindChild(ToolTip.transform, "ToolTipText").GetComponent<TextMeshProUGUI>();
            inventoryGroupParent = GetComponentInParent<InventoryUserInterface>();
            Utilities.AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { ShowToolTip(); });
            Utilities.AddEvent(gameObject, EventTriggerType.PointerExit, delegate { RemoveToolTip(); });
        }
        private void Start()
        {
            UpdateSlotUI(slot.gear.Id < 0 ? null : inventoryGroupParent.inventory.database.gearObjects[slot.gear.Id]);
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
        public void SetSlot(InventorySlot slotToRepresent)
        {
            slot = slotToRepresent;
        }
        public void UpdateSlotUI(GearObject gearObject)
        {
            if (slot == null || gearObject == null)
            {
                SetCounterText("");
                SetIconSpriteAndColor(null, Color.clear);
                tipText = "";
                tipTitle = "";
            }
            else
            {
                SetCounterText(slot.amount == 1 || slot.amount == 0 ? "" : slot.amount.ToString());
                SetIconSpriteAndColor(gearObject.sprite, gearObject.Color);

                string buffDescriptions = "";
                foreach (GearBuff buff in slot.gear.buffs)
                {
                    buffDescriptions += buff.Stringify() + "\n";
                }
                tipTitle = gearObject.ItemName;
                tipText = $"{gearObject.ItemDescription}\n\n{buffDescriptions}";
            }
        }
    }
}

