using BulletRPG.Characters.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationHelper : MonoBehaviour
{
    [SerializeField] NPC myNPC;
    private void Awake()
    {
        myNPC = GetComponentInParent<NPC>();
    }
    public void Shoot()
    {
        var shoot = GetComponentInParent<BaseNPCShoot>();
        shoot.FireAttack();
    }
    public void AttackFinished()
    {
        myNPC.AttackFinished();
    }

}
