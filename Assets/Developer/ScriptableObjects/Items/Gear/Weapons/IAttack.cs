using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletRPG.Items
{
    public interface IAttack
    {
        public void StartAttackFromInput(InputAction.CallbackContext context);
        public void FireAttack();
    }
}

