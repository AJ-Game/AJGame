using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquippedItemPage : MonoBehaviour
{
    [field: SerializeField]
    private UIEquippedItemImage itemPrefab;
    UIEquippedItemImage createdItemUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void initializeUIEquippedItem()
    {
        UIEquippedItemImage uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
        createdItemUI = uiItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
