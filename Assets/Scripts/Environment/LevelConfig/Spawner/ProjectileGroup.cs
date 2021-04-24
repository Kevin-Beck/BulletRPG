using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="projectile group", menuName = "Spawner/ProjectileGroup")]
public class ProjectileGroup : ScriptableObject
{
    [SerializeField] public List<GameObject> projectiles;

}

