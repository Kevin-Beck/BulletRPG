using System;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public EquipmentSlot(Transform transform, GearSlots gearslot)
        {
            SpawnPoint = transform;
            GearSlot = gearslot;
        }
        public Transform SpawnPoint;
        public GearSlots GearSlot;
    }
    public class EquipmentManager : MonoBehaviour
    {
        [SerializeField] GameEvent playerInventoryChanged;
        public List<EquipmentSlot> myEquipmentSlots = new List<EquipmentSlot>();
        private Dictionary<GearSlots, Gear> equippedGear = new Dictionary<GearSlots, Gear>();

        IInventory myInventory;
        void Awake()
        {
            myEquipmentSlots.Add(new EquipmentSlot(Utilities.RecursiveFindChild(transform, "HeadSlot"), GearSlots.Head));
            myEquipmentSlots.Add(new EquipmentSlot(Utilities.RecursiveFindChild(transform, "LeftHandSlot"), GearSlots.OffHand));
            myEquipmentSlots.Add(new EquipmentSlot(Utilities.RecursiveFindChild(transform, "RightHandSlot"), GearSlots.MainHand));

            for (int i = myEquipmentSlots.Count - 1; i >= 0; i--)
            {
                if (myEquipmentSlots[i].SpawnPoint == null)
                {
                    myEquipmentSlots.RemoveAt(i);
                }
            }

            myInventory = GetComponent<IInventory>();

            foreach (EquipmentSlot equipmentSlot in myEquipmentSlots)
            {
                equippedGear.Add(equipmentSlot.GearSlot, null);
            }
        }
        public Gear GetGearInSlot(GearSlots slot)
        {
            return equippedGear[slot];
        }
        public bool Unequip(GearSlots slot)
        {
            // Unequip if something is there
            if (equippedGear[slot] != null)
            {
                if (myInventory.Add(equippedGear[slot]))
                {
                    Debug.Log($"Returned {slot} item to inventory");
                    foreach (EquipmentSlot equipmentSlot in myEquipmentSlots)
                    {
                        if (equipmentSlot.GearSlot == slot)
                        {
                            foreach (Transform child in equipmentSlot.SpawnPoint)
                            {
                                Destroy(child.gameObject);
                                Debug.Log($"Removing Visual Object in {slot}");
                            }
                            equippedGear[slot] = null;
                            playerInventoryChanged.Raise();
                            return true;
                        }
                    }
                }
            }
            Debug.Log($"Failed To return Item to inventory, exiting unequip");
            return false;
        }

        public bool Equip(Gear gear)
        {
            foreach (EquipmentSlot equipmentSlot in myEquipmentSlots)
            {
                if (equipmentSlot.GearSlot == gear.Slot)
                {
                    // Unequip if something is there
                    if (equippedGear[gear.Slot] != null)
                    {
                        Unequip(gear.Slot);
                    }

                    // Equip
                    equippedGear[gear.Slot] = gear;
                    Instantiate(gear.InGameObject, equipmentSlot.SpawnPoint.position, equipmentSlot.SpawnPoint.rotation, equipmentSlot.SpawnPoint);
                    return true;
                }
            }
            Debug.Log("Equip Failed");
            return false;
        }
    }
}

