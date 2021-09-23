using BulletRPG.Characters;
using UnityEngine;

namespace BulletRPG.Items.Powerups
{
    public class ChangeHealth : Interactable
    {
        [SerializeField] float healAmount;
        [SerializeField] HealType healType;
        public override void Interact(Interactor interactor)
        {
            var health = interactor.GetComponent<IHealth>();
            switch (healType)
            {
                case HealType.Flat:
                    health.HealFlatAmount(healAmount);
                    break;
                case HealType.Percentage:
                    health.HealPercentage(healAmount);
                    break;
                default:
                    Debug.LogWarning("Fell out of HealTypeSwitch");
                    break;
            }
            Destroy(gameObject);
        }
    }
    public enum HealType
    {
        Flat,
        Percentage
    }
}

