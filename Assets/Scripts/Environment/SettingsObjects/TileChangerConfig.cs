using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileChangerConfig", menuName = "NPC/TileChangerConfig")]
public class TileChangerConfig : ScriptableObject
{
    public float size = 1f;   
    public ChangerType type;

    public enum ChangerType
    {
        Raiser,
        Freezer,
    }
}