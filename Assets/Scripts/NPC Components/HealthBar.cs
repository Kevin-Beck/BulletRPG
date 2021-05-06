using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HealthBar : MonoBehaviour
{
    Health myHealth;
    Slider slider;
    Camera mainCamera;
    

    private void Start()
    {
        mainCamera = Camera.main;
        myHealth = GetComponent<Health>();
        myHealth.AddHealthBar(this);
        slider = GetComponentInChildren<Slider>();
        if(slider == null)
        {
            Debug.Log($"Slider not found for Healthbar on {gameObject}");           
        }
        slider.gameObject.SetActive(false);
    }
    public void UpdateBar(float percentage)
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
