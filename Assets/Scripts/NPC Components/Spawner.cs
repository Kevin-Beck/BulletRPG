using BulletRPG.NPCBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Items To Be Dropped")]
    [SerializeField] public Collectibles group;

    [Header("Drop Statistics")]
    [Range(0f, 1f)]
    [SerializeField] public float dropChance;
    [Range(0f, 10f)]
    [SerializeField] public float dropRate;
    [Range(0f, 100f)]
    [SerializeField] public int maxDropped;

    [Header("Config")]
    [SerializeField] Vector3 dropPositionOffset = new Vector3(1, .25f, 1);
    WaitForSeconds delay;
    


    void Start()
    {
        delay = new WaitForSeconds(dropRate);
        StartCoroutine("SpawnDrop");
    }
    private IEnumerator SpawnDrop()
    {
        while (true)
        {
            yield return delay;
            if (Random.Range(0f, 1f) < dropRate)
            {
                var dropLocation = SnapPosition(transform.position, dropPositionOffset, 2f);
                var drop = Instantiate(group.items[Random.Range(0, group.items.Count)], dropLocation, Quaternion.identity);
            }
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