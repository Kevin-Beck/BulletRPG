using BulletRPG.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtonPassthrough : MonoBehaviour
{
    // Start is called before the first frame update
    private InventoryUI inventoryUI;
    void Start()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
    }

    public void EquipPassThrough()
    {
        inventoryUI.Equip(gameObject);
    }
}
