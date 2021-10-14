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
        public InventoryObject EquippedGearSlots;
        Dictionary<InventorySlot, Transform> EquipmentSlotPositionMap = new Dictionary<InventorySlot, Transform>();

        IMove movement;
        IHealth health;

        public Attribute[] attributes;
        CharacterStats baseStats; // stats based on the Selected Character

        private void Awake()
        {
            EquippedGearSlots.Clear();
            SetEquipmentSlots();

            movement = GetComponent<IMove>();
            health = GetComponent<IHealth>();
            baseStats = GetComponentInChildren<CharacterStats>();
        }
        private void SetEquipmentSlots()
        {
            int count = 0;
            List<GearSlot> types = new List<GearSlot>();
            foreach (GearSlot gearType in (GearSlot[])System.Enum.GetValues(typeof(GearSlot)))
            {
                var equipmentPoint = Utilities.RecursiveFindChild(transform, gearType.ToString());
                if (equipmentPoint != null)
                {
                    types.Add(gearType);
                    count++;
                }
            }
            Debug.Log("Player found " + count + " equipment slots on the character model");
            EquippedGearSlots.SetSize(count);
            for(int i = 0; i < EquippedGearSlots.GetSlots.Length; i++)
            {
                EquippedGearSlots.GetSlots[i].InitializeAllowedItemTypes(new ItemType[] { ItemType.Gear });
                EquippedGearSlots.GetSlots[i].InitializeAllowedGearTypes(new GearSlot[] { types[i] });
                EquipmentSlotPositionMap.Add(EquippedGearSlots.GetSlots[i], Utilities.RecursiveFindChild(transform, types[i].ToString()));
            }
        }
        private void Start()
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].SetParent(this);
            }
            for (int i = 0; i < EquippedGearSlots.GetSlots.Length; i++)
            {
                EquippedGearSlots.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
                EquippedGearSlots.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
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
            if(_slot == null || _slot.item == null || _slot.item.Id < 0)
            {
                return;
            }
            Debug.Log($"Removed {_slot.item.name} on slot that allows these GearTypes: {string.Join(", ",_slot.AllowedGearTypes)} and these ItemTypes: {string.Join(", ", _slot.AllowedItemTypes)}");
            var gear = _slot.item as BulletRPG.Gear.Gear;
            for (int i = 0; i < gear.buffs.Length; i++)
            {
                for (int j = 0; j < attributes.Length; j++)
                {
                    if (attributes[j].attributeType == gear.buffs[i].attribute)
                    {
                        attributes[j].value.RemoveModifier(gear.buffs[i]);
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
            if (_slot == null || _slot.item == null || _slot.item.Id < 0)
            {
                return;
            }
            var gear = _slot.item as Gear.Gear;
            Debug.Log($"Placed {gear.name} on slot that allows these gear types: {string.Join(", ", _slot.AllowedGearTypes)}");
            for(int i = 0; i < gear.buffs.Length; i++)
            {
                for (int j = 0; j < attributes.Length; j++)
                {
                    if(attributes[j].attributeType == gear.buffs[i].attribute)
                    {
                        attributes[j].value.AddModifier(gear.buffs[i]);
                    }
                }
            }
            ChangeFeaturesToMatchStats();
            var gearObject = (GearObject)(EquippedGearSlots.database.GetItemObject[gear.Id]);
            var inGameItem = gearObject.EquippedInGameObject;


            // TODO equip all item slots like this
            var equipment = Instantiate(inGameItem, EquipmentSlotPositionMap[_slot]);
            if(gear.gearSlot == GearSlot.Wand)
            {
                var weaponShoot = equipment.GetComponent<WeaponShoot>();
                weaponShoot.SetWeapon((RangedWeapon)gear);
            }
        }

        public void AttributeModified(Attribute attribute)
        {
            Debug.Log("Attribute was updated: " + attribute.attributeType + ": " + attribute.value.ModifiedValue);
        }       
    }
}

