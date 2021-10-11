using BulletRPG.Gear;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Characters.Player
{
    /// <summary>
    /// Driver of the player statistics, controls elements of the other scripts
    /// </summary>
    public class Player : MonoBehaviour
    {
        public InventoryObject EquippedItems;
        Dictionary<InventorySlot, Transform> EquipmentSlotPositionMap = new Dictionary<InventorySlot, Transform>();

        IMove movement;
        IHealth health;

        public Attribute[] attributes;
        CharacterStats baseStats; // stats based on this character

        private void Awake()
        {
            // TODO find all the slots in the player model, set equipment length to that amount
            // Set the equipment allowed objects to be what was found in the model
            // Currently EquippedItems is hard coded to inventory size, it needs to be
            // the length of the actual slots in our inventory
            EquippedItems.Clear();
            SetEquipmentSlots();
            // When we set the allowed gear in the InventorySlot also add it to
            // an equipmentslotmap that maps the InventorySlot to The equipmentLocation Transform
            
            // On Before SlotUpdate can Remove The children objects
            // On after slot update can add the in game object using the inventory database to fetch
            // the equipped game object

            // Currently the Equipment slot's allowed gear is being set by the Equipment UI
            // Remove that InitializeAllowedGear from that spot

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
            EquippedItems.SetSize(count);
            for(int i = 0; i < EquippedItems.GetSlots.Length; i++)
            {
                EquippedItems.GetSlots[i].InitializeAllowedGear(new GearType[] { types[i] });
                EquipmentSlotPositionMap.Add(EquippedItems.GetSlots[i], Utilities.RecursiveFindChild(transform, types[i].ToString()));
            }
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
            Instantiate(EquippedItems.database.GetGearObject[_slot.gear.Id].EquippedInGameObject, EquipmentSlotPositionMap[_slot]);
        }

        public void AttributeModified(Attribute attribute)
        {
            Debug.Log("Attribute was updated: " + attribute.attributeType + ": " + attribute.value.ModifiedValue);
        }       
    }
}

