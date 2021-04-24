using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    private Spawner _evCtrl = null;
    void OnEnable()
    {
        _evCtrl = (Spawner)target;        
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Delayed Start", GUILayout.Width(150));
        _evCtrl.DelayStart = EditorGUILayout.Toggle(_evCtrl.DelayStart);
        if (_evCtrl.DelayStart)
        {
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Seconds", GUILayout.Width(150));
            _evCtrl.startAfterDelaySeconds = EditorGUILayout.FloatField(_evCtrl.startAfterDelaySeconds);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Destroy After", GUILayout.Width(150));
        _evCtrl.destroyAfter = EditorGUILayout.Toggle(_evCtrl.destroyAfter);
        if (_evCtrl.destroyAfter)
        {
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Seconds", GUILayout.Width(150));
            _evCtrl.destroyAfterSeconds = EditorGUILayout.FloatField(_evCtrl.destroyAfterSeconds);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
        }
        GUILayout.EndHorizontal();
    }
}