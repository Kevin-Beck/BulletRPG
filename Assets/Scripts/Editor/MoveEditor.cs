using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BulletRPG.Characters.NPC;

[CustomEditor(typeof(Move))]
public class MoveEditor : Editor
{
    private Move movement;

    private void OnSceneGUI()
    {
        movement = (Move)target;

        Handles.color = Color.cyan;
        if(movement.pattern != null)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.blue;
            Handles.Label(Vector3.up*3 +  movement.transform.position, $"Movement: {movement.phaseConfig.name}", style);


            var positions = movement.pattern.waypoints;

            for (int i = 1; i < positions.Count + 1; i++)
            {
                var previousPoint = positions[i - 1] + movement.patternOffset;
                var currentPoint = positions[i % positions.Count] + movement.patternOffset;

                Handles.DrawLine(previousPoint, currentPoint, 6f);
            }

            int startingPoint = CalculateStartingIndex();
            
            for (int i = 0; i < positions.Count; i++)
            {
                if (i == startingPoint)
                {
                    Handles.color = Color.green;
                    Handles.DrawWireCube(positions[i], 1f * Vector3.one);
                    if (positions.Count > 1 && !movement.reverseDirection)
                    {
                        Handles.DrawLine(positions[i] + movement.patternOffset, positions[(i+1)%positions.Count] + movement.patternOffset, 8f);
                    }else if(positions.Count > 1 && movement.reverseDirection)
                    {
                        if(i == 0)
                        {
                            Handles.DrawLine(positions[i] + movement.patternOffset, positions[positions.Count - 1] + movement.patternOffset, 8f);
                        }else
                        {
                            Handles.DrawLine(positions[i] + movement.patternOffset, positions[i - 1] + movement.patternOffset, 8f);
                        }
                    }
                }
            }
        }        
    }    

    private int CalculateStartingIndex()
    {
        if (movement.startAtFirstPoint)
        {
            return 0;
        }
        else
        {
            int closestIndex = -1;
            float closestSquareMagnitude = 1000000f;
            for (int i = 0; i < movement.pattern.waypoints.Count; i++)
            {
                var currentSquareMagnitude = Vector3.SqrMagnitude(movement.transform.position - movement.pattern.waypoints[i]);
                if (currentSquareMagnitude < closestSquareMagnitude)
                {
                    closestIndex = i;
                    closestSquareMagnitude = currentSquareMagnitude;
                }
            }
            return closestIndex;
        }
    }
}
