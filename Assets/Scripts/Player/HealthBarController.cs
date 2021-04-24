using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private static Image HealthBarImage;
    [SerializeField] Gradient gradient;
    
    [SerializeField] FloatReference CurrentPlayerHealth;
    [SerializeField] FloatReference MaxPlayerHealth;

    private void Start()
    {
        HealthBarImage = GetComponent<Image>();
    }

    public void UpdateHealthBar()
    {
        HealthBarImage.fillAmount = CurrentPlayerHealth.Value / MaxPlayerHealth.Value;
        SetHealthBarColor();
    }

    public void SetHealthBarColor()
    {        
        HealthBarImage.color = gradient.Evaluate(CurrentPlayerHealth.Value / MaxPlayerHealth.Value);
    }
}
