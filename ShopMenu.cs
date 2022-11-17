using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI shopText;
    [SerializeField] private TextMeshProUGUI creditsText;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject confirmationWindow;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject blankShopSlot;
    [SerializeField] private GameObject selector1;
    [SerializeField] private GameObject selector2;
    public GameObject fullShop;
    [SerializeField] private Stats player;
    [SerializeField] private PlayerInventory shopInventory;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private string welcome;
    [SerializeField] private string shop;
    [SerializeField] private ShopOpen open;
    [SerializeField] private Paused paused;
    private InventoryItem currentItem;
    private int amount;
    public bool started = false;
    //needs some way of specifiying inventory

    void Start()
    {
        MakeShopSlots();
        SetTextAndButton(welcome , "", 0,"");
        shopText.text = shop;
    }

    private void MakeShopSlots()
    {
        if (shopInventory)
        {
            for (int i = 0; i < shopInventory.myInven.Count; i++)
            {
                    GameObject temp = Instantiate(blankShopSlot, shopPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(shopPanel.transform);
                    ShopSlot newSlot = temp.GetComponent<ShopSlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(shopInventory.myInven[i], this);
                    }
            }
        }
    }

    private void SetTextAndButton(string desc, string name, int cost, string amount)
    {
        descText.text = desc;
        nameText.text = name;
        amountText.text = amount;
        buyButton.gameObject.SetActive(false);
        creditsText.text = "$" + player.credits;
        if (name == "")
        {
            costText.text = "";
            selector1.gameObject.SetActive(false);
            selector2.gameObject.SetActive(false);
        }
        else
        {
            costText.text = "Cost: " + cost.ToString();
            if (player.credits > cost)
            {
                buyButton.gameObject.SetActive(true);
            }
            selector1.gameObject.SetActive(true);
            selector2.gameObject.SetActive(true);
        }

    }

    public void SetupItem(string newDesc, string newName, int newCost,InventoryItem newItem)
    {
        currentItem = newItem;
        descText.text = newDesc;
        nameText.text = newName;
        costText.text = "Cost: " + newCost.ToString();
        amountText.text = "Amount : 1";
        amount = 1;
        selector1.gameObject.SetActive(true);
        selector2.gameObject.SetActive(true);
        if (player.credits>=newCost)
        {
            buyButton.gameObject.SetActive(true);
        }
        else
        {
            buyButton.gameObject.SetActive(false);
        }
    }

    public void IncreaseAmount()
    {
        amount += 1;
        amountText.text = "Amount: " + amount;
        costText.text = "Cost: " + (currentItem.cost * amount).ToString();
        if(player.credits<currentItem.cost*amount)
        {
            buyButton.gameObject.SetActive(false);
        }
    }

    public void DecreaseAmount()
    {
        if(amount>1)
        {
            amount -= 1;
            amountText.text = "Amount: " + amount;
            costText.text = "Cost: " + (currentItem.cost * amount).ToString();
            if (player.credits>=currentItem.cost*amount)
            {
                buyButton.gameObject.SetActive(true);
            }
        }
    }

    public void BuyButtonPress()
    {
        confirmationWindow.gameObject.SetActive(true);
        fullShop.gameObject.SetActive(false);
        //opens up confirmation window
    }

    public void CancelBuy()
    {
        confirmationWindow.gameObject.SetActive(false);
        fullShop.gameObject.SetActive(true);
        //goes back to shop
    }

    public void ConfirmBuy()
    {
        //subtracts money and adds the item(s) to the player's inventory
        confirmationWindow.gameObject.SetActive(false);
        fullShop.gameObject.SetActive(true);
        player.credits -= currentItem.cost * amount;
        creditsText.text = "$" + player.credits;
        SetTextAndButton("Thank you for your purchase", "", 0, "");
        if (currentItem.isEquipable)
        {
            playerInventory.myInven.Add(currentItem);
            //add directly to end of inventory
        }
        else
        {
            for (int i = 0; i < playerInventory.myInven.Count; i++)
            {
                if (playerInventory.myInven[i].itemName == currentItem.itemName && !currentItem.isEquipable)
                {
                    playerInventory.myInven[i].numHeld += amount;
                }
            }
        }
    }

    public void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        fullShop.gameObject.SetActive(false);
        open.paused = false;
        paused.isPaused = false;
        open.interactText.gameObject.SetActive(true);
    }

    void Update()
    {
        if(started)
        {
            SetTextAndButton(welcome, "", 0, "");
            started = false;
        }
    }
}
