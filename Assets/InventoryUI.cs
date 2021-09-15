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
        public GameObject inventorySlot;
        public int rows = 2;
        public int columns = 6;
        public int outerBorderSize = 5;
        public int innerBorderSize = 2;

        private GameObject[,] slotArray;
        private Gear[,] gearArray;

        private void Start()
        {
            slotArray = new GameObject[rows, columns];
            gearArray = new Gear[rows, columns];
            InitializeInventoryUI();
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<IInventory>();
            UpdateInventoryUI();
        }
        public void UpdateInventoryUI()
        {
            ClearInventoryUI();
            List<Gear> currentGear = inventory.GetInventoryGearList();
            if(currentGear.Count > rows * columns)
            {
                Debug.LogWarning("Inventory size is greater than UI inventory Allows!");
                return;
            }

            int row = 0;
            int col = 0;
            foreach(Gear gear in inventory.GetInventoryGearList())
            {
                SetSlotColorAndImage(row, col, gear.Sprite, gear.Color);
                EnableToolTip(row, col, gear.Slot, gear.ItemName, gear.ItemDescription);
                gearArray[row, col] = gear;
                col++;
                if(col == columns)
                {
                    row++;
                    col = 0;
                }
            }
        }
        private void ClearInventoryUI()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    gearArray[i, j] = null;
                    SetSlotColorAndImage(i, j, null, Color.clear);
                    DisableTooTip(i, j);
                }
            }
        }
        public void Equip(GameObject uiButton)
        {
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    if (uiButton.Equals(slotArray[i, j].gameObject))
                    {
                        if(gearArray[i,j] != null)
                        {
                            inventory.Equip(gearArray[i, j]);
                        }
                    }
                }
            }
        }
        private void InitializeInventoryUI()
        {
            var container = GetComponent<RectTransform>();
            var slotSize = inventorySlot.GetComponent<RectTransform>().sizeDelta;

            container.sizeDelta = new Vector2(columns * slotSize.x + 2 * outerBorderSize + (columns-1)*innerBorderSize, rows * slotSize.y + 2 * outerBorderSize + (columns - 1) * innerBorderSize);

            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    slotArray[j, i] = Instantiate(inventorySlot, container);
                    slotArray[j, i].GetComponent<RectTransform>().anchoredPosition = new Vector2(outerBorderSize + i * (slotSize.x + innerBorderSize), -1 * j * (slotSize.y + innerBorderSize));

                    SetSlotColorAndImage(j, i, null, Color.clear);
                }
            }
        }
        private void EnableToolTip(int row, int column, GearSlots gearSlot, string itemName, string itemDescription)
        {
            var tooltip = Utilities.RecursiveFindChild(slotArray[row, column].transform, "Tooltip");
            if(tooltip == null)
            {
                Debug.LogWarning("no tooltip");
            }
            tooltip.gameObject.SetActive(true);
            Utilities.RecursiveFindChild(tooltip, "Title").GetComponent<TextMeshProUGUI>().text = itemName;
            Utilities.RecursiveFindChild(tooltip, "Text").GetComponent<TextMeshProUGUI>().text = $"{gearSlot}\n\n{itemDescription}";
        }
        private void DisableTooTip(int row, int column)
        {
            Utilities.RecursiveFindChild(slotArray[row, column].transform, "Tooltip").gameObject.SetActive(false);
        }
        private void SetSlotColorAndImage(int row, int column, Sprite sprite, Color color)
        {
            var icon = Utilities.RecursiveFindChild(slotArray[row, column].transform, "Icon");
            var image = icon.GetComponent<Image>();
            image.sprite = sprite;
            image.color = color;
        }


    }
}
