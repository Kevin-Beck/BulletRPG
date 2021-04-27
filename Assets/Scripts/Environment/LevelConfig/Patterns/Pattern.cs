using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Pattern", menuName = "Pattern")]
public class Pattern : ScriptableObject
{
    public List<Vector3> waypoints;
}
