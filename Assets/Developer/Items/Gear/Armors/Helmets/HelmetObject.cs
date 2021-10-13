using UnityEngine;


namespace BulletRPG.Gear.Armor
{
    [CreateAssetMenu(fileName = "Helmet", menuName = "Item/Armor/Helmet")]
    public class HelmetObject : ArmorObject
    {
        private void Awake()
        {
            gearSlot = GearSlot.Helmet;
        }
    }
}
