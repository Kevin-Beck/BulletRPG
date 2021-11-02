using BulletRPG;
using BulletRPG.Gear;
using BulletRPG.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : Spawner<ItemObject>
{
    public LootableItemTable lootables;

    public override void SpawnElement()
    {
        Vector3 position;
        if(Utilities.GetRandomPointOnNavMesh(transform.position, range, out position))
        {
            var itemObject = elements[Random.Range(0, elements.Count)];
            var item = Instantiate(lootables.lootableItemMap[itemObject.config.itemType], position+Vector3.up*0.5f, Quaternion.identity);
            var lootable = item.GetComponent<LootableItem>();
            lootable.itemObject = itemObject;
            lootable.amount = 1;
        }
    }
}