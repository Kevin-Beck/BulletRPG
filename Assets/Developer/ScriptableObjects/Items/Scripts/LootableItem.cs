using BulletRPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public class LootableItem : MonoBehaviour
    { 
        public GearObject gearObject;
        public Gear setGear;
        public int amount = 1;
    }
}
