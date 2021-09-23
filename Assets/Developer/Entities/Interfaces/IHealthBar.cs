using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters
{
    public interface IHealthBar
    {
        public void UpdateHealthBar(float percentage);
    }

}