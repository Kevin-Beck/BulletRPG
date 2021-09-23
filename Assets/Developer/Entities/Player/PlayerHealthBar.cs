using UnityEngine;
using UnityEngine.UI;

namespace BulletRPG.Characters.Player
{
    public class PlayerHealthBar : MonoBehaviour, IHealthBar
    {
        public GameObject playerHealthObject;
        private IHealth playerHealth;
        private static Image HealthBarImage;
        [SerializeField] Gradient gradient;

        private void Start()
        {
            playerHealth = playerHealthObject.GetComponent<IHealth>();
            playerHealth.AddHealthBar(this);
            if (playerHealth == null)
            {
                Debug.LogWarning("Healthbar not connected to health");
            }
            HealthBarImage = GetComponent<Image>();
        }

        public void UpdateHealthBar(float percentage)
        {
            HealthBarImage.fillAmount = percentage;
            SetHealthBarColor(percentage);
        }

        public void SetHealthBarColor(float percentage)
        {
            HealthBarImage.color = gradient.Evaluate(percentage);
        }
    }

}