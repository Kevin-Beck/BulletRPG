using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SnakeConfig", menuName = "Snake/SnakeConfig")]
public class SnakeConfig : ScriptableObject
{
    public float size = 1f;   
    public SnakeType type;

    public enum SnakeType
    {
        Raiser,
        Freezer,
    }
}