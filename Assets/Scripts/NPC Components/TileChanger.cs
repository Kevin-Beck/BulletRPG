using System.Collections.Generic;
using BulletRPG.NPCBehavior;
using UnityEngine;


public class TileChanger : NPCBehavior
{
    [SerializeField] public TileChangerConfig tileChangerConfig;

    private void Start()
    {
        GetComponent<BoxCollider>().transform.localScale = tileChangerConfig.size * Vector3.one;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }    

    private void OnTriggerEnter(Collider other)
    {
        var tile = other.GetComponent<TileController>();
        if (tileChangerConfig.type == TileChangerConfig.ChangerType.Raiser)
        {
            tile.Raise(tileChangerConfig.timeEffectStays);
        }else if(tileChangerConfig.type == TileChangerConfig.ChangerType.Freezer)
        {
            tile.Freeze(tileChangerConfig.timeEffectStays);
        }
        else if (tileChangerConfig.type == TileChangerConfig.ChangerType.Fire)
        {
            tile.Fire(tileChangerConfig.timeEffectStays);
        }
    }   
}
