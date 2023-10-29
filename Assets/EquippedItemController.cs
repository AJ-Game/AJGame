using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemController : MonoBehaviour
{
    [SerializeField]
    private UIEquippedItemPage equippedUI;

    [SerializeField]
    private EquippedItemSO equippedItemData;

    private void Start()
    {
        equippedUI.initializeUIEquippedItem();
        equippedItemData.Initialize();

    }
    public void Update()
    {
        //update ui
        equippedUI.updateEquippedUI(equippedItemData.getEquippedItem().UISprite);
    }
}
