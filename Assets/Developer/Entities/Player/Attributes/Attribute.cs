using BulletRPG.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters.Player
{
    [System.Serializable]
    public class Attribute
    {
        [System.NonSerialized]
        public Player parent;
        public AttributeType attributeType;
        public ModifiableInt value;

        public void SetParent(Player _player)
        {
            parent = _player;
            value = new ModifiableInt(AttributeModified);
        }

        public void AttributeModified()
        {
            parent.AttributeModified(this);
        }
    }
    public enum AttributeType
    {
        Endurance,
        Strength,
        Agility,
        Speed,
        Charm,
        Intelligence,
        Willpower,
        COUNT
    }
}

