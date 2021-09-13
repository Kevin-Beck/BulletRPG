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
        public List<EquipmentSlot> myEquipmentSlots = new List<EquipmentSlot>();
        private Dictionary<GearSlots, Gear> equippedGear = new Dictionary<GearSlots, Gear>();

        IInventory myInventory;
    
        void Start()
        {
            
            myEquipmentSlots.Add(new EquipmentSlot(RecursiveFindChild(transform, "HeadSlot"), GearSlots.Head));
            myEquipmentSlots.Add(new EquipmentSlot(RecursiveFindChild(transform, "LeftHandSlot"), GearSlots.OffHand));
            myEquipmentSlots.Add(new EquipmentSlot(RecursiveFindChild(transform, "RightHandSlot"), GearSlots.MainHand));

            for(int i = myEquipmentSlots.Count-1; i >= 0; i--)
            {
                if (myEquipmentSlots[i].SpawnPoint == null) {
                    myEquipmentSlots.RemoveAt(i);
                }
            }

            myInventory = GetComponent<IInventory>();

            foreach(EquipmentSlot equipmentSlot in myEquipmentSlots)
            {
                equippedGear.Add(equipmentSlot.GearSlot, null);
            }
        }

        public void Equip(Gear gear)
        {
            foreach(EquipmentSlot equipmentSlot in myEquipmentSlots)
            {
                if(equipmentSlot.GearSlot == gear.Slot)
                {
                    // Unequip if something is there
                    if(equippedGear[gear.Slot] != null)
                    {
                        if (myInventory.Add(equippedGear[gear.Slot]))
                        {
                            Debug.Log($"Returned {equipmentSlot.GearSlot} item to inventory");
                            foreach (Transform child in equipmentSlot.SpawnPoint)
                            {
                                Destroy(child.gameObject);
                                Debug.Log($"Removing Visual Object in {gear.Slot}");
                            }
                            equippedGear[gear.Slot] = null;
                        }
                        else
                        {
                            Debug.Log($"Failed To return Item to inventory, exiting equip");
                            return;
                        }
                    }

                    // Equip
                    equippedGear[gear.Slot] = gear;
                    Instantiate(gear.VisualObject, equipmentSlot.SpawnPoint.position, equipmentSlot.SpawnPoint.rotation, equipmentSlot.SpawnPoint);
                }
            }
        }
        Transform RecursiveFindChild(Transform parent, string childName)
        {
            foreach (Transform child in parent)
            {
                if (child.name == childName)
                {
                    return child;
                }
                else
                {
                    Transform found = RecursiveFindChild(child, childName);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }
    }
}

