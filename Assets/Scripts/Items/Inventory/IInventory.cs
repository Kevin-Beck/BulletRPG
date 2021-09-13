using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{    public interface IInventory
    {
        public bool Add(Gear gear);
        public void Remove(Gear gear);
    }
}

