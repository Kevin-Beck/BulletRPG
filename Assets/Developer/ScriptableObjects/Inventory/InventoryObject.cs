using BulletRPG.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
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

        public bool AddItem(Gear gear, int _amount)
        {
            Debug.Log("Adding gear in InventoryObject");
            // if the item is not stackable
            if (gear.buffs.Length > 0)
            {
                AddToFirstEmptySlot(gear, _amount);
                InventoryChanged.Invoke();
                return true;
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
            AddToFirstEmptySlot(gear, _amount);
            return true;
        }
        private InventorySlot AddToFirstEmptySlot(Gear item, int amount)
        {
            for (int i = 0; i < Container.inventorySlots.Length; i++)
            {
                if (Container.inventorySlots[i].gear == null || Container.inventorySlots[i].gear.Id == -1)
                {
                    Container.inventorySlots[i].UpdateSlot(item, amount);
                    Debug.Log($"Added {amount}x {item} to new stack");
                    InventoryChanged?.Invoke();
                    return Container.inventorySlots[i];
                }
            }
            // full inventory here!
            Debug.Log("Full Inventory not handled");
            return null;
        }
        public void MoveItem(InventorySlot firstSlot, InventorySlot secondSlot)
        {
            InventorySlot temp = new InventorySlot (secondSlot.gear, secondSlot.amount);
            secondSlot.UpdateSlot(firstSlot.gear, firstSlot.amount);
            firstSlot.UpdateSlot(temp.gear, temp.amount);
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
                            Container.inventorySlots[i].UpdateSlot(null, amountInSlot-amountToRemove);                        
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
                inventorySlots[i].UpdateSlot(new Gear(), 0);
            }
        }
    }
    [System.Serializable]
    public class InventorySlot
    {
        public Gear gear = null;
        public int amount;
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
        public void UpdateSlot(Gear _gear, int _amount)
        {
            gear = _gear;
            amount = _amount;
        }
        public void AddAmount(int value)
        {
            amount += value;
        }
    }
}

