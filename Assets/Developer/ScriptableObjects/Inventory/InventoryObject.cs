using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Item/InventoryObject")]
    public class InventoryObject : ScriptableObject
    {
        public string savePath;
        public ItemDatabaseObject database;
        public Inventory Container;
        public GameEvent inventoryChanged;
        public int InventoryMaxSize = 5;
        public bool AddItem(Item _item, int _amount)
        {
            if (Container.Items.Count >= InventoryMaxSize)
            {
                Debug.Log("Inventory is full");
                return false;
            }
            for (int i = 0; i < Container.Items.Count; i++)
            {
                if (Container.Items[i].item == _item)
                {
                    Container.Items[i].AddAmount(_amount);
                    Debug.Log($"Added {_amount}x {_item.Name} to existing stack");
                    inventoryChanged.Raise();
                    return true;
                }
            }

            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
            Debug.Log($"Added {_amount}x {_item.Name} to new stack");
            inventoryChanged.Raise();
            return true;
        }
        [ContextMenu("Save")]
        public void Save()
        {
            //string saveData = JsonUtility.ToJson(this, true);
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
            //bf.Serialize(file, saveData);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, Container);
            stream.Close();
            Debug.Log($"Inventory: {name} saved to: {savePath}");
        }
        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                //BinaryFormatter bf = new BinaryFormatter();
                //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                //file.Close();

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
                Container = (Inventory)formatter.Deserialize(stream);
                for (int i = 0; i < Container.Items.Count; i++)
                {
                    Container.Items[i].UpdateSlot(Container.Items[i].ID, Container.Items[i].item, Container.Items[i].amount);
                }
                stream.Close();
                Debug.Log($"Inventory: {name} loaded from: {savePath}");
                inventoryChanged.Raise();
            }
        }
        [ContextMenu("Clear")]
        public void Clear()
        {
            Container.Items.Clear();
            inventoryChanged.Raise();
        }
    }
    [System.Serializable]
    public class Inventory
    {
        public List<InventorySlot> Items= new List<InventorySlot>();
    }
    [System.Serializable]
    public class InventorySlot
    {
        public int ID;
        public Item item;
        public int amount;
        public InventorySlot()
        {
            ID = -1;
            item = null;
            amount = 0;
        }
        public InventorySlot(int _id, Item _item, int _amount)
        {
            ID = _id;
            item = _item;
            amount = _amount;
        }
        public void UpdateSlot(int _id, Item _item, int _amount)
        {
            ID = _id;
            item = _item;
            amount = _amount;
        }
        public void AddAmount(int value)
        {
            amount += value;
        }
    }
}

