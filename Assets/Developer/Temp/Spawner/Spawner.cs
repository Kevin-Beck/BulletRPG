using BulletRPG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : MonoBehaviour
{
    public float range = 10.0f;
    public List<T> elements;

    public virtual void SpawnElement() {
        Debug.Log("Instantiate Not implemented");       
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
