using BulletRPG;
using BulletRPG.Gear;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public float range = 10.0f;
    public List<ItemObject> items;    

    public void SpawnObject()
    {
        Vector3 position;
        if(Utilities.GetRandomPointOnNavMesh(transform.position, range, out position))
        {
            Instantiate(items[Random.Range(0, items.Count)].lootObject, position+Vector3.up*0.5f, Quaternion.identity);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
