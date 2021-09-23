using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items.RangedWeapons
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

