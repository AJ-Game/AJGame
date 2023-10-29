using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public Item itemPickedUp;
    public EquippedItemSO equippedItem;
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("in pickedup");
        itemPickedUp = collision.GetComponent<Item>();
        if (itemPickedUp != null)
        {
            print("collected " + itemPickedUp.InventoryItem.Name);
            equippedItem.changeEquippedItem(itemPickedUp.InventoryItem);
            itemPickedUp.DestroyItem();
        }
    }
}
