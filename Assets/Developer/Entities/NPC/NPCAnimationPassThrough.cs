using BulletRPG.Characters.NPC;
using UnityEngine;

namespace BulletRPG.Gear
{
    /// <summary>
    /// This Class is required to be attached to the character root body which holds the Animator Component.
    /// The animation of the character will trigger a called event that will call "FireAttack" on this script.
    /// This script will then search the children transforms for a script of type IAttack. Which will then fire the triggered attack.
    /// </summary>
    public class NPCAnimationPassThrough : MonoBehaviour
    {
        NPCShoot shootScript;
        public void FireAttack()
        {
            Debug.Log("firingfrom AnimationFunctionPassthrough");
            if(shootScript == null)
            {
                shootScript = GetComponentInChildren<NPCShoot>();
            }

            if (shootScript != null)
            {
                shootScript.FireAttack();
            }
            else
            {
                Debug.Log("ShootScript was not found");
            }
        }
        public void Continue()
        {
            Debug.Log("Calling Continue");
            GetComponentInParent<NPC>().Continue();
        }
    }
}

