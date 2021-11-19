using BulletRPG.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear.Weapons.RangedWeapons.Wands
{
    [System.Serializable]
    public class WandProjectile
    {
        public DamageType damageType;
        public GameObject damageProjectilePrefab;
    }

    [CreateAssetMenu(fileName = "WandProjectilePool", menuName = "Pool/Other/WandProjectiles")]
    public class WandProjectiles : ScriptableObject, ISerializationCallbackReceiver
    {
        public Dictionary<DamageType, GameObject> projectileMap = new Dictionary<DamageType, GameObject>();

        [SerializeField] private List<WandProjectile> projectileSet = new List<WandProjectile>();

        public void OnAfterDeserialize()
        {
            foreach (WandProjectile wp in projectileSet)
            {
                projectileMap.Add(wp.damageType, wp.damageProjectilePrefab);
            }
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}
