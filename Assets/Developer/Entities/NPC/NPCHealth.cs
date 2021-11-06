using BulletRPG.Gear.Weapons;
using BulletRPG.Temp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters.NPC
{
    [RequireComponent(typeof(Collider))]
    public class NPCHealth : MonoBehaviour, IHealth
    {
        private NPC npc;
        private Animator animator;
        [SerializeField] public float startingHealth;

        CooldownTimer destroyTimer;
        CooldownTimer lowerTimer;

        private IHealthBar healthBar;
        public float currentHealth;
        private bool _isDead = false;
        public bool IsDead { get => _isDead; }


        public List<DamageMitigator> mitigators = new List<DamageMitigator>();

        private void Awake()
        {
            npc = GetComponent<NPC>();
            animator = GetComponentInChildren<Animator>();
        }
        private void Update()
        {
            if(destroyTimer != null)
            {
                destroyTimer.Update(Time.deltaTime);
            }
            if(lowerTimer != null)
            {
                lowerTimer.Update(Time.deltaTime);
            }
        }
        private void Start()
        {
            if (startingHealth == 0)
            {
                Debug.LogWarning($"Starting Health of {gameObject} is zero.");
            }
            currentHealth = startingHealth;
            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(currentHealth / startingHealth);
            }
        }

        private void ChangeHealth(float amount)
        {
            if (_isDead)
            {
                return;
            }
            currentHealth += amount;
            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(currentHealth / startingHealth);
            }
            if (currentHealth <= 0)
            {
                animator.SetBool("die", true);
                _isDead = true;
                DeathSequence();
                npc.Die();
            }

        }
        private void DeathSequence()
        {
            lowerTimer = new CooldownTimer(1.8f);
            lowerTimer.TimerCompleteEvent += () => StartCoroutine(LowerUnderground());
            lowerTimer.Start();

            destroyTimer = new CooldownTimer(3.6f);
            destroyTimer.TimerCompleteEvent += () => Destroy(gameObject);
            destroyTimer.Start();            
        }
        public IEnumerator LowerUnderground()
        {
            float timeElapsed = 0;
            Vector3 start = transform.position;
            Vector3 end = transform.position - transform.up * 3;
            Debug.Log("Called the function LowerUnderground");
            while (true)
            {
                transform.position = Vector3.Lerp(start, end, timeElapsed / 1.8f);
                timeElapsed += Time.deltaTime;

                yield return null;
            }
        }
        public void HealFlatAmount(float amount)
        {
            if(amount < 0)
            {
                Debug.Log("Cannot heal for less than 0");
                return;
            }
            ChangeHealth(amount);
        }

        public void HealPercentage(float percentage)
        {
            if(percentage < 0)
            {
                Debug.Log("Cannot heal with percent less than 0");
                return;
            }
            if(percentage > 100)
            {
                percentage = 100;
            }
            ChangeHealth(startingHealth * (percentage/100));
        }

        private void TakeDamageAmount(float amount)
        {
            if(amount < 0)
            {
                Debug.Log("Cannot damage for less than 0");
                return;
            }
            ChangeHealth(-1 * amount);
        }

        public void TakeDamagePercentage(float percentage)
        {
            if (percentage < 0)
            {
                Debug.Log("Cannot damage with percent less than 0");
                return;
            }
            if (percentage > 1)
            {
                percentage = 1;
            }
            ChangeHealth(startingHealth * (percentage/100) * -1);
        }

        public void AddHealthBar(IHealthBar bar)
        {
            healthBar = bar;
        }

        public void SetCurrentAndMaxHealth(float currentHealth, float maxHealth)
        {
            throw new NotImplementedException();
        }

        public Tuple<float, float> GetCurrentAndMaxHealth()
        {
            throw new NotImplementedException();
        }

        public void ProcessDamage(Damage damage)
        {
            Damage damageToTake = damage;
            for (int i = 0; i < mitigators.Count; i++)
            {
                if(damage.damageType == mitigators[i].damageType)
                {
                    damageToTake = mitigators[i].MitigateDamage(damage);
                }
            }
            TakeDamageAmount(damageToTake.amount);
            DamagePopup.Create(transform, transform.position + transform.up * 4f, damageToTake);
            Debug.Log("Processing npc damage");
            // TODO apply other damage affects here
        }

        public void AddDamageMitigators(List<DamageMitigator> mitigators)
        {
            throw new NotImplementedException();
        }
    }
}



