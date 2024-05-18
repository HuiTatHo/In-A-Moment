using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory myBackpack;
    public GameObject slotGrid;
    public Slot slotPrefab;

    public Image itemPicture;
    public TMP_Text itemTitle;
    public TMP_Text itemInfromation;

    public TMP_Text itemType;
    public TMP_Text hpAmount;

    public GameObject useBtn;
    public GameObject objHpAmount;
    private bool itemCanUse;
    public int addHpAmount;



    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    void Update()
    {
        useBtn.SetActive(instance.itemCanUse);
        objHpAmount.SetActive(instance.itemCanUse);
    }
    private void OnEnable()
    {
        RefreshItem();
        instance.itemPicture.sprite = null;
        instance.itemTitle.text = "";
        instance.itemType.text = "";    
        instance.itemInfromation.text = "";
        instance.hpAmount.text = "";
        instance.itemCanUse = false;
        instance.addHpAmount = 0;
    }


    public static int GetAddHpAmount()
    {
        return instance.addHpAmount;
    }
    public static void UpdateItemInfo(Image _itemImage, String _itemName, String _itemType, String itemDescription, bool _itemCanUse, int _addHpAmount)
    {
        instance.itemPicture.sprite = _itemImage.sprite;
        instance.itemTitle.text = _itemName;
        instance.itemType.text = _itemType;
        instance.itemInfromation.text = itemDescription;
        instance.itemCanUse = _itemCanUse;
        instance.addHpAmount = _addHpAmount;
        instance.hpAmount.text = "HP: "+_addHpAmount.ToString();

    }

    public static void RefreshImage()
    {
        instance.itemPicture.color = Color.white;
    }

    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    }


    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance.myBackpack.itemList.Count; i++)
        {
            CreateNewItem(instance.myBackpack.itemList[i]);
        }
    }
}
