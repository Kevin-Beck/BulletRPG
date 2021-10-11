using BulletRPG.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear
{
    /// <summary>
    /// This Class is required to be attached to the character root body which holds the Animator Component.
    /// The animation of the character will trigger a called event that will call "FireAttack" on this script.
    /// This script will then search the children transforms for a script of type IAttack. Which will then fire the triggered attack.
    /// </summary>
    public class AnimationFunctionPassthrough : MonoBehaviour
    {
        IAttack attackScript;
        public void FireAttack()
        {
            Debug.Log("firingfrom AnimationFunctionPassthrough");
            attackScript = GetComponentInChildren<IAttack>();


            if (attackScript != null)
            {
                attackScript.FireAttack();
            }
        }
    }
}

