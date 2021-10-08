using BulletRPG.Items;
using UnityEngine;


namespace BulletRPG.Characters.Player
{
    /// <summary>
    /// Driver of the player statistics, controls elements of the other scripts
    /// </summary>
    public class Player : MonoBehaviour
    {
        public InventoryObject EquippedItems;

        IMove movement;
        IHealth health;

        public Attribute[] attributes;
        CharacterStats baseStats; // stats based on this character

        private void Awake()
        {
            movement = GetComponent<IMove>();
            health = GetComponent<IHealth>();
            baseStats = GetComponentInChildren<CharacterStats>();
        }
        private void Start()
        {

            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].SetParent(this);
            }
            for (int i = 0; i < EquippedItems.GetSlots.Length; i++)
            {
                EquippedItems.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
                EquippedItems.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
            }


            // TODO loop through base stats and set the base value of the attributes to base stats
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].attributeType = baseStats.CharacterAttributes[i].attributeType;
                attributes[i].value.BaseValue = baseStats.CharacterAttributes[i].value.BaseValue;
            }
            ChangeFeaturesToMatchStats();


        }
        private void ChangeFeaturesToMatchStats()
        {
            foreach(Attribute attribute in attributes)
            {
                if(attribute.attributeType == AttributeType.Agility)
                {
                    movement.SetSpeedAndAcceleration(attribute.value.ModifiedValue, attribute.value.ModifiedValue);
                }else if(attribute.attributeType == AttributeType.Stamina)
                {
                    health.SetCurrentAndMaxHealth(attribute.value.ModifiedValue, attribute.value.ModifiedValue);
                }
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
                    if (attributes[j].attributeType == _slot.gear.buffs[i].attribute)
                    {
                        attributes[j].value.RemoveModifier(_slot.gear.buffs[i]);
                    }
                }
            }
            ChangeFeaturesToMatchStats();
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
                    if(attributes[j].attributeType == _slot.gear.buffs[i].attribute)
                    {
                        attributes[j].value.AddModifier(_slot.gear.buffs[i]);
                    }
                }
            }
            ChangeFeaturesToMatchStats();
        }

        public void AttributeModified(Attribute attribute)
        {
            Debug.Log("Attribute was updated: " + attribute.attributeType + ": " + attribute.value.ModifiedValue);
        }       
    }
}

