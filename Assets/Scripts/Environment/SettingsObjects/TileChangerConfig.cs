using UnityEngine;

[CreateAssetMenu(fileName = "TileChangerConfig", menuName = "NPC/TileChangerConfig")]
public class TileChangerConfig : ScriptableObject
{
    public float size = 1f;   
    public TileElement tileElement;
    public float timeEffectStays = 3f;
}