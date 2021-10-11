using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitch : MonoBehaviour
{
    public GameObject panel;
    public void Toggle()
    {
        panel.SetActive(!panel.activeInHierarchy);
    }
}
