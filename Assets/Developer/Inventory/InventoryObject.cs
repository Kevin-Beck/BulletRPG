
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BulletRPG.Gear
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Item/InventoryObject")]
    public class InventoryObject : ScriptableObject
    {
        public string savePath;
        public ItemDatabaseObject database;
        [SerializeField] private Inventory Container;
        public InventorySlot[] GetSlots { get { return Container.inventorySlots; } }

        public InventoryObject()
        {
            Container = new Inventory();
        }
        public bool SetInventorySlot(InventorySlot slot, Item item, int amount)
        {
            slot.SetSlotData(item, amount);
             
            return true;
        }
        public bool AddItem(Item item, int _amount)
        {
            Debug.Log("Attempting to add item in InventoryObject");
            // if the item is not stackable
            if (!item.isStackable)
            {
                return AddToFirstEmptySlot(item, _amount);
            }

            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item != null && GetSlots[i].item.Id == item.Id)
                {
                    GetSlots[i].AddAmount(_amount);
                    
                    return true;
                }
            }
            return AddToFirstEmptySlot(item, _amount);
        }
        private bool AddToFirstEmptySlot(Item item, int amount)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item == null || GetSlots[i].item.Id == -1)
                {
                    GetSlots[i].SetSlotData(item, amount);
                    Debug.Log($"Added {amount}x {item.name} to new stack");                    
                    return true;
                }
            }
            // full inventory here!
            return false;
        }
        public void MoveItem(InventorySlot firstSlot, InventorySlot secondSlot)
        {
            InventorySlot temp = new InventorySlot (secondSlot.item, secondSlot.amount);
            secondSlot.SetSlotData(firstSlot.item, firstSlot.amount);
            firstSlot.SetSlotData(temp.item, temp.amount);
            
        }
        public bool RemoveItem(Item _item, int amountToRemove)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if(_item.Id > -1)
                {
                    if (GetSlots[i].item == _item)
                    {
                        var amountInSlot = GetSlots[i].amount;
                        if( amountInSlot > amountToRemove)
                        {
                            
                            GetSlots[i].SetSlotData(_item, amountInSlot-amountToRemove);                            
                            return true;
                        }else if(amountInSlot == amountToRemove)
                        {
                            GetSlots[i].SetSlotData(null, 0);
                            return true;
                        }
                        else
                        {
                            Debug.Log("tried to remove more than we have!");
                            return false;
                        }
                    }
                }
            }
            Debug.Log("Failed To Remove Item");
            return false;
        }

        public bool SplitStack(InventorySlot slot)
        {
            if(slot.amount < 2)
            {
                return false;
            }
            if(slot.amount % 2 == 0)
            {
                if (AddToFirstEmptySlot(slot.item, slot.amount / 2))
                {
                    slot.amount = slot.amount / 2;
                     
                    return true;
                }
            }
            else
            {
                if(AddToFirstEmptySlot(slot.item, slot.amount / 2))
                {
                    slot.amount = slot.amount / 2 + 1;
                     
                    return true;
                }
            }
            return false;
        }
        [ContextMenu("Save")]
        public void Save()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
            bf.Serialize(file, saveData);
            file.Close();

            Debug.Log($"Inventory: {name} saved to: {savePath}");

            System.IO.File.WriteAllText(Application.persistentDataPath + "/save.json", saveData);
        }
        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();

                Debug.Log($"Inventory: {name} loaded from: {savePath}");
                database = Resources.Load<ItemDatabaseObject>("ItemDatabase");    
                foreach(InventorySlot slot in Container.inventorySlots)
                {
                    slot.SetSlotData(slot.item, slot.amount);
                }
            }
        }
        [ContextMenu("Clear")]
        public void Clear()
        {
            Container.Clear();             
        }
        public void SetSize(int size)
        {
            Container.SetSize(size);
        }
    }
    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] inventorySlots = new InventorySlot[21];
        public void SetSize(int size)
        {
            inventorySlots = new InventorySlot[size];
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i] = new InventorySlot();
                inventorySlots[i].SetSlotData(new Item(), 0);
            }
        }
        public Inventory()
        {
        }
        public void Clear()
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i].SetSlotData(new Item(), 0);
            }
        }
    }


    public delegate void SlotUpdated(InventorySlot slot);
    [System.Serializable]
    public class InventorySlot
    {
        public Item item = null;
        public int amount;
        public HashSet<GearSlot> AllowedGearTypes = new HashSet<GearSlot>();
        public HashSet<ItemType> AllowedItemTypes = new HashSet<ItemType>();

        [System.NonSerialized]
        public SlotUpdated OnAfterUpdate;
        [System.NonSerialized]
        public SlotUpdated OnBeforeUpdate;
        public InventorySlot()
        {
            SetSlotData(new Item(), 0);
        }
        public InventorySlot(Item _item, int _amount)
        {
            SetSlotData(_item, _amount);
        }
        public void SetSlotData(Item _item, int _amount)
        {
            OnBeforeUpdate?.Invoke(this);
            item = _item;
            amount = _amount;
            OnAfterUpdate?.Invoke(this);
        }
        public void AddAmount(int value)
        {
            SetSlotData(item, amount += value);
        }
        public void InitializeAllowedItemTypes(ItemType[] itemsToAllow)
        {
            foreach(ItemType type in itemsToAllow)
            {
                AllowedItemTypes.Add(type);
            }
        }
        public void InitializeAllowedGearTypes(GearSlot[] typesToAllow)
        {
            foreach(GearSlot slot in typesToAllow)
            {
                AllowedGearTypes.Add(slot);
            }
        }
        public void AllowThisItemType(ItemType itemType)
        {
            AllowedItemTypes.Add(itemType);
        }
        public void AllowThisGearType(GearSlot typeToAllow)
        {
            AllowedItemTypes.Add(ItemType.Gear);
            AllowedGearTypes.Add(typeToAllow);
        }
        public bool CanPlaceInSlot(Item item)
        {
            if (AllowedItemTypes.Contains(ItemType.All))
            {
                return true;
            }
            if(item == null)
            {
                return true;
            }

            if (!AllowedItemTypes.Contains(item.itemType))
            {
                Debug.Log("Does not containType in allowedItemtypes");
                return false;
            }

            if(item.itemType == ItemType.Gear)
            {
                if(!AllowedGearTypes.Contains((item as Gear).gearSlot))
                {
                    Debug.Log("Does not contain GearType in allowed gearTypes");
                    return false;
                }
            }
            return true;
        }
    }
}

