
using UnityEngine;
using UnityEngine.UI;

namespace BulletRPG.Characters.NPC
{
    [RequireComponent(typeof(NPCHealth))]
    public class NPCHealthBar : MonoBehaviour
    {
        NPCHealth myHealth;
        Slider slider;
        Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
            myHealth = GetComponent<NPCHealth>();
            myHealth.AddHealthBar(this);
            slider = GetComponentInChildren<Slider>();
            if (slider == null)
            {
                Debug.Log($"Slider not found for Healthbar on {gameObject}");
            }
            slider.gameObject.SetActive(false);
        }
        public void UpdateHealthBar(float percentage)
        {
            slider.value = percentage;
            slider.gameObject.SetActive(!Mathf.Approximately(percentage, 1));
        }
        private void Update()
        {
            //slider.gameObject.transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
            slider.gameObject.transform.LookAt(gameObject.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }

}