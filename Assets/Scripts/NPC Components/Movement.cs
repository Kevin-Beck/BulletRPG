using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletRPG.NPCBehavior;

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
        if(speed == 0)
        {
            Debug.LogWarning($"Movement on {gameObject} is set to 0.");
        }
        if (startAtFirstPoint)
        {
            transform.position = pattern.waypoints[0] + patternOffset;
        }else
        {
            int closestIndex = -1;
            float closestSquareMagnitude = 1000000f;
            for(int i = 0; i < pattern.waypoints.Count; i++)
            {
                var currentSquareMagnitude = Vector3.SqrMagnitude(transform.position - pattern.waypoints[i]);
                if ( currentSquareMagnitude < closestSquareMagnitude)
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
}
