using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public Item itemPickedUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        itemPickedUp = collision.GetComponent<Item>();
        if (itemPickedUp != null)
        {
            print("collected " + itemPickedUp.InventoryItem.Name);
            itemPickedUp.DestroyItem();
        }
    }
}
