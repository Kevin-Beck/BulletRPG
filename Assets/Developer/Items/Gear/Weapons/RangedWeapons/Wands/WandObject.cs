using UnityEngine;
namespace BulletRPG.Gear.Weapons.RangedWeapons.Wands
{
    [CreateAssetMenu(fileName = "newWand", menuName = "Item/Gear/Wand")]
    public class WandObject : RangedWeaponObject
    {
        [Header("WandObject Data")]
        public WandProjectiles projectiles;
        private void Awake()
        {
            gearSlot = GearSlot.Wand;
            config.itemType = ItemType.Gear;
        }
        public Wand CreateInstance(WandObject wandObject)
        {
            return new Wand(wandObject);
        }
    }

    [System.Serializable]
    public class Wand : RangedWeapon
    {        
        public Wand(WandObject wandObject) : base(wandObject)
        {            
            projectile = wandObject.projectiles.projectileMap[damage.type];
        }            
    }   
}