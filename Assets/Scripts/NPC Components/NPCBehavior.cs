using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.NPCBehavior
{
    public class NPCBehavior : MonoBehaviour
    {
        [SerializeField] public PhaseConfig phaseConfig;

        private void Awake()
        {
            if(phaseConfig == null)
            {
                Debug.LogError($"PhaseConfig for {this} on {gameObject} is not set.");
            }
            Invoke("Initialize", phaseConfig.startAfterDelaySeconds);
            enabled = false;
        }
        private void Initialize()
        {
            enabled = true;
            if (phaseConfig.destroyAfter)
            {
                Destroy(this, phaseConfig.destroyAfterSeconds);
            }
            if (phaseConfig.blink)
            {
                Invoke("PhaseOut", phaseConfig.blinkInTime);                
            }
        }
        private void PhaseOut()
        {
            enabled = false;
            Invoke("PhaseIn", phaseConfig.blinkOutTime);
        }
        private void PhaseIn()
        {
            enabled = true;
            Invoke("PhaseOut", phaseConfig.blinkInTime);
        }
        
    }
}

