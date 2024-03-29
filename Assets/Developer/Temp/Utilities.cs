using BulletRPG.Gear;
using BulletRPG.Gear.Armor;
using BulletRPG.Gear.Weapons;
using BulletRPG.Gear.Weapons.RangedWeapons;
using System;
using System.Collections.Generic;
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
        private static int UILayer = LayerMask.NameToLayer("UI");

        public static void AddEvent(GameObject button, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = button.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }
        public static bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
        private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == UILayer)
                    return true;
            }
            return false;
        }

        //Gets all event system raycast results of current mouse or touch position.
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
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
        public static List<GearSlot> GetCharactersGearSlots(Transform characterRoot)
        {
            List<GearSlot> types = new List<GearSlot>();
            foreach (GearSlot gearType in (GearSlot[])System.Enum.GetValues(typeof(GearSlot)))
            {
                var equipmentPoint = RecursiveFindChild(characterRoot, gearType.ToString());
                if (equipmentPoint != null)
                {
                    types.Add(gearType);
                }
            }
            return types;
        }
        public static Transform GetPlayerTransform()
        {
            Transform position = GameObject.FindGameObjectWithTag("Player").transform;
            return position;
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
    }
}
