using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [Header("UI stuff to change")]
    [SerializeField] private TextMeshProUGUI nameS;

    [Header("Variables from item")]
    public SkillTemplate thisSkill;
    public SkillMenu thisMenu;

    public void Setup(SkillTemplate newSkill, SkillMenu newMenu)//Sets the inventory slot up
    {
        thisSkill = newSkill;
        thisMenu = newMenu;
        nameS.text = thisSkill.skillName;
    }

    public void ClickedOn()//passes the correct data back to the manager when the item is clicked on
    {
        if (thisSkill)
        {
            thisMenu.SetupSkill(thisSkill.description,thisSkill.skillName, thisSkill);
        }
    }
}
