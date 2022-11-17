using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [Header("UI stuff to change")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI numItemsText;

    [Header("Variables from item")]
    public InventoryItem thisItem;
    public ShopMenu thisManager;

    public void Setup(InventoryItem newItem, ShopMenu newManager)//Sets the inventory slot up
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
        }
    }

    public void ClickedOn()//passes the correct data back to the manager when the item is clicked on
    {
        if (thisItem)
        {
            thisManager.SetupItem(thisItem.itemDescription, thisItem.itemName, thisItem.cost,thisItem);
        }
    }
}
