using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileChangerConfig", menuName = "NPC/TileChangerConfig")]
public class TileChangerConfig : ScriptableObject
{
    public float size = 1f;   
    public ChangerType type;
    public float timeEffectStays = 3f;

    public enum ChangerType
    {
        Raiser,
        Freezer,
        Fire
    }
}