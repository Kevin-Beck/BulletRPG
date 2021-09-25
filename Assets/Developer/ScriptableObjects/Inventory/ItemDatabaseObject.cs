using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Item/Item Database")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] Items;
        public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

        public void OnAfterDeserialize()
        {
            for(int i = 0; i < Items.Length; i++)
            {
                Debug.Log("Database Saving: " + Items[i].ItemName + " with id: " + i);
                Items[i].Id = i;
                GetItem.Add(i, Items[i]);
            }
        }

        public void OnBeforeSerialize()
        {
            GetItem = new Dictionary<int, ItemObject>();
        }
    }
}

