using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear
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
                gearObjects[i].Id = i;
                GetGearObject.Add(i, gearObjects[i]);
                if(i == gearObjects.Length - 1)
                {
                    Debug.Log("Database Saved " + gearObjects.Length + " total objects, the last being: " + gearObjects[gearObjects.Length-1].ItemName + " with id " + (gearObjects.Length-1));
                }
            }
        }

        public void OnBeforeSerialize()
        {
            GetGearObject = new Dictionary<int, GearObject>();
        }
    }
}

