using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Range(30, 250)]
    [SerializeField] int targetFrameRate;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        Debug.Log(Application.companyName);
    }

    

}
