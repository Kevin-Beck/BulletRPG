using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    //override public void OnInspectorGUI()
    //{
    //    var spawner = target as Spawner;
    //    string[] ignore = {"delayStart", "startAfterDelaySeconds", "destroyAfter", "destroyAfterSeconds" };
    //    DrawPropertiesExcluding(serializedObject, ignore);

    //    GUILayout.BeginHorizontal();
    //    spawner.delayStart = GUILayout.Toggle(spawner.delayStart, "Delay Start");

    //    if (spawner.delayStart)
    //        spawner.startAfterDelaySeconds = EditorGUILayout.FloatField(spawner.startAfterDelaySeconds);
    //    GUILayout.EndHorizontal();

    //    GUILayout.BeginHorizontal();
    //    spawner.destroyAfter = GUILayout.Toggle(spawner.destroyAfter, "Destroy After");

    //    if (spawner.destroyAfter)
    //        spawner.destroyAfterSeconds = EditorGUILayout.FloatField(spawner.destroyAfterSeconds);
    //    GUILayout.EndHorizontal();
    //}
}
