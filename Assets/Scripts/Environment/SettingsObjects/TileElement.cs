using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileElement", menuName = "Environment/TileElement")]
public class TileElement : ScriptableObject
{
    [SerializeField] public Color color = Color.cyan;
    [Tooltip("The list of elements this element will overwrite")]
    public List<TileElement> overwriteElements;
}
