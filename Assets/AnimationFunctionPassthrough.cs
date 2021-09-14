using BulletRPG.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFunctionPassthrough : MonoBehaviour
{
    IAttack attackScript;
    public void FireAttack()
    {
        Debug.Log("firingfrom AnimationFunctionPassthrough");
        if(attackScript == null)
        {
            attackScript = GetComponentInChildren<IAttack>();
        }

        if (attackScript != null)
        {
            attackScript.FireAttack();
        }
    }
}
