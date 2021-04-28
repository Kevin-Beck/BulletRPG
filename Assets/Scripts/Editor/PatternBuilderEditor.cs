using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatternBuilder))]
public class PatternBuilderEditor : Editor
{
    private PatternBuilder patternBuilder;
    List<Vector3> positions;



    // Disables the transform of the pattern builder, we dont want it to be moved around, just left in the center
    Tool LastTool = Tool.None;
    void OnEnable()
    {
        LastTool = Tools.current;
        Tools.current = Tool.None;
    }
    void OnDisable()
    {
        Tools.current = LastTool;
    }


    private void OnSceneGUI()
    {
        patternBuilder = (PatternBuilder)target;
        positions = patternBuilder.waypoints;
        Handles.color = Color.cyan;
        if (positions != null)
        {
            for (int i = 1; i < positions.Count + 1; i++)
            {
                var previousPoint = positions[i - 1];
                var currentPoint = positions[i % positions.Count];

                Handles.DrawLine(previousPoint, currentPoint, 8f);
            }

            for (int i = 0; i < positions.Count; i++)
            {
                if (i == 0)
                {
                    Handles.color = Color.green;
                    Handles.DrawWireCube(positions[i], 0.75f * Vector3.one);
                }
                else if (i == positions.Count - 1)
                {
                    Handles.color = Color.blue;
                    Handles.DrawWireCube(positions[i], 0.75f * Vector3.one);
                }
                positions[i] = Handles.PositionHandle(positions[i], Quaternion.identity);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Space(20f);
        GUILayout.Label("Pattern Builder Tool", EditorStyles.boldLabel);
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Drag Layout to Start or click Add Waypoint Below");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        DropAreaGUI();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Waypoint"))
        {
            if (positions.Count == 0)
            {
                positions.Add(Vector3.zero);
            }
            else
            {
                positions.Add(positions[positions.Count - 1] + Vector3.forward);
            }
            SceneView.RepaintAll();
        }
        if (GUILayout.Button("Reset"))
        {
            if (positions.Count > 0 && EditorUtility.DisplayDialog("Reset Waypoints", $"Are you sure you want to delete all { positions.Count} waypoints?", "Delete Waypoints", "Keep Waypoints"))
            {
                positions.Clear();
                SceneView.RepaintAll();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(20f);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save Pattern"))
        {
            if (positions.Count < 2)
            {
                EditorUtility.DisplayDialog("Save Pattern", "This pattern does not contain enough waypoints! Add more waypoints by clicking the Add Waypoint button in the tool.", "OK");
            }
            else
            {
                Pattern pattern = CreateInstance<Pattern>();
                pattern.waypoints = positions;

                string name = AssetDatabase.GenerateUniqueAssetPath("Assets/GameDev/Patterns/NewPattern.asset");
                AssetDatabase.CreateAsset(pattern, name);
                AssetDatabase.SaveAssets();


                EditorUtility.FocusProjectWindow();
                Selection.activeObject = pattern;
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(20f);
        DrawDefaultInspector();
    }

    public void DropAreaGUI()
    {
        Event evt = Event.current;
        Rect drop_area = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(drop_area, "Drop Pattern Here to Start");

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!drop_area.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Pattern dragged_object in DragAndDrop.objectReferences)
                    {
                        foreach (Vector3 vector in dragged_object.waypoints)
                            patternBuilder.waypoints.Add(vector);
                    }
                    SceneView.RepaintAll();
                }
                break;
        }
    }
}
