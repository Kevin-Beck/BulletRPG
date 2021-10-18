using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemObjectConfig", menuName = "Item/ItemObjectConfig")]
public class ItemObjectConfig : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public bool isStackable;
    [TextArea(2, 4)]
    public string itemDescription;
    public Sprite sprite;
    [TextArea(5, 20)]
    public string DevNotes;
}
