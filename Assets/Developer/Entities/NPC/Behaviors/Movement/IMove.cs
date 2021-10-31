using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters
{
    public interface IMove
    {
        /// <summary>
        /// Used externally by elements that can slowdown and speed up the character
        /// </summary>
        /// <param name="speedMultiplier"></param>
        /// <param name="timeUntilReversion"></param>
        public void SetSpeedMultiplier(float speedMultiplier, float timeUntilReversion);
        /// <summary>
        /// Sets the speed and acceleration of the character. Used to initialize and revert the speed settings.
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="acceleration"></param>
        public void SetSpeedAndAcceleration(float speed, float acceleration);
    }
    public interface INPCMove : IMove
    {
        public void Move();
        public void Stop();
    }
}

