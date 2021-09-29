using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Item/Item Database")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public GearObject[] gearObjects;
        public Dictionary<int, ItemObject> GetGearObject = new Dictionary<int, ItemObject>();

        public void OnAfterDeserialize()
        {
            for(int i = 0; i < gearObjects.Length; i++)
            {
                Debug.Log("Database Saving: " + gearObjects[i].ItemName + " with id: " + i);
                gearObjects[i].Id = i;
                GetGearObject.Add(i, gearObjects[i]);
            }
        }

        public void OnBeforeSerialize()
        {
            GetGearObject = new Dictionary<int, ItemObject>();
        }
    }
}

