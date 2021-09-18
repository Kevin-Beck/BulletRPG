using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [Serializable]
    [CreateAssetMenu(fileName = "NewHelmet", menuName = "Item/Helmet")]
    public class Helmet : Gear
    {
        [Header("Helmet Elements")]
        [SerializeField] public float DamageReduction;
    }
}

