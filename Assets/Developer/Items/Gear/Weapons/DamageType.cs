
using System;
using UnityEngine;

namespace BulletRPG.Gear
{
    [CreateAssetMenu(fileName = "New Damage Type", menuName = "Type/DamageType")]
    public class DamageType : ScriptableObject
    {
        [SerializeField] private string _damageName;
        public string DamageName { get => _damageName; }

    }
}

