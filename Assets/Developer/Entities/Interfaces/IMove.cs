using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters
{
    public interface IMove
    {
        public void SetSpeedMultiplier(float speedMultiplier, float timeUntilReversion);
        public void SetSpeedAndAcceleration(float speed, float acceleration);
    }
}

