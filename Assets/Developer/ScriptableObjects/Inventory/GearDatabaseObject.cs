using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName = "New Gear Database", menuName = "Gear/Gear Database")]
    public class GearDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public GearObject[] gearObjects;
        public Dictionary<int, GearObject> GetGearObject = new Dictionary<int, GearObject>();

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
            GetGearObject = new Dictionary<int, GearObject>();
        }
    }
}

