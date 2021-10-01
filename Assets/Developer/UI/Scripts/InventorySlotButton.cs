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
        public UserInterface inventoryGroupParent;
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
            inventoryGroupParent = GetComponentInParent<UserInterface>();
        }

        public void SetToolTipTitleAndText(string title, string text)
        {
            tooltiptitle.text = title;
            tooltiptext.text = text;
        }
        public void SetToolTipEnabled(bool enabled)
        {
            ToolTip.SetActive(enabled);
        }
        public void SetCounterText(string count)
        {
            counter.text = count;
        }
        public void SetIconSpriteAndColor(Sprite sprite, Color color)
        {            
            icon.sprite = sprite;
            icon.color = color;
        }        
    }
}

