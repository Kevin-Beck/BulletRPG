using System.Collections.Generic;
using BulletRPG.NPCBehavior;
using UnityEngine;


public class TileChanger : NPCBehavior
{
    [SerializeField] public TileChangerConfig tileChangerConfig;

    private void Start()
    {
        GetComponent<BoxCollider>().transform.localScale = tileChangerConfig.size * Vector3.one;
    }    

    private void OnTriggerEnter(Collider other)
    {
        var tile = other.GetComponent<TileController>();
        if (tileChangerConfig.type == TileChangerConfig.ChangerType.Raiser)
        {
            tile.Raise();
        }else if(tileChangerConfig.type == TileChangerConfig.ChangerType.Freezer)
        {
            tile.Freeze();
        }
    }   
}
