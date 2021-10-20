using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName = "lootableItemTable", menuName = "Settings/LootableItemTable")]
    public class LootableItemTable : ScriptableObject, ISerializationCallbackReceiver
    {
        public Dictionary<ItemType, GameObject> lootableItemMap = new Dictionary<ItemType, GameObject>();
        public void OnBeforeSerialize()
        {

        }
        public void OnAfterDeserialize()
        {
            foreach (ItemLootables il in lootables)
            {
                lootableItemMap.Add(il.itemType, il.lootableObject);
            }
        }
    }

    [System.Serializable]
    public struct ItemLootables
    {
        public ItemType itemType;
        public GameObject lootableObject;
    }
}

