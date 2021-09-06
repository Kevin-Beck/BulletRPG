using UnityEngine;
using UnityEditor;
using BulletRPG.NPCBehavior;

[CustomEditor(typeof(PlayerMove))]
public class PlayerMoveEditor : Editor
{
    private PlayerMove playerMove;

    private void OnSceneGUI()
    {
        playerMove = (PlayerMove)target;

        Handles.color = Color.blue;
        if(playerMove.navMeshAgent != null)
        {
                Handles.DrawLine(playerMove.transform.position-new Vector3(0, playerMove.navMeshAgent.baseOffset, 0), playerMove.navMeshAgent.destination, 16f);
        }
    }
}
