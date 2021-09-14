using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletRPG.Items
{
    public interface IAttack
    {
        public void StartAttack(InputAction.CallbackContext context);
        public void FireAttack();
    }
}

