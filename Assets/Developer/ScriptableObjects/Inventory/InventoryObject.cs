
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Item/InventoryObject")]
    public class InventoryObject : ScriptableObject
    {
        public string savePath;
        public GearDatabaseObject database;
        private Inventory Container;
        public InventorySlot[] GetSlots { get { return Container.inventorySlots; } }

        public bool SetInventorySlot(InventorySlot slot, Gear gear, int amount)
        {
            slot.SetSlotData(gear, amount);
             
            return true;
        }
        public bool AddItem(Gear gear, int _amount)
        {
            Debug.Log("Attempting to add gear in InventoryObject");
            // if the item is not stackable
            if (!gear.IsStackable)
            {
                return AddToFirstEmptySlot(gear, _amount);
            }

            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].gear != null && GetSlots[i].gear.Id == gear.Id)
                {
                    GetSlots[i].AddAmount(_amount);
                    
                    return true;
                }
            }
            return AddToFirstEmptySlot(gear, _amount);
        }
        private bool AddToFirstEmptySlot(Gear item, int amount)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].gear == null || GetSlots[i].gear.Id == -1)
                {
                    GetSlots[i].SetSlotData(item, amount);
                    Debug.Log($"Added {amount}x {item} to new stack");
                    
                    return true;
                }
            }
            // full inventory here!
            return false;
        }
        public void MoveItem(InventorySlot firstSlot, InventorySlot secondSlot)
        {
            InventorySlot temp = new InventorySlot (secondSlot.gear, secondSlot.amount);
            secondSlot.SetSlotData(firstSlot.gear, firstSlot.amount);
            firstSlot.SetSlotData(temp.gear, temp.amount);
            
        }
        public bool RemoveItem(Gear _gear, int amountToRemove)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if(_gear.Id > -1)
                {
                    if (GetSlots[i].gear == _gear)
                    {
                        var amountInSlot = GetSlots[i].amount;
                        if( amountInSlot >= amountToRemove)
                        {
                            
                            GetSlots[i].SetSlotData(new Gear(), amountInSlot-amountToRemove);                            
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
                if (AddToFirstEmptySlot(slot.gear, slot.amount / 2))
                {
                    slot.amount = slot.amount / 2;
                     
                    return true;
                }
            }
            else
            {
                if(AddToFirstEmptySlot(slot.gear, slot.amount / 2))
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
                database = Resources.Load<GearDatabaseObject>("ItemDatabase");                 
            }
        }
        [ContextMenu("Clear")]
        public void Clear()
        {
            Container.Clear();
             
        }
    }
    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] inventorySlots = new InventorySlot[21];
        public void Clear()
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i].SetSlotData(new Gear(), 0);
            }
        }
    }


    public delegate void SlotUpdated(InventorySlot slot);
    [System.Serializable]
    public class InventorySlot
    {
        public Gear gear = null;
        public int amount;
        public GearType[] AllowedGear = new GearType[0];

        [System.NonSerialized]
        public SlotUpdated OnAfterUpdate;
        [System.NonSerialized]
        public SlotUpdated OnBeforeUpdate;
        public InventorySlot()
        {
            SetSlotData(null, 0);
        }
        public InventorySlot(Gear _gear, int _amount)
        {
            SetSlotData(_gear, _amount);
        }
        public void SetSlotData(Gear _gear, int _amount)
        {
            OnBeforeUpdate?.Invoke(this);
            gear = _gear;
            amount = _amount;
            OnAfterUpdate?.Invoke(this);
        }
        public void AddAmount(int value)
        {
            SetSlotData(gear, amount += value);
        }
        public void InitializeAllowedGear(GearType[] typesToAllow)
        {
            AllowedGear = typesToAllow;
        }
        public void AllowThisGear(GearType typeToAllow)
        {
            Debug.Log($"Adding {typeToAllow} to allowedGear on {this}");
            GearType[] temp = new GearType[AllowedGear.Length + 1];

            for (int i = 0; i < AllowedGear.Length; i++)
            {
                temp[i] = AllowedGear[i];
            }
            temp[AllowedGear.Length] = typeToAllow;
            AllowedGear = temp;
        }
        public bool CanPlaceInSlot(Gear _gear)
        {
            if(AllowedGear.Length == 0)
            {
                return true;
            }
            for (int i = 0; i < AllowedGear.Length; i++)
            {
                if(_gear.gearType == AllowedGear[i])
                {
                    return true;
                }                
            }
            return false;
        }
    }
}

