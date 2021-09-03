using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDropper : MonoBehaviour
{
    [Header("Tile Group")]
    [SerializeField] public Tiles group;
   
    [Header("Config")]
    [SerializeField] Vector3 dropPositionOffset = new Vector3(1, .25f, 1);
    [SerializeField] float tileLifeTime = 10f;
    private List<Vector3> tilePositions;

    void Start()
    {
        tilePositions = new List<Vector3>();
        StartCoroutine("InstantiateTiles");
    }
    private void OnTriggerEnter(Collider collider)
    {
        tilePositions.Add(SnapPosition(collider.ClosestPoint(transform.position), dropPositionOffset, 2f));
    }

    private IEnumerator InstantiateTiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            foreach (Vector3 position in tilePositions)
            {
                var tile = Instantiate(group.items[Random.Range(0, group.items.Count)], position, Quaternion.identity);                
                Destroy(tile, tileLifeTime);
            }
            tilePositions.Clear();
        }
    }
    
    Vector3 SnapPosition(Vector3 input, Vector3 offset, float factor = 1f)
    {
        if (factor <= 0f)
            throw new UnityException("factor argument must be above 0");

        float x = Mathf.Round(input.x / factor) * factor;
        float y = Mathf.Round(input.y / factor) * factor;
        float z = Mathf.Round(input.z / factor) * factor;

        return new Vector3(x, y, z) + offset;
    }

}
