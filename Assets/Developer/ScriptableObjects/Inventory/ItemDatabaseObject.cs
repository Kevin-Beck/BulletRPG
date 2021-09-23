using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Item/Item Database")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] Items;
        public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();
        public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

        public void OnAfterDeserialize()
        {
            GetId = new Dictionary<ItemObject, int>();
            GetItem = new Dictionary<int, ItemObject>();

            for(int i = 0; i < Items.Length; i++)
            {
                GetItem.Add(i, Items[i]);
                GetId.Add(Items[i], i);
            }
        }

        public void OnBeforeSerialize()
        {
        }
    }
}

