using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [Header("UI stuff to change")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI numItemsText;

    [Header("Variables from item")]
    public InventoryItem thisItem;
    public ItemMenu thisMenu;

    public void Setup(InventoryItem newItem, ItemMenu newMenu)//Sets the inventory slot up
    {
        thisItem = newItem;
        thisMenu = newMenu;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
        }
    }

    public void ClickedOn()//passes the correct data back to the manager when the item is clicked on
    {
        if (thisItem)
        {
            thisMenu.SetupItem(thisItem.itemDescription, thisItem.itemName, thisItem.numHeld.ToString(), thisItem.isConsumable, thisItem);
        }
    }
}
