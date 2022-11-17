using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    public PlayerInventory playerInven;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject fullInven;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject equippedItems;
    [SerializeField] private GameObject confirmWindow;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject unequipButton;
    [SerializeField] private GameObject useButton;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI numText;
    [SerializeField] private TextMeshProUGUI reqNotMetText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private TextMeshProUGUI partyText;
    [SerializeField] private TextMeshProUGUI powText;
    [SerializeField] private TextMeshProUGUI tenText;
    [SerializeField] private TextMeshProUGUI intText;
    [SerializeField] private TextMeshProUGUI dexText;
    [SerializeField] private TextMeshProUGUI affText;
    [SerializeField] private TextMeshProUGUI dvText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI dodgeText;
    [SerializeField] private TextMeshProUGUI resistText;
    [SerializeField] private TextMeshProUGUI equipText;
    [SerializeField] private TextMeshProUGUI damText;
    [SerializeField] private TextMeshProUGUI lvText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI nextText;
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private CurrentParty party;
    [SerializeField] private TMP_Dropdown targetSelect;
    [SerializeField] private GameObject lButton;
    [SerializeField] private GameObject rButton;
    [SerializeField] private GameObject PowUp;
    [SerializeField] private GameObject TenUp;
    [SerializeField] private GameObject IntUp;
    [SerializeField] private GameObject DexUp;
    [SerializeField] private GameObject AffUp;
    public InventoryItem currentItem;
    public bool started = false;
    private int currentStats = 0;

    private void SetTextAndButton(string desc,string name, string num, bool buttonActiveUse, 
        bool buttonActiveEquip, bool buttonActiveUnequip)//code to set the texts and buttons
    {
        descText.text = desc;
        nameText.text = name;
        numText.text = num;
        reqNotMetText.gameObject.SetActive(false);
        targetSelect.gameObject.SetActive(false);
        confirmWindow.gameObject.SetActive(false);
        if(buttonActiveUse)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
        if(buttonActiveEquip && !buttonActiveUnequip)
        {
            equipButton.SetActive(true);
        }
        else
        {
            equipButton.SetActive(false);
        }
        if(buttonActiveUnequip)
        {
            unequipButton.SetActive(true);
        }
        else
        {
            unequipButton.SetActive(false);
        }    
    }

    private void SetStats()//Pulls the stats from the party list and displays them
    {
        partyText.text = party.myParty[currentStats].memberName;
        healthText.text ="Hp " + party.myParty[currentStats].hpCurrent.ToString() + " / " + party.myParty[currentStats].hpMax.ToString();
        energyText.text = "Energy " + party.myParty[currentStats].energyCurrent.ToString() + " / " + party.myParty[currentStats].energyMax.ToString();
        powText.text = "Power " + party.myParty[currentStats].power.ToString();
        tenText.text = "Tenacity " + party.myParty[currentStats].tenacity.ToString();
        intText.text = "Intellect " + party.myParty[currentStats].intellect.ToString();
        dexText.text = "Dexterity " + party.myParty[currentStats].dexterity.ToString();
        affText.text = "Affinity " + party.myParty[currentStats].affinity.ToString();
        dvText.text = "Defence Value " + party.myParty[currentStats].defenceValue.ToString();
        speedText.text = "Speed " + party.myParty[currentStats].speed.ToString();
        dodgeText.text = "Dodge Chance " + party.myParty[currentStats].dodge.ToString() + "%";
        resistText.text = "Negative Effect Resist    " + party.myParty[currentStats].effectRes.ToString() + "%";
        equipText.text = "Equipment Weight " + party.myParty[currentStats].currentWeight.ToString() + " / " + party.myParty[currentStats].maxWeight.ToString();
        damText.text = "Damage " + party.myParty[currentStats].damMin.ToString() + " - " + party.myParty[currentStats].damMax.ToString();
        lvText.text = "LV " + party.myParty[currentStats].level.ToString();
        statText.text = "Stat Points Left: " + party.myParty[currentStats].statPoints.ToString();
        expText.text = "Exp: " + party.myParty[currentStats].exp.ToString();
        nextText.text = "Next level: " + party.myParty[currentStats].expNext.ToString();
        creditText.text = "Credits: " + party.myParty[currentStats].credits.ToString();
        //probably add the current damage increase for each weapon type
        if(party.myParty[currentStats].statPoints>0)
        {
            PowUp.gameObject.SetActive(true);
            TenUp.gameObject.SetActive(true);
            IntUp.gameObject.SetActive(true);
            DexUp.gameObject.SetActive(true);
            AffUp.gameObject.SetActive(true);
        }
        else
        {
            PowUp.gameObject.SetActive(false);
            TenUp.gameObject.SetActive(false);
            IntUp.gameObject.SetActive(false);
            DexUp.gameObject.SetActive(false);
            AffUp.gameObject.SetActive(false);
        }
    }

    public void IncreaseStats(int i)
    {
        if(i==0)
        {
            party.myParty[currentStats].power += 1;
            party.myParty[currentStats].maxWeight += 2;
            party.myParty[currentStats].damMelee += 0.02f;
            party.myParty[currentStats].statPoints -= 1;
        }
        else if(i==1)
        {
            party.myParty[currentStats].tenacity += 1;
            party.myParty[currentStats].hpMax += 5f;
            party.myParty[currentStats].energyMax += 2f;
            party.myParty[currentStats].statPoints -= 1;
        }
        else if(i ==2)//need some way of updating weapon damage when level up
        {
            party.myParty[currentStats].intellect += 1;
            party.myParty[currentStats].effectRes += 0.4f;
            party.myParty[currentStats].damTech += 0.02f;
            party.myParty[currentStats].statPoints -= 1;
        }
        else if(i == 3)
        {
            party.myParty[currentStats].dexterity += 1;
            party.myParty[currentStats].dodge += 0.3f;
            party.myParty[currentStats].damGun += 0.02f;
            party.myParty[currentStats].statPoints -= 1;
        }
        else if(i==4)
        {
            party.myParty[currentStats].affinity += 1;
            party.myParty[currentStats].speed += 1;
            party.myParty[currentStats].damMech += 0.02f;
            party.myParty[currentStats].statPoints -= 1;
        }
        SetStats();
    }

    private void MakeInventorySlots()//pulls the data from the inventory scriptable object and displays the items
    {
        if(playerInven)
        {
            for (int i = 0; i < playerInven.myInven.Count; i++)
            {
                if (playerInven.myInven[i].numHeld > 0 && !playerInven.myInven[i].isEquiped)
                {
                    GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInven.myInven[i], this);
                    }
                }
            }
        }
    }

    void Start()//sets up the inventory and stats
    {
        MakeInventorySlots();
        SetTextAndButton("", "", "", false, false, false);
        SetStats();
        targetSelect.ClearOptions();
        List<string> options = new List<string>();
        for(int i = 0; i<party.myParty.Count;i++)
        {
            options.Add(party.myParty[i].memberName);
        }
        targetSelect.AddOptions(options);
        targetSelect.value = 0;
        targetSelect.RefreshShownValue();
        lButton.gameObject.SetActive(false);
        ShowEquipped();
    }

    void Update()
    {
        if(started)//resets the selected item when the inventory is closed
        {
            SetTextAndButton("", "", "", false,false,false);
            ClearInvenSlots();
            MakeInventorySlots();
            ClearEquipped();
            ShowEquipped();
            SetStats();
            started = false;
        }
    }
    public void SetupItem(string newDesc, string newName, string newNum, bool isEquipable, bool isConsumable, bool isFightExclusive, bool isEquipped,
        InventoryItem newItem)//the code to change the display when the item is selected
    {
        currentItem = newItem;
        descText.text = newDesc;
        nameText.text = newName;
        numText.text = "x " + newNum;
        reqNotMetText.text = "";
        if(isEquipable && !isEquipped)
        {
            equipButton.SetActive(true);
        }
        else if(isEquipable && isEquipped)
        {
            unequipButton.SetActive(true);
        }
        else
        {
            unequipButton.SetActive(false);
            equipButton.SetActive(false);
        }
        if(isConsumable&&!isFightExclusive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    public void EquipButtonPress()//if the equip button is pressed
    {
        if(currentItem)
        {
            targetSelect.gameObject.SetActive(true);
            fullInven.gameObject.SetActive(false);
            confirmWindow.gameObject.SetActive(true);
        }
    }

    public void UseButtonPress()//if the use button is pressed
    {
        if (currentItem)
        {
            targetSelect.gameObject.SetActive(true);
            fullInven.gameObject.SetActive(false);
            confirmWindow.gameObject.SetActive(true);

        }
    }

    public void UnequipButtonPress()//unequips the selected item
    {
        party.myParty[currentStats].currentWeight -= currentItem.itemWeight;
        party.myParty[currentStats].damMin -= currentItem.dmgMin;
        party.myParty[currentStats].damMax -= currentItem.dmgMax;
        party.myParty[currentStats].defenceValue -= currentItem.defenceValue;
        party.myParty[currentStats].speed -= currentItem.speedUp;
        party.myParty[currentStats].dodge -= currentItem.dodgeUp;
        party.myParty[currentStats].effectRes -= currentItem.resUp;
        party.myParty[currentStats].energyMax -= currentItem.energyUp;
        party.myParty[currentStats].hpMax -= currentItem.healthUp;
        if(party.myParty[currentStats].setBonus)
        {
            party.myParty[currentStats].defenceValue -= currentItem.defenceValueSet;
            party.myParty[currentStats].speed -= currentItem.speedUpSet;
            party.myParty[currentStats].dodge -= currentItem.dodgeUpSet;
            party.myParty[currentStats].effectRes -= currentItem.resUpSet;
            party.myParty[currentStats].energyMax -= currentItem.energyUpSet;
            party.myParty[currentStats].hpMax -= currentItem.healthUpSet;
            party.myParty[currentStats].setBonus = false;
        }
        if(party.myParty[currentStats].hpCurrent> party.myParty[currentStats].hpMax)
        {
            party.myParty[currentStats].hpCurrent = Mathf.RoundToInt(party.myParty[currentStats].hpMax);
        }
        if(party.myParty[currentStats].energyCurrent> party.myParty[currentStats].energyMax)
        {
            party.myParty[currentStats].energyCurrent = Mathf.RoundToInt(party.myParty[currentStats].energyMax);
        }
        currentItem.isEquiped = false;
        ClearInvenSlots();
        ClearEquipped();
        ShowEquipped();
        MakeInventorySlots();
        SetStats();
        SetTextAndButton(currentItem.itemDescription, currentItem.itemName, "x " + currentItem.numHeld.ToString(),
                currentItem.isConsumable, currentItem.isEquipable, currentItem.isEquiped);
    }

    private void ClearInvenSlots()//clears what is currently displayed in the inventory
    {
        for(int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void TargetSelect()
    {
        Stats target;
        target = party.myParty[targetSelect.value];
        if(currentItem.isEquipable&&currentItem.lvlReq<=target.level&&currentItem.powReq <= target.power&&
            currentItem.intReq <= target.intellect&&currentItem.dexReq <= target.dexterity&&currentItem.affReq <= target.affinity)
        {
            reqNotMetText.text = "Requirements not met";
            reqNotMetText.gameObject.SetActive(true);
            targetSelect.gameObject.SetActive(false);
            confirmButton.gameObject.SetActive(false);

        }
        else
        {
            currentItem.Use(target);
            ClearInvenSlots();//Update number left if used consumable
            MakeInventorySlots();
            ClearEquipped();
            ShowEquipped();
            if (currentItem.numHeld > 0)
            {
                SetTextAndButton(currentItem.itemDescription, currentItem.itemName, "x " + currentItem.numHeld.ToString(),
                  currentItem.isConsumable, currentItem.isEquipable, currentItem.isEquiped);
            }
            else
            {
                SetTextAndButton("", "", "", false, false, false);
            }
            SetStats();
            fullInven.gameObject.SetActive(true);
            confirmWindow.gameObject.SetActive(false);
            targetSelect.gameObject.SetActive(false);
        }
    }
    
    public void Cancel()
    {
        fullInven.gameObject.SetActive(true);
        confirmWindow.gameObject.SetActive(false);
        targetSelect.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(true);
    }    

    public void SwitchCharacterPlus()//code to make stats switch
    {
        currentStats++;
        if(currentStats>=party.myParty.Count-1)
        {
            currentStats = party.myParty.Count-1;
            rButton.gameObject.SetActive(false);
        }
        SetStats();
        ClearEquipped();
        ShowEquipped();
        lButton.gameObject.SetActive(true);
    }

    public void SwitchCharacterMinus()//code to make stats switch
    {
        currentStats--;
        if(currentStats<=0)
        {
            currentStats = 0;
            lButton.gameObject.SetActive(false);
        }
        SetStats();
        ClearEquipped();
        ShowEquipped();
        rButton.gameObject.SetActive(true);
    }

    private void ShowEquipped()
    {
        if (playerInven)
        {
            for (int i = 0; i < playerInven.myInven.Count; i++)
            {
                bool found = false;
                if (playerInven.myInven[i].numHeld > 0 && playerInven.myInven[i].isEquiped && playerInven.myInven[i].target == party.myParty[currentStats] && 
                    playerInven.myInven[i].itemType == 0)
                {
                    GameObject temp = Instantiate(blankInventorySlot, equippedItems.transform.position, Quaternion.identity);
                    temp.transform.SetParent(equippedItems.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInven.myInven[i], this);
                    }
                    found = true;
                }
                if(!found && i == playerInven.myInven.Count-1)
                {
                    GameObject temp = Instantiate(blankInventorySlot, equippedItems.transform.position, Quaternion.identity);
                    temp.transform.SetParent(equippedItems.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInven.myInven[0], this);
                    }
                }
            }
            for (int i = 0; i < playerInven.myInven.Count; i++)
            {
                bool found = false;
                if (playerInven.myInven[i].numHeld > 0 && playerInven.myInven[i].isEquiped && playerInven.myInven[i].target == party.myParty[currentStats] &&
                    playerInven.myInven[i].itemType == 1)
                {
                    GameObject temp = Instantiate(blankInventorySlot, equippedItems.transform.position, Quaternion.identity);
                    temp.transform.SetParent(equippedItems.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInven.myInven[i], this);
                    }
                    found = true;
                }
                if (!found && i == playerInven.myInven.Count - 1)
                {
                    GameObject temp = Instantiate(blankInventorySlot, equippedItems.transform.position, Quaternion.identity);
                    temp.transform.SetParent(equippedItems.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInven.myInven[0], this);
                    }
                }
            }
            for (int i = 0; i < playerInven.myInven.Count; i++)
            {
                bool found = false;
                if (playerInven.myInven[i].numHeld > 0 && playerInven.myInven[i].isEquiped && playerInven.myInven[i].target == party.myParty[currentStats] &&
                    playerInven.myInven[i].itemType == 2)
                {
                    GameObject temp = Instantiate(blankInventorySlot, equippedItems.transform.position, Quaternion.identity);
                    temp.transform.SetParent(equippedItems.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInven.myInven[i], this);
                    }
                    found = true;
                }
                if (!found && i == playerInven.myInven.Count - 1)
                {
                    GameObject temp = Instantiate(blankInventorySlot, equippedItems.transform.position, Quaternion.identity);
                    temp.transform.SetParent(equippedItems.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInven.myInven[0], this);
                    }
                }
            }
            for (int i = 0; i < playerInven.myInven.Count; i++)
            {
                bool found = false;
                if (playerInven.myInven[i].numHeld > 0 && playerInven.myInven[i].isEquiped && playerInven.myInven[i].target == party.myParty[currentStats] &&
                    playerInven.myInven[i].itemType == 3)
                {
                    GameObject temp = Instantiate(blankInventorySlot, equippedItems.transform.position, Quaternion.identity);
                    temp.transform.SetParent(equippedItems.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInven.myInven[i], this);
                    }
                    found = true;
                }
                if (!found && i == playerInven.myInven.Count - 1)
                {
                    GameObject temp = Instantiate(blankInventorySlot, equippedItems.transform.position, Quaternion.identity);
                    temp.transform.SetParent(equippedItems.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInven.myInven[0], this);
                    }
                }
            }
        }
        //goes through the inventory list and shows the equipped items in the right slots for the character that is currently selected
    }
    
    private void ClearEquipped()
    {
        for (int i = 0; i < equippedItems.transform.childCount; i++)
        {
            Destroy(equippedItems.transform.GetChild(i).gameObject);
        }
        //clears the items that are currently equipped
    }
}
