using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public class BulletMovement : MonoBehaviour
    {
        [HideInInspector] public float BulletSpeed;

        void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * BulletSpeed);
        }
    }
}

