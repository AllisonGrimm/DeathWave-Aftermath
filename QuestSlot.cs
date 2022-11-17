using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSlot : MonoBehaviour
{
    [Header("UI stuff to change")]
    [SerializeField] private TextMeshProUGUI nameS;

    [Header("Variables from item")]
    public Quests thisQuest;
    public JournalMenu thisMenu;

    public void Setup(Quests newQuest, JournalMenu newMenu)//Sets the inventory slot up
    {
        thisQuest = newQuest;
        thisMenu = newMenu;
        nameS.text = thisQuest.questName;
    }

    public void ClickedOn()//passes the correct data back to the manager when the item is clicked on
    {
        if (thisQuest)
        {
            thisMenu.SetupQuest(thisQuest.questName,thisQuest.questDescription,thisQuest.currentStep,thisQuest);
        }
    }
}
