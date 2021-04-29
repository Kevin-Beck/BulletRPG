using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhaseConfig", menuName = "NPC/PhaseConfig")]
public class PhaseConfig : ScriptableObject
{
    [SerializeField] public bool delayStart;
    [SerializeField] public float startAfterDelaySeconds;
    [SerializeField] public bool destroyAfter;
    [SerializeField] public float destroyAfterSeconds;
    [SerializeField] public bool blink;
    [SerializeField] public float blinkInTime;
    [SerializeField] public float blinkOutTime;

}
