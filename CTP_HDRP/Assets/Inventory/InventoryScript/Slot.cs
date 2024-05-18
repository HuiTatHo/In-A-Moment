using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public Item slotItem;
    public Image slotImage;
    public TMP_Text slotNum;



    public void ItemOnClick()
    {
        InventoryManager.UpdateItemInfo(slotImage ,slotItem.itemName, slotItem.itemType, slotItem.itemInfo, slotItem.itemCanUse, slotItem.addHpAmount);
        InventoryManager.RefreshImage();
    }
}
