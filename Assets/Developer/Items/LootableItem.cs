using BulletRPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear
{
    public class LootableItem : MonoBehaviour
    { 
        public ItemObject itemObject;
        public Item setItem;
        public int amount = 1;
    }
}
