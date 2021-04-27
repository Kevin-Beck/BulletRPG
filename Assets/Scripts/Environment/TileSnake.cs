using System.Collections.Generic;
using UnityEngine;


public class TileSnake : MonoBehaviour
{
    [SerializeField] public SnakeConfig snakeConfig;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = snakeConfig.size;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        var tile = other.GetComponent<TileController>();
        if (snakeConfig.type == SnakeConfig.SnakeType.Raiser)
        {
            tile.Raise();
        }else if(snakeConfig.type == SnakeConfig.SnakeType.Freezer)
        {
            tile.Freeze();
        }
    }   
}
