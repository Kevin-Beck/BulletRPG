using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory/Item Database")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] itemObjects;
        public Dictionary<int, ItemObject> GetItemObject = new Dictionary<int, ItemObject>();

        public void OnAfterDeserialize()
        {
            for(int i = 0; i < itemObjects.Length; i++)
            {                
                itemObjects[i].Id = i;
                GetItemObject.Add(i, itemObjects[i]);
                //Debug.Log("Items Database Set");
            }
        }

        public void OnBeforeSerialize()
        {
            GetItemObject = new Dictionary<int, ItemObject>();
        }
    }
}

