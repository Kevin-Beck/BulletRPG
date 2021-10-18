using BulletRPG.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : ScriptableObject
{
    [Header("Item Data")]
    [HideInInspector]
    public int Id = -1;
    public string itemName;
    public ItemType itemType;
    public bool isStackable;
    [TextArea(5, 20)]
    public string itemDescription;
    public Sprite sprite;

}

[System.Serializable]
public class Item
{    
    public int Id = -1;
    public ItemType itemType;
    public bool isStackable;
    public string name;
    public string description;

    public Item()
    {
        Id = -1;
    }
    public Item(ItemObject itemObject)
    {
        if(itemObject == null)
        {
            Debug.Log("ItemObject is null");
        }
        Id = itemObject.Id;
        itemType = itemObject.itemType;
        isStackable = itemObject.isStackable;
        name = itemObject.itemName;
        description = itemObject.itemDescription;
    }
    public virtual string StringifyName()
    {
        return name;
    }
    public virtual string StringifyDescription()
    {
        return description;
    }
}
public enum ItemType
{
    All,
    Gear,
    Consumable,
    Egg,
    Gold,
    Quest,
}
