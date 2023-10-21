using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EquippedItem : MonoBehaviour
{
    [field: SerializeField]
    UIEquippedItemPage uIEquippedItemPage = new UIEquippedItemPage();

    void Start()
    {
        uIEquippedItemPage.initializeUIEquippedItem();
    }
    public void equipItem(Sprite item)
    {
    }
}