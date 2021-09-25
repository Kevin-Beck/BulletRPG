using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Items.Armor
{
    [CreateAssetMenu(fileName = "Helmet", menuName = "Item/Armor/Helmet")]
    public class HelmetObject : ArmorObject
    {
        private void Awake()
        {
            Slot = GearSlots.Head;
        }
    }
}
