using BulletRPG.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters.NPC
{
    public class NPC : MonoBehaviour
    {
        public NPCHealth myHealth;
        public NPCMove myMove;
        public NPCShoot myShoot;

        bool isAlive = true;
        bool isAttacking = false;

        public Attribute[] attributes;
        CharacterStats baseStats;

        private void Awake()
        {
            myHealth = GetComponent<NPCHealth>();
            myMove = GetComponent<NPCMove>();
            myShoot = GetComponentInChildren<NPCShoot>();
            baseStats = GetComponentInChildren<CharacterStats>();
        }
        private void Start()
        {
            myMove.MoveToRandomPosition();
        }
        private void Update()
        {
            if (isAlive && !myMove.IsMoving && !isAttacking)
            {
                isAttacking = true;
                myShoot.StartAttackAnimation();
            }
        }    
        public void Continue()
        {
            Debug.Log("Continue in NPC");
            isAttacking = false;
            myMove.MoveToRandomPosition();
        }
    }
}

