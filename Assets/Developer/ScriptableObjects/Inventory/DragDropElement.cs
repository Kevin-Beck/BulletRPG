using BulletRPG.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.UI
{    public class DragDropElement
    {
        private DragDropElement() { }
        private static DragDropElement instance = null;
        public static DragDropElement Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DragDropElement();
                }
                return instance;
            }
        }
        public GameObject hoveringOverObject;
        public InventorySlotButton hoveringOverButton;
        public InventorySlotButton fromButton;
        public GameObject icon;
    }
}

