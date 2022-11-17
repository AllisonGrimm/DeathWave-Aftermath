using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemMenu : MonoBehaviour
{
    [Header("Item Info")]
    public PlayerInventory playerInven;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject fullInven;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject confirmWindow;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private GameObject useButton;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI numText;
    [SerializeField] private CurrentParty party;
    [SerializeField] private EnemyGroup group;
    [SerializeField] private TMP_Dropdown targetSelect;
    public InventoryItem currentItem;
    [SerializeField] private GameObject battle;
    [SerializeField] private GameObject dropdown;

    private void SetTextAndButton(string desc, string name, string num, bool buttonActiveUse)//code to set the texts and buttons
    {
        descText.text = desc;
        nameText.text = name;
        numText.text = num;
        targetSelect.gameObject.SetActive(false);
        confirmWindow.gameObject.SetActive(false);
        if (buttonActiveUse)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    private void MakeInventorySlots()//pulls the data from the inventory scriptable object and displays the items
    {
        if (playerInven)
        {
            for (int i = 0; i < playerInven.myInven.Count; i++)
            {
                if (playerInven.myInven[i].numHeld > 0 && !playerInven.myInven[i].isEquiped)
                {
                    GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    ItemSlot newSlot = temp.GetComponent<ItemSlot>();
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
        SetTextAndButton("", "", "", false);
        targetSelect.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < party.myParty.Count; i++)
        {
            options.Add(party.myParty[i].memberName);
        }
        targetSelect.AddOptions(options);
        targetSelect.value = 0;
        targetSelect.RefreshShownValue();
    }

    public void SetupItem(string newDesc, string newName, string newNum, bool isConsumable, InventoryItem newItem)
        //the code to change the display when the item is selected
    {
        currentItem = newItem;
        descText.text = newDesc;
        nameText.text = newName;
        numText.text = "x " + newNum;
        if (isConsumable)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    public void UseButtonPress()//if the use button is pressed
    {
        if (currentItem)
        {
            targetSelect.gameObject.SetActive(true);
            fullInven.gameObject.SetActive(false);
            confirmWindow.gameObject.SetActive(true);
            if(currentItem.isFightExclusive)
            {
                dropdown.gameObject.SetActive(false);
            }
        }
    }

    private void ClearInvenSlots()//clears what is currently displayed in the inventory
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void TargetSelect()
    {
        StartCoroutine(TargetSelectEnum());
    }

    IEnumerator TargetSelectEnum()//may need to turn into an enum to add wait
    {
        //needs to determine if targeting enemies or allies if needed
        Stats target;
        target = party.myParty[targetSelect.value];
        currentItem.Use(target);// have text stored on item?
        ClearInvenSlots();//Update number left if used consumable
        MakeInventorySlots();
        SetTextAndButton("", "", "", false);
        confirmWindow.gameObject.SetActive(false);
        dropdown.gameObject.SetActive(true);
        targetSelect.gameObject.SetActive(false);
        BattleHandler handler = battle.GetComponent(typeof (BattleHandler)) as BattleHandler;
        handler.genBox.gameObject.SetActive(true);
        handler.normalAttack.gameObject.SetActive(false);
        handler.skills.gameObject.SetActive(false);
        handler.items.gameObject.SetActive(false);
        handler.run.gameObject.SetActive(false);
        handler.reload.gameObject.SetActive(false);
        handler.UpdateHP();
        handler.boxText.text = currentItem.boxText;
        currentItem.boxText = "";//needs to update the hp/energy of everything
        yield return new WaitForSeconds(2f);//prolly need a wait for player input type system instead of waits
        if (handler.highPlay.broken)
        {
            handler.highPlay.brokenStacks -= 1;
            if (handler.highPlay.brokenStacks == 0)
            {
                handler.highPlay.broken = false;
            }
        }
        if (handler.highPlay.blind)
        {
            handler.highPlay.blindStacks -= 1;
            if (handler.highPlay.blindStacks == 0)
            {
                handler.highPlay.blind = false;
            }
        }
        if (handler.highPlay.weak)
        {
            handler.highPlay.weakStacks -= 1;
            if (handler.highPlay.weakStacks == 0)
            {
                handler.highPlay.weak = false;
            }
        }
        handler.DetermineTurn();
    }

    public void X()
    {
        SetTextAndButton("", "", "", false);
        ClearInvenSlots();
        MakeInventorySlots();
    }
    
    public void Cancel()
    {
        fullInven.gameObject.SetActive(true);
        confirmWindow.gameObject.SetActive(false);
        targetSelect.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(true);
    }
}
