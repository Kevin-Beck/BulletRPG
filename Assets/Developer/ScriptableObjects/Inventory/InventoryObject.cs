
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BulletRPG.Items
{
    public delegate void Notify();

    [CreateAssetMenu(fileName = "New Inventory", menuName = "Item/InventoryObject")]
    public class InventoryObject : ScriptableObject
    {
        public string savePath;
        public GearDatabaseObject database;
        public Inventory Container;

        public event Notify InventoryChanged;

        public bool SetInventorySlot(InventorySlot slot, Gear gear, int amount)
        {
            slot.SetSlotData(gear, amount);
            InventoryChanged.Invoke();
            return true;
        }
        public bool AddItem(Gear gear, int _amount)
        {
            Debug.Log("Adding gear in InventoryObject");
            // if the item is not stackable
            if (!gear.IsStackable)
            {
                return AddToFirstEmptySlot(gear, _amount);
            }

            for (int i = 0; i < Container.inventorySlots.Length; i++)
            {
                if (Container.inventorySlots[i].gear != null && Container.inventorySlots[i].gear.Id == gear.Id)
                {
                    Container.inventorySlots[i].AddAmount(_amount);
                    InventoryChanged?.Invoke();
                    return true;
                }
            }
            return AddToFirstEmptySlot(gear, _amount);
        }
        private bool AddToFirstEmptySlot(Gear item, int amount)
        {
            for (int i = 0; i < Container.inventorySlots.Length; i++)
            {
                if (Container.inventorySlots[i].gear == null || Container.inventorySlots[i].gear.Id == -1)
                {
                    Container.inventorySlots[i].SetSlotData(item, amount);
                    Debug.Log($"Added {amount}x {item} to new stack");
                    InventoryChanged?.Invoke();
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
            InventoryChanged?.Invoke();
        }
        public bool RemoveItem(Gear _gear, int amountToRemove)
        {
            for (int i = 0; i < Container.inventorySlots.Length; i++)
            {
                if(_gear.Id > -1)
                {
                    if (Container.inventorySlots[i].gear == _gear)
                    {
                        var amountInSlot = Container.inventorySlots[i].amount;
                        if( amountInSlot >= amountToRemove)
                        {
                            Container.inventorySlots[i].SetSlotData(null, amountInSlot-amountToRemove);                        
                            InventoryChanged?.Invoke();
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
                    InventoryChanged.Invoke();
                    return true;
                }
            }
            else
            {
                if(AddToFirstEmptySlot(slot.gear, slot.amount / 2))
                {
                    slot.amount = slot.amount / 2 + 1;
                    InventoryChanged.Invoke();
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
                InventoryChanged.Invoke();
            }
        }
        [ContextMenu("Clear")]
        public void Clear()
        {
            Container.Clear();
            InventoryChanged.Invoke();
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
    [System.Serializable]
    public class InventorySlot
    {
        public Gear gear = null;
        public int amount;
        public GearType[] AllowedGear = new GearType[0];
        public InventorySlot()
        {
            gear = null;
            amount = 0;
        }
        public InventorySlot(Gear _gear, int _amount)
        {
            gear = _gear;
            amount = _amount;
        }
        /// <summary>
        /// Warning: Updating this does not trigger an UpdateDisplay call, use the InventoryObject.SetInventorySlot() instead
        /// </summary>
        /// <param name="_gear"></param>
        /// <param name="_amount"></param>
        public void SetSlotData(Gear _gear, int _amount)
        {
            gear = _gear;
            amount = _amount;
        }
        public void AddAmount(int value)
        {
            amount += value;
        }
        public string PrintAllowedSlots()
        {
            string slots = $"AllowedGear.Length = {AllowedGear.Length}\n";
            for (int i = 0; i < AllowedGear.Length; i++)
            {
                slots += $"{AllowedGear[i]}\n";
            }
            return slots;
        }
        public void AllowThisGear(GearType typeToAllow)
        {
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

