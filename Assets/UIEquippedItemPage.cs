using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEquippedItemPage : MonoBehaviour
{
    // [SerializeField]
    // private UIEquippedItemImage itemPrefab;

    [SerializeField]
    private Image itemImage;
    public void initializeUIEquippedItem()
    {
        itemImage.sprite = Resources.Load<Sprite>("Weapons/Fist");
    }

    public void updateEquippedUI(Sprite s)
    {
        // itemPrefab.changeEquippedItemImage(s);
        itemImage.sprite = s;
    }

}
