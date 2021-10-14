using BulletRPG.Gear;
using BulletRPG.Gear.Armor;
using BulletRPG.Gear.Weapons;
using BulletRPG.Gear.Weapons.RangedWeapons;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BulletRPG
{
    public static class Utilities
    {
        private static Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
        private static readonly Camera main = Camera.main;

        public static void AddEvent(GameObject button, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = button.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }
        public static Vector3 MouseOnPlane()
        {// calculates the intersection of a ray through the mouse pointer with a static x/z plane for example for movement etc, source: http://unifycommunity.com/wiki/index.php?title=Click_To_Move
            Ray mouseray = main.ScreenPointToRay(Input.mousePosition);
            if (xzPlane.Raycast(mouseray, out float hitdist))
            {// check for the intersection point between ray and plane
                return mouseray.GetPoint(hitdist);
            }
            if (hitdist < -1.0f)
            {// when point is "behind" plane (hitdist != zero, fe for far away orthographic camera) simply switch sign https://docs.unity3d.com/ScriptReference/Plane.Raycast.html
                return mouseray.GetPoint(-hitdist);
            }     // both are parallel or plane is behind camera so write a log and return zero vector
            return Vector3.zero;
        }
        public static bool GetRandomPointOnNavMesh(Vector3 center, float radius, out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * radius;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }
        public static Transform RecursiveFindChild(Transform parent, string childName)
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
        public static Tuple<string, string> NameAndDescribe(Item item)
        {
            string description = item.description;
            string name = item.name;

            var gear = item as Gear.Gear;
            string buffDescriptions = "";

            if (gear != null)
            {
                foreach (GearBuff buff in gear.buffs)
                {
                    buffDescriptions += buff.Stringify() + "\n";
                }
                description += $"\n\n{buffDescriptions}";

                if(gear.gearSlot == GearSlot.Wand || gear.gearSlot == GearSlot.Javelin || gear.gearSlot == GearSlot.Bow)
                {
                    var rangedWeapon = item as RangedWeapon;
                    if(rangedWeapon != null)
                    {
                        description += $"\nReload Time: {rangedWeapon.coolDown}\nBullet Speed: {rangedWeapon.projectileSpeed}\n";
                        description += $"\n{rangedWeapon.damage.Stringify()}";
                        name += $" of {rangedWeapon.damage.type}";
                    }
                }

                if(gear.gearSlot == GearSlot.Helmet)
                {
                    var armor = item as Armor;
                    foreach(DamageMitigator dm in armor.damageMitigators)
                    {
                        description += $"\n{dm.Stringify()}";
                        name += $" of {dm.damageType} Protection";
                    }
                }
            }

            return new Tuple<string, string>(name, description);
        }
    }
}
