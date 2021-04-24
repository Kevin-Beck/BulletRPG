using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NPCHealth))]
public class NPCHealthBar : MonoBehaviour
{
    NPCHealth myHealth;
    public GameObject sliderObject;
    public Slider slider;
    Camera mainCamera;
    

    private void Start()
    {
        mainCamera = Camera.main;
        myHealth = GetComponent<NPCHealth>();
        myHealth.AddHealthBar(this);
        sliderObject.SetActive(false);
    }
    public void UpdateBar(float percentage)
    {
        slider.value = percentage;
        sliderObject.SetActive(!Mathf.Approximately(percentage, 1));
    }
    private void Update()
    {
        slider.transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back, mainCamera.transform.rotation * Vector3.up);
    }
}
