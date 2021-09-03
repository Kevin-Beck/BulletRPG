using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletRPG.NPCBehavior;

namespace BulletRPG.NPCBehavior
{
    public class Movement : NPCBehavior
    {
        public Pattern pattern;
        public Vector3 patternOffset;
        public float speed = 4;
        private int currentWaypoint = 0;
        public bool startAtFirstPoint = true;
        public bool reverseDirection = false;

        private void Start()
        {
            if (speed == 0)
            {
                Debug.LogWarning($"Movement on {gameObject} is set to 0.");
            }
            if (pattern.waypoints.Count == 0 || pattern.waypoints.Count == 1)
            {
                Debug.LogWarning($"Movement on {gameObject} is set to 0 or 1. Patter:{pattern} must have more points for movement to function as intended.");
            }
            if (startAtFirstPoint)
            {
                transform.position = pattern.waypoints[0] + patternOffset;
            }
            else
            {
                // find the nearest point in the pattern and move there to start movement
                int closestIndex = -1;
                float closestSquareMagnitude = 1000000f;
                for (int i = 0; i < pattern.waypoints.Count; i++)
                {
                    var currentSquareMagnitude = Vector3.SqrMagnitude(transform.position - pattern.waypoints[i]);
                    if (currentSquareMagnitude < closestSquareMagnitude)
                    {
                        closestIndex = i;
                        closestSquareMagnitude = currentSquareMagnitude;
                    }
                }

                transform.position = pattern.waypoints[closestIndex] + patternOffset;
                currentWaypoint = closestIndex;
            }
        }
        private void Update()
        {
            // If we're close, get next point
            if (Vector3.Distance(transform.position, pattern.waypoints[currentWaypoint] + patternOffset) < .1f)
            {
                if (reverseDirection)
                {
                    currentWaypoint--;
                    if (currentWaypoint < 0)
                    {
                        // if we're out of points start over
                        currentWaypoint = pattern.waypoints.Count - 1;
                    }
                }
                else
                {
                    currentWaypoint++;
                    if (currentWaypoint >= pattern.waypoints.Count)
                    {
                        // if we're out of points start over
                        currentWaypoint = 0;
                    }
                }
            }
            // walk towards point        
            transform.position = Vector3.MoveTowards(transform.position, pattern.waypoints[currentWaypoint] + patternOffset, Time.deltaTime * speed);
        }
        public void SetSpeedMultiplier(float speedMultiplier, float revertAfterSeconds) // 0 for permanent change
        {
            var currentSpeed = speed;
            var newSpeed = speed * speedMultiplier;

            StartCoroutine(SetSpeed(currentSpeed, revertAfterSeconds));
        }
        IEnumerator SetSpeed(float speedValue, float timeDelay)
        {
            yield return new WaitForSeconds(timeDelay);
            speed = speedValue;
        }
    }
}