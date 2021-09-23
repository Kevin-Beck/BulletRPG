using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Item/InventoryObject")]
    public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public string savePath;
        public ItemDatabaseObject database;
        public List<InventorySlot> Container = new List<InventorySlot>();
        public GameEvent inventoryChanged;
        public void AddItem(ItemObject _item, int _amount)
        {
            for (int i = 0; i < Container.Count; i++)
            {
                if(Container[i].item == _item)
                {
                    Container[i].AddAmount(_amount);
                    inventoryChanged.Raise();
                    return;
                }
            }
            Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
        }
        public void Save()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Create(string.Concat(Application.persistentDataPath, savePath));
            binaryFormatter.Serialize(fileStream, saveData);
            fileStream.Close();
        }
        public void Load()
        {
            if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(file).ToString(), this);
                file.Close();
                inventoryChanged.Raise();
            }
        }
        public void OnAfterDeserialize()
        {
            for(int i = 0; i < Container.Count; i++)
            {
                Container[i].item = database.GetItem[Container[i].ID];
            }
        }

        public void OnBeforeSerialize()
        {
        }
    }
    [System.Serializable]
    public class InventorySlot
    {
        public int ID;
        public ItemObject item;
        public int amount;
        public InventorySlot(int _id, ItemObject _item, int _amount)
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

