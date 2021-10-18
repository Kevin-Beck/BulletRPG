using BulletRPG;
using BulletRPG.Gear;
using BulletRPG.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public float range = 10.0f;
    public List<ItemObject> items;
    public LootableItemTable lootables;

    public void SpawnObject()
    {
        Vector3 position;
        if(Utilities.GetRandomPointOnNavMesh(transform.position, range, out position))
        {
            var itemObject = items[Random.Range(0, items.Count)];
            var item = Instantiate(lootables.lootableItemMap[itemObject.config.itemType], position+Vector3.up*0.5f, Quaternion.identity);
            var lootable = item.GetComponent<LootableItem>();
            lootable.itemObject = itemObject;
            lootable.amount = 1;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}