using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float BulletSpeed;

    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * BulletSpeed);
    }
}
