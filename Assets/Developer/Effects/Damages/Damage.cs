namespace BulletRPG.Effects
{
    public class Damage
    {
        public float amount;
        public DamageType damageType;
        public Damage(float amountOfDamage, DamageType typeOfDamage)
        {
            amount = amountOfDamage;
            damageType = typeOfDamage;
        }
    }
}
