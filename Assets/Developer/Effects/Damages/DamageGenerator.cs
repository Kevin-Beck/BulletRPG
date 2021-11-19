namespace BulletRPG.Effects
{
    [System.Serializable]
    public class DamageGenerator
    {
        public float min;
        public float max;
        public DamageType type;
        public DamageGenerator(float minimumDamage, float maximumDamage, DamageType damageType)
        {
            type = damageType;
            min = minimumDamage;
            max = maximumDamage;
        }
        public Damage GetDamage()
        {
            return new Damage(UnityEngine.Random.Range(min, max), type);
        }
        public string Stringify()
        {
            return $"Damage\n{type.DamageName}: {min:F1}-{max:F1}";
        }
    }
}

