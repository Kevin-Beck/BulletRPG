using UnityEngine;
using UnityEditor;
using BulletRPG.Player;

[CustomEditor(typeof(Move))]
public class MoveEditor : Editor
{
    private Move move;

    private void OnSceneGUI()
    {
        move = (Move)target;

        Handles.color = Color.blue;
        if(move.navMeshAgent != null)
        {
                Handles.DrawLine(move.transform.position-new Vector3(0, move.navMeshAgent.baseOffset, 0), move.navMeshAgent.destination, 16f);
        }
    }
}
