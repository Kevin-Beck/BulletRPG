using System.Collections.Generic;
using UnityEngine;

public class TileSnake : MonoBehaviour
{
    [SerializeField] private SnakeConfig snakeData;
    int currentWaypoint = 0;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = snakeData.size;
        transform.position = snakeData.pattern.waypoints[0];
    }
    private void Update()
    {
        transform.LookAt(snakeData.pattern.waypoints[currentWaypoint]);
        if (Vector3.Distance(this.transform.position, snakeData.pattern.waypoints[currentWaypoint]) < .1f)
        {                
            currentWaypoint++;  
            if(currentWaypoint >= snakeData.pattern.waypoints.Count)
            {
                currentWaypoint = 0;
            }
            transform.LookAt(snakeData.pattern.waypoints[currentWaypoint]);
        }
        this.transform.position += (this.transform.forward * Time.deltaTime * snakeData.speed);

    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }



    private void OnTriggerEnter(Collider other)
    {
        var tile = other.GetComponent<TileController>();
        if (snakeData.type == SnakeConfig.SnakeType.Raiser)
        {
            tile.Raise();
        }else if(snakeData.type == SnakeConfig.SnakeType.Freezer)
        {
            tile.Freeze();
        }
    }
}
