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
        private ItemDatabaseObject database;
        private void Awake()
        {
            ToolTip = Utilities.RecursiveFindChild(transform, "ToolTip").gameObject;
            icon = Utilities.RecursiveFindChild(transform, "Icon").GetComponent<Image>();
            counter = Utilities.RecursiveFindChild(transform, "Counter").GetComponent<TextMeshProUGUI>();
            tooltiptitle = Utilities.RecursiveFindChild(transform, "ToolTipTitle").GetComponent<TextMeshProUGUI>();
            tooltiptext = Utilities.RecursiveFindChild(transform, "ToolTipText").GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            UpdateButton();
        }
        private void UpdateButton()
        {
            if (inventorySlot == null || inventorySlot.item.Id < 0)
            {
                SetIconSpriteAndColor(null, Color.clear);
                SetToolTipTitleAndText("", "");
                SetCounterText("");
                ToolTip.SetActive(false);
            }
            else
            {
                var gameItem = inventorySlot.item;
                var referenceObject = database.GetItem[gameItem.Id];
                SetIconSpriteAndColor(referenceObject.sprite, referenceObject.Color); // references the scriptable object
                SetToolTipTitleAndText(gameItem.Name, gameItem.Description); // references the actual object instance
                SetCounterText(inventorySlot.amount.ToString());
                ToolTip.SetActive(true);
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
        public void SetItemDatabaseObject(ItemDatabaseObject databaseObject)
        {
            database = databaseObject;
        }
        
    }
}

