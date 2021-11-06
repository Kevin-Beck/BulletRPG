
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear
{
    [CreateAssetMenu(fileName = "DamageTypeColors", menuName = "Settings/DamageColors")]
    public class DamageTypeColors : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<DamageColorGroup> colorgroup = new List<DamageColorGroup>();
        public Dictionary<DamageType, Color> DamageColorMap = new Dictionary<DamageType, Color>();

        public void OnBeforeSerialize()
        {

        }
        public void OnAfterDeserialize()
        {
            if(colorgroup.Count > 0)
            {
                foreach (DamageColorGroup dcg in colorgroup)
                {
                    DamageColorMap.Add(dcg.damageType, dcg.color);
                }
            }
        }
    }
    [System.Serializable]
    public struct DamageColorGroup
    {
        public Color color;
        public DamageType damageType;
    }

}