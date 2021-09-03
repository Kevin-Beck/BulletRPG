using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BulletRPG.NPCBehavior;

[CustomEditor(typeof(Shoot))]
public class ShootEditor : Editor
{
    private void OnSceneGUI()
    {
        var shoot = target as Shoot;
        GUIStyle phaseStyle = new GUIStyle();
        phaseStyle.normal.textColor = Color.blue;
        

        if (shoot != null)
        {
            if(shoot.phaseConfig != null && shoot.phaseConfig.name != null)
            {
                Handles.Label(Vector3.up * 3.5f + shoot.transform.position, $"Shooting: {shoot.phaseConfig.name}", phaseStyle);
            }


            var firingConfig = shoot.firingSettings;
            if (firingConfig != null && shoot.target != null)
            {
                Handles.color = new Color(1, 0.8f, 0.4f, 1);
                Handles.DrawDottedLine(shoot.transform.position, shoot.target.position, 8f);
                Handles.color = Color.red;
                Handles.DrawWireDisc(shoot.target.position, Vector3.up, shoot.target.localScale.x + shoot.target.localScale.z);

                Vector3 thirdway = shoot.transform.position + (shoot.target.position - shoot.transform.position) / 2f;
                GUIStyle style = new GUIStyle();
                
                style.normal.textColor = Color.green;
                Handles.Label(thirdway, 
                    $"FireRate: {firingConfig.fireRate}{System.Environment.NewLine}" +
                    $"FireSequence: {firingConfig.firingSequence}{System.Environment.NewLine}", style);

                Vector3 partway = shoot.transform.position + (shoot.target.position - shoot.transform.position) / 1.3f;
                if (shoot.projectileGroup != null) {
                    Handles.Label(partway,
                    $"Projectiles: {shoot.projectileGroup.name}{System.Environment.NewLine}" +
                    $"Speed: {firingConfig.projectileSpeed}{System.Environment.NewLine}" +
                    $"Spin: {firingConfig.projectileSpin}{System.Environment.NewLine}", style);
                }
            }
        }
       
    }
}
