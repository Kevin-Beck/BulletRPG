using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTiles : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] int rows = 1;
    [SerializeField] int columns = 1;

    float xMax;
    float xMin;
    float zMax;
    float zMin;
    float tileSizeX;
    float tileSizeZ;


    // Start is called before the first frame update
    void Start()
    {
        Transform planeTransform = GetComponent<Transform>();
        xMax = planeTransform.localScale.x*5;
        zMax = planeTransform.localScale.z*5;
        xMin = -xMax;
        zMin = -zMax;

        Transform tileTransform = tile.GetComponent<Transform>();
        tileSizeX = (xMax - xMin) / columns;
        tileSizeZ = (zMax - zMin) / rows;

        tileTransform.localScale = new Vector3(tileSizeX, tileTransform.localScale.y, tileSizeZ);

        Vector3 position = new Vector3(xMin + (tileSizeX / 2), 0, zMin + (tileSizeZ / 2));
        for(int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                var currentTile = Instantiate(tile, position + new Vector3(i * tileSizeX, planeTransform.position.y-1.8f, j * tileSizeZ), Quaternion.identity);
                currentTile.GetComponent<Transform>().parent = planeTransform;
            }
        }        
    }    
}
