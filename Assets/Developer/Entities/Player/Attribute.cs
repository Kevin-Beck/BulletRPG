using BulletRPG.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [System.Serializable]
    public class Attribute
    {
        [System.NonSerialized]
        public PlayerStats parent;
        public Attributes attribute;
        public ModifiableInt value;

        public void SetParent(PlayerStats _player)
        {
            parent = _player;
            value = new ModifiableInt(AttributeModified);
        }

        public void AttributeModified()
        {
            parent.AttributeModified(this);
        }
    }
}

