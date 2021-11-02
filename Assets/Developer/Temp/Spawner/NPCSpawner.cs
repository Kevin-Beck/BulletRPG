using BulletRPG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : Spawner<GameObject>
{
    public override void SpawnElement()
    {
        Vector3 position;
        if (Utilities.GetRandomPointOnNavMesh(transform.position, range, out position))
        {
            var enemy = elements[Random.Range(0, elements.Count)];
            var item = Instantiate(enemy, position + Vector3.up * 0.5f, Quaternion.identity);
        }
    }
}
