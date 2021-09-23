using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "StatBlockObject", menuName = "Character/StatBlockObject")]
    public class StatBlockObject : ScriptableObject
    {
        [SerializeField] public bool Initialized;
        [SerializeField] public float MaxHealth;
        [SerializeField] public float CurrentHealth;
        [SerializeField] public float DamageReduction;
        [SerializeField] public float Speed;
        [SerializeField] public float Acceleration;
        [SerializeField] public int InventorySize;

        public void BecomeCopy(StatBlockObject objectToCopy)
        {
            Initialized = true;
            MaxHealth = objectToCopy.MaxHealth;
            CurrentHealth = objectToCopy.CurrentHealth;
            DamageReduction = objectToCopy.DamageReduction;
            Speed = objectToCopy.Speed;
            Acceleration = objectToCopy.Acceleration;
            InventorySize = objectToCopy.InventorySize;
        }
    }
}
