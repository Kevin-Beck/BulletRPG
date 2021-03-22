using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "snake data", menuName = "Snake/SnakeData")]
public class SnakeData : ScriptableObject
{
    public float size = 1f;
    public float speed = 1f;
    public SnakeType type;

    public SnakePattern pattern;

    public enum SnakeType
    {
        Raiser,
        Freezer,
    }

}