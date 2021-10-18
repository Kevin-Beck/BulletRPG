using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear
{
    [CreateAssetMenu(fileName = "RarityColorMap", menuName = "Settings/RarityColors")]
    public class RarityColors : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<RarityColorGroup> colorgroup = new List<RarityColorGroup>();
        public Dictionary<Rarity, Color> RarityColorMap = new Dictionary<Rarity, Color>();

        public void OnBeforeSerialize()
        {

        }
        public void OnAfterDeserialize()
        {
            foreach (RarityColorGroup rcg in colorgroup)
            {
                RarityColorMap.Add(rcg.rarity, rcg.color);
            }
        }
    }
    [System.Serializable]
    public struct RarityColorGroup
    {
        public Color color;
        public Rarity rarity;
    }
    
}

