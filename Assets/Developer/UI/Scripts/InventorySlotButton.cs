using BulletRPG.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BulletRPG.UI
{
    public class InventorySlotButton : MonoBehaviour
    {
        public InventorySlot inventorySlot;
        private Image icon;
        private TextMeshProUGUI counter;
        private TextMeshProUGUI tooltiptitle;
        private TextMeshProUGUI tooltiptext;
        private GameObject ToolTip;
        private void Awake()
        {
            ToolTip = Utilities.RecursiveFindChild(transform, "ToolTip").gameObject;
            icon = Utilities.RecursiveFindChild(transform, "Icon").GetComponent<Image>();
            counter = Utilities.RecursiveFindChild(transform, "Counter").GetComponent<TextMeshProUGUI>();
            tooltiptitle = Utilities.RecursiveFindChild(transform, "ToolTipTitle").GetComponent<TextMeshProUGUI>();
            tooltiptext = Utilities.RecursiveFindChild(transform, "ToolTipText").GetComponent<TextMeshProUGUI>();
            UpdateButton();
        }

        private void UpdateButton()
        {
            var item = inventorySlot.item;
            if(item == null)
            {
                SetIconSpriteAndColor(null, Color.clear);
                SetToolTipTitleAndText("", "");
                SetCounterText("");
                ToolTip.SetActive(false);
            }
            else
            {
                ToolTip.SetActive(true);
                SetIconSpriteAndColor(item.Sprite, item.Color);
                SetToolTipTitleAndText(item.name, item.ItemDescription);
                SetCounterText(inventorySlot.amount.ToString());                
            }
        }
        private void SetToolTipTitleAndText(string title, string text)
        {
            tooltiptitle.text = title;
            tooltiptext.text = text;
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
        public void SetInventorySlot(InventorySlot slot)
        {
            inventorySlot = slot;
            UpdateButton();
        }
    }
}

