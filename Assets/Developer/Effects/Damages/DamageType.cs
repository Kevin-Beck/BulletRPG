using UnityEngine;

namespace BulletRPG.Effects
{
    [CreateAssetMenu(fileName = "New Damage Type", menuName = "Type/DamageType")]
    public class DamageType : ScriptableObject
    {
        [SerializeField] private string _damageName;
        public string DamageName { get => _damageName; }
    }
}

