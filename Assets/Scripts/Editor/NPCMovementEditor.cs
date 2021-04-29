using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Movement))]
public class NPCMovementEditor : Editor
{
    private Movement npcMovement;

    private void OnSceneGUI()
    {
        npcMovement = (Movement)target;

        Handles.color = Color.cyan;
        if(npcMovement.pattern != null)
        {
            var positions = npcMovement.pattern.waypoints;

            for (int i = 1; i < positions.Count + 1; i++)
            {
                var previousPoint = positions[i - 1];
                var currentPoint = positions[i % positions.Count];

                Handles.DrawLine(previousPoint, currentPoint, 6f);
            }
            for (int i = 0; i < positions.Count; i++)
            {
                if (i == 0)
                {
                    Handles.color = Color.green;
                    Handles.DrawWireCube(positions[i], 0.75f * Vector3.one);
                    if (positions.Count > 1)
                    {
                        Handles.DrawLine(positions[i], positions[i + 1], 8f);
                    }
                }
                else
                {
                    Handles.color = Color.gray;
                    Handles.DrawWireDisc(positions[i], Vector3.up, 0.25f);
                }
            }
        }        
    }    
}
