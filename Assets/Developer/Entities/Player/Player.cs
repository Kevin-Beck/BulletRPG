using BulletRPG.Gear;
using BulletRPG.Gear.Weapons.RangedWeapons;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Characters.Player
{
    /// <summary>
    /// Driver of the player statistics, controls elements of the other scripts
    /// </summary>
    public class Player : MonoBehaviour
    {
        public InventoryObject EquippedItemSlots;
        Dictionary<InventorySlot, Transform> EquipmentSlotPositionMap = new Dictionary<InventorySlot, Transform>();

        IMove movement;
        IHealth health;

        public Attribute[] attributes;
        CharacterStats baseStats; // stats based on the Selected Character

        private void Awake()
        {
            EquippedItemSlots.Clear();
            SetEquipmentSlots();

            movement = GetComponent<IMove>();
            health = GetComponent<IHealth>();
            baseStats = GetComponentInChildren<CharacterStats>();
        }
        private void SetEquipmentSlots()
        {
            int count = 0;
            List<GearType> types = new List<GearType>();
            foreach (GearType gearType in (GearType[])System.Enum.GetValues(typeof(GearType)))
            {
                var equipmentPoint = Utilities.RecursiveFindChild(transform, gearType.ToString());
                if (equipmentPoint != null)
                {
                    types.Add(gearType);
                    count++;
                }
            }
            EquippedItemSlots.SetSize(count);
            for(int i = 0; i < EquippedItemSlots.GetSlots.Length; i++)
            {
                EquippedItemSlots.GetSlots[i].InitializeAllowedGear(new GearType[] { types[i] });
                EquipmentSlotPositionMap.Add(EquippedItemSlots.GetSlots[i], Utilities.RecursiveFindChild(transform, types[i].ToString()));
            }
        }
        private void Start()
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].SetParent(this);
            }
            for (int i = 0; i < EquippedItemSlots.GetSlots.Length; i++)
            {
                EquippedItemSlots.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
                EquippedItemSlots.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
            }

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
                }else if(attribute.attributeType == AttributeType.Endurance)
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
            foreach(Transform child in EquipmentSlotPositionMap[_slot])
            {
                Destroy(child.gameObject);
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
                    if(attributes[j].attributeType == _slot.gear.buffs[i].attribute)
                    {
                        attributes[j].value.AddModifier(_slot.gear.buffs[i]);
                    }
                }
            }
            ChangeFeaturesToMatchStats();
            var equipment = Instantiate(EquippedItemSlots.database.GetGearObject[_slot.gear.Id].EquippedInGameObject, EquipmentSlotPositionMap[_slot]);
            if(_slot.gear.gearType == GearType.Wand)
            {
                var weaponShoot = equipment.GetComponent<WeaponShoot>();
                weaponShoot.SetWeapon((RangedWeapon)_slot.gear);
            }
        }

        public void AttributeModified(Attribute attribute)
        {
            Debug.Log("Attribute was updated: " + attribute.attributeType + ": " + attribute.value.ModifiedValue);
        }       
    }
}

