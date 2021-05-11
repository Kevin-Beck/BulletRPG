using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeybindingActions keyAction in (KeybindingActions[])Enum.GetValues(typeof(KeybindingActions)))
        {

            if (inputManager.GetKeyDown(keyAction))
            {
                Debug.Log($"{keyAction} key pressed");
            }
        }
    }

}
