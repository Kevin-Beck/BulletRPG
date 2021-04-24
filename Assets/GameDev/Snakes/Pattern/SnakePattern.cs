using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "snake pattern", menuName = "Snake/Pattern")]
public class SnakePattern : ScriptableObject
{
    public List<Vector3> waypoints;
}
