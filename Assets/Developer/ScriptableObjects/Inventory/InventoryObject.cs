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
        public ItemDatabaseObject database;
        public Inventory Container;

        public event Notify PlayerInventoryChanged;

        public bool AddItem(Gear gear, int _amount)
        {
            // if the item is not stackable
            if (gear.buffs.Length > 0)
            {
                AddToFirstEmptySlot(gear, _amount);
                PlayerInventoryChanged?.Invoke();
                return true;
            }

            for (int i = 0; i < Container.inventorySlots.Length; i++)
            {
                if (Container.inventorySlots[i].ID == gear.Id)
                {
                    Container.inventorySlots[i].AddAmount(_amount);
                    PlayerInventoryChanged?.Invoke();
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
                if (Container.inventorySlots[i].ID <= -1)
                {
                    Container.inventorySlots[i].UpdateSlot(item.Id, item, amount);
                    Debug.Log($"Added {amount}x {item} to new stack");
                    PlayerInventoryChanged?.Invoke();
                    return Container.inventorySlots[i];
                }
            }
            // full inventory here!
            Debug.Log("Full Inventory not handled");
            return null;
        }
        public void MoveItem(InventorySlot firstSlot, InventorySlot secondSlot)
        {
            InventorySlot temp = new InventorySlot(secondSlot.ID, secondSlot.gear, secondSlot.amount);
            secondSlot.UpdateSlot(firstSlot.ID, firstSlot.gear, firstSlot.amount);
            firstSlot.UpdateSlot(temp.ID, temp.gear, temp.amount);
            PlayerInventoryChanged?.Invoke();
        }
        public bool RemoveItem(Gear _gear)
        {
            for (int i = 0; i < Container.inventorySlots.Length; i++)
            {
                if(Container.inventorySlots[i].gear == _gear)
                {
                    Container.inventorySlots[i].UpdateSlot(-1, null, 0);
                    Instantiate(database.gearObjects[_gear.Id].LootObject, Vector3.zero, Quaternion.identity);
                    PlayerInventoryChanged?.Invoke();
                    return true;
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

            //IFormatter formatter = new BinaryFormatter();
            //Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
            //formatter.Serialize(stream, Container);
            //stream.Close();
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

                //IFormatter formatter = new BinaryFormatter();
                //Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
                //Container = (Inventory)formatter.Deserialize(stream);
                //for (int i = 0; i < Container.Items.Count; i++)
                //{
                //    Container.Items[i].UpdateSlot(Container.Items[i].ID, Container.Items[i].item, Container.Items[i].amount);
                //}
                //stream.Close();
                Debug.Log($"Inventory: {name} loaded from: {savePath}");
                database = Resources.Load<ItemDatabaseObject>("ItemDatabase");
                PlayerInventoryChanged.Invoke();
            }
        }
        [ContextMenu("Clear")]
        public void Clear()
        {
            Container.inventorySlots = new InventorySlot[Container.inventorySlots.Length];
            PlayerInventoryChanged.Invoke();
        }
    }
    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] inventorySlots = new InventorySlot[21];
    }
    [System.Serializable]
    public class InventorySlot
    {
        public int ID = -1;
        public Gear gear = null;
        public int amount;
        public InventorySlot()
        {
            ID = -1;
            gear = null;
            amount = 0;
        }
        public InventorySlot(int _id, Gear _gear, int _amount)
        {
            ID = _id;
            gear = _gear;
            amount = _amount;
        }
        public void UpdateSlot(int _id, Gear _gear, int _amount)
        {
            ID = _id;
            gear = _gear;
            amount = _amount;
        }
        public void AddAmount(int value)
        {
            amount += value;
        }
    }
}

