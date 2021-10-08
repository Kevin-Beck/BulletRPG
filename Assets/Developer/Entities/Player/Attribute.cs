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
        Agility,
        Intellect,
        Stamina,
        Strength,
    }
}

