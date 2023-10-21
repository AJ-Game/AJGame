using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIEquippedItemImage : MonoBehaviour
{
    public string equippedItem;
    public Image image;

    // Start is called before the first frame update
    // void Start()
    // {    
    //     equippedItem = "Weapons/Bat_1";
    //     image = gameObject.GetComponent<Image>();
    //     // image.sprite = Resources.Load<Sprite>(equippedItem);
    //     changeEquippedItemImage(Resources.Load<Sprite>(equippedItem));
    // }

    public void changeEquippedItemImage(Sprite toEquip){
        // equippedItem = toEquip;
        print(toEquip);
        print(image);
        image.sprite = toEquip;
        // image.sprite = Resources.Load<Sprite>(equippedItem);
    }

    // Update is called once per frame
//     void Update()
//     {
//     }
}
