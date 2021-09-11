using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters
{    
    public interface IHealth
    {
        public void HealFlatAmount(float amount);
        public void HealPercentage(float percentage);
        public void TakeDamageAmount(float amount);
        public void TakeDamagePercentage(float percentage);

        public void AddHealthBar(IHealthBar healthBar);
    }
}

