using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton that manages the input and keybindings for actions in the game
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] private Keybindings keybindings;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != null)
        {
            Debug.Log("WARNING: inputmanager already exists");
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public KeyCode GetKeyForAction(KeybindingActions keybindingAction)
    {
        //Find keycode
        foreach(Keybindings.KeybindingCheck keybindingCheck in keybindings.keybindingChecks)
        {
            if(keybindingCheck.keybindingAction == keybindingAction)
            {
                return keybindingCheck.keyCode;
            }
        }

        Debug.Log("NOT BOUND TO KEY");
        return KeyCode.None;
    }
    public bool GetKeyDown(KeybindingActions key)
    {
        //Find keycode
        foreach (Keybindings.KeybindingCheck keybindingCheck in keybindings.keybindingChecks)
        {
            if (keybindingCheck.keybindingAction == key)
            {
                return Input.GetKeyDown(keybindingCheck.keyCode);
            }
        }
        return false;
    }
    public bool GetKeyUp(KeybindingActions key)
    {
        //Find keycode
        foreach (Keybindings.KeybindingCheck keybindingCheck in keybindings.keybindingChecks)
        {
            if (keybindingCheck.keybindingAction == key)
            {
                return Input.GetKeyUp(keybindingCheck.keyCode);
            }
        }
        return false;
    }
    public bool GetKey(KeybindingActions key)
    {
        //Find keycode
        foreach (Keybindings.KeybindingCheck keybindingCheck in keybindings.keybindingChecks)
        {
            if (keybindingCheck.keybindingAction == key)
            {
                return Input.GetKey(keybindingCheck.keyCode);
            }
        }
        return false;
    }
}
