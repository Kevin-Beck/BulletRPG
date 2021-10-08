using BulletRPG.Items;
using UnityEngine;


namespace BulletRPG.Characters.Player
{
    /// <summary>
    /// Driver of the player statistics, controls elements of the other scripts
    /// </summary>
    public class PlayerStats : MonoBehaviour
    {
        public InventoryObject EquippedItems;

        IMove movement;
        IHealth health;

        public Attribute[] attributes;

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


            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].SetParent(this);
            }
            for (int i = 0; i < EquippedItems.GetSlots.Length; i++)
            {
                EquippedItems.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
                EquippedItems.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
            }
        }
        public void OnBeforeSlotUpdate(InventorySlot _slot)
        {
            if(_slot == null || _slot.gear == null || _slot.gear.Id < 0)
            {
                return;
            }
            Debug.Log($"Removed {_slot.gear.Name} on slot that allows this many types: {string.Join(", ",_slot.AllowedGear)}");
            for (int i = 0; i < _slot.gear.buffs.Length; i++)
            {
                for (int j = 0; j < attributes.Length; j++)
                {
                    if (attributes[j].attribute == _slot.gear.buffs[i].attribute)
                    {
                        attributes[j].value.RemoveModifier(_slot.gear.buffs[i]);
                    }
                }
            }
        }
        public void OnAfterSlotUpdate(InventorySlot _slot)
        {
            if (_slot == null || _slot.gear == null || _slot.gear.Id < 0)
            {
                return;
            }
            Debug.Log($"Placed {_slot.gear.Name} on slot that allows these types: {string.Join(", ", _slot.AllowedGear)}");
            for(int i = 0; i < _slot.gear.buffs.Length; i++)
            {
                for (int j = 0; j < attributes.Length; j++)
                {
                    if(attributes[j].attribute == _slot.gear.buffs[i].attribute)
                    {
                        attributes[j].value.AddModifier(_slot.gear.buffs[i]);
                    }
                }
            }
        }

        public void AttributeModified(Attribute attribute)
        {
            Debug.Log("Attribute was updated: " + attribute.attribute + ": " + attribute.value.ModifiedValue);
        }
        public void EquipmentUpdated()
        {
            // TODO FIX TRIPLE DAMAGE, re-sums all attributes and slots instead of just the slot that was actually modified
            for (int i = 0; i < EquippedItems.GetSlots.Length; i++)
            {
                for (int j = 0; j < EquippedItems.GetSlots[i].gear.buffs.Length; j++)
                {
                    for(int k = 0; k < attributes.Length; k++)
                    {
                        if(attributes[k].attribute == EquippedItems.GetSlots[i].gear.buffs[j].attribute)
                        {
                            if(EquippedItems.GetSlots[i].gear.Id != -1)
                            {
                                attributes[k].value.AddModifier(EquippedItems.GetSlots[i].gear.buffs[j]);
                            }                            
                        }
                    }
                }
            }
        }
    }
}

