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

        public bool AddItem(Item _item, int _amount)
        {
            // TODO inventory is casting the gear as an item and we lose Gear specific stats and instead get item
            //// if the item is not stackable
            //if (_item.itemType == ItemType.Gear && ((Gear)_item).buffs.Length > 0)
            //{
            //    Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
            //    Debug.Log($"Added {_amount}x {_item.Name} to new stack because it is unstackable");
            //    PlayerInventoryChanged?.Invoke();
            //    return true;
            //}

            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].ID == _item.Id)
                {
                    Container.Items[i].AddAmount(_amount);
                    PlayerInventoryChanged?.Invoke();
                    return true;
                }
            }
            AddToFirstEmptySlot(_item, _amount);
            return true;
        }
        private InventorySlot AddToFirstEmptySlot(Item item, int amount)
        {
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].ID <= -1)
                {
                    Container.Items[i].UpdateSlot(item.Id, item, amount);
                    Debug.Log($"Added {amount}x {item} to new stack");
                    PlayerInventoryChanged?.Invoke();
                    return Container.Items[i];
                }
            }
            // full inventory here!
            Debug.Log("Full Inventory not handled");
            return null;
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
            Container.Items = new InventorySlot[Container.Items.Length];
            PlayerInventoryChanged.Invoke();
        }
    }
    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] Items = new InventorySlot[21];
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

