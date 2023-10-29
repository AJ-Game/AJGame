using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class EquippedItemSO : ScriptableObject
{
    [SerializeField]
    private ItemSO equippedItem;

    public event Action<ItemSO> OnEquippedItemUpdated;

    public void Initialize()
    {
        equippedItem = getDefaultItem();
    }
    public void changeEquippedItem(ItemSO item)
    {
        equippedItem = item;
    }
    public ItemSO getEquippedItem()
    {
        return equippedItem;
    }
    public ItemSO getDefaultItem()
    {
        Sprite fist = Resources.Load<Sprite>("Weapons/Fist");
        ItemSO defaultItem = ScriptableObject.CreateInstance<ItemSO>();
        defaultItem.IsStackable = false;
        defaultItem.MaxStackSize = 1;
        defaultItem.Name = "Fist";
        defaultItem.ItemImage = fist;
        defaultItem.UISprite = fist;
        return defaultItem;
    }
}