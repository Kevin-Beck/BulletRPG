using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Characters.Player
{
    /// <summary>
    /// Set of attributes, dynamically set and adjusted
    /// </summary>
    public class CharacterStats : MonoBehaviour
    {
        public Attribute[] CharacterAttributes;
        private Dictionary<AttributeType, Attribute> attributeDictionary = new Dictionary<AttributeType, Attribute>();
        private void Start()
        {
            for (int i = 0; i < CharacterAttributes.Length; i++)
            {
                attributeDictionary.Add(CharacterAttributes[i].attributeType, CharacterAttributes[i]);
            }
        }
    }
}

