using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Spawner/SpawnerConfig")]
public class SpawnerConfig : ScriptableObject
{
    public ProjectileGroup projectileGroup;
    public FiringSettings firingSettings;
}
