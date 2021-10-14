using BulletRPG.Characters;
using UnityEngine;

namespace BulletRPG.Gear
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
                    Debug.Log("TODO");
                    break;
                case HealType.Percentage:
                    health.HealPercentage(healAmount);
                    Debug.Log("TODO");
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

