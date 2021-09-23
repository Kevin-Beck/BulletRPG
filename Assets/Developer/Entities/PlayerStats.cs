using BulletRPG.Items;
using UnityEngine;


namespace BulletRPG.Characters.Player
{
    /// <summary>
    /// Driver of the player statistics, controls elements of the other scripts
    /// </summary>
    public class PlayerStats : MonoBehaviour
    {
        IMove movement;
        IHealth health;
        IInventoryManager inventoryManager;

        [SerializeField] StatBlockObject currentStats; // changes with upgrades etc
        CharacterStats baseStats; // stats based on this character

        private void Awake()
        {
            movement = GetComponent<IMove>();
            health = GetComponent<IHealth>();
            baseStats = GetComponentInChildren<CharacterStats>();
        }
        private void Start()
        {
            if(currentStats == null)
            {
                Debug.Log("No object for currentstats on player");
                return;
            }
            if(!currentStats.Initialized)
            {
                Debug.Log("Inheriting from base character statistics.");
                currentStats.BecomeCopy(baseStats.Statblock);
            }
            movement.SetSpeedAndAcceleration(currentStats.Speed, currentStats.Acceleration);
            health.SetCurrentAndMaxHealth(currentStats.CurrentHealth, currentStats.MaxHealth);
        }

    }
}

