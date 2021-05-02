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
        if (tile != null)
        {
            tile.ProcessChange(tileChangerConfig);
        }
        else
        {
            Debug.Log("Changer collided with non-tile object");
        }
    }   
}
