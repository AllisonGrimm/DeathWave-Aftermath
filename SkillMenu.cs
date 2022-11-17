using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject skillMenu;
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private SkillList list;//add some sort of passing in function so it gets the correct list
    [SerializeField] private GameObject blankSkillSlot;
    [SerializeField] private GameObject battle;
    [SerializeField] private GameObject targetWindowAlly;
    [SerializeField] private GameObject targetWindowEnemy;
    [SerializeField] private TMP_Dropdown allyTarget;
    [SerializeField] private TMP_Dropdown enemyTarget;
    private BattleHandler handler;
    [SerializeField] private CurrentParty party;
    private bool closed = false;

    private Stats currentTurn;
    private EnemyGroup group;

    private SkillTemplate currentSkill;

    void Start()
    {
        handler = battle.GetComponent(typeof (BattleHandler)) as BattleHandler;
        group = handler.group;
        currentTurn = handler.highPlay;
        allyTarget.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < handler.party.myParty.Count; i++)
        {
            options.Add(handler.party.myParty[i].memberName);
        }
        allyTarget.AddOptions(options);
        allyTarget.value = 0;
        allyTarget.RefreshShownValue();
        enemyTarget.ClearOptions();
        List<string> enemy = new List<string>();
        for (int i = 0; i < handler.group.enemyGroup.Count; i++)
        {
            enemy.Add(handler.group.enemyGroup[i].EnemyName);
        }
        enemyTarget.AddOptions(enemy);
        enemyTarget.value = 0;
        enemyTarget.RefreshShownValue();
        //list = currentTurn.skills; don't know why this doesn't work
        MakeSkillSlots();
        SetTextAndButton("", "", false);
    }

    private void Update()
    {
        if(closed)
        {
            SetTextAndButton("", "", false);
            closed = false;
        }
    }

    public void Closed()
    {
        closed = true;
    }

    private void SetTextAndButton(string desc, string name, bool buttonActiveUse)
    {
        descText.text = desc;
        nameText.text = name;
        if (buttonActiveUse)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    private void MakeSkillSlots()
    {
        if(list)
        {
            for(int i = 0; i<list.skillList.Count;i++)
            {
                GameObject temp = Instantiate(blankSkillSlot, skillPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(skillPanel.transform);
                SkillSlot newSlot = temp.GetComponent<SkillSlot>();
                if (newSlot)
                {
                    newSlot.Setup(list.skillList[i], this);
                }
            }
        }
    }

    public void SetupSkill(string description, string name, SkillTemplate skill)
    {
        currentTurn = handler.highPlay;
        currentSkill = skill;
        descText.text = description;
        nameText.text = name;
        if (currentTurn.energyCurrent>=skill.energyUse && currentTurn.magCurrent>= skill.ammoUse)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    public void Use()
    {
        if(currentSkill.targetAlly&&!currentSkill.noTarget)
        {
            targetWindowAlly.gameObject.SetActive(true);
        }
        else if(!currentSkill.targetAlly && !currentSkill.noTarget)
        {
            targetWindowEnemy.gameObject.SetActive(true);
        }
        else
        {
            Confirm();
        }
        SetTextAndButton("", "", false);
        skillMenu.gameObject.SetActive(false);
    }

    public void Confirm()
    {
        StartCoroutine(ConfirmEnum());
    }

    IEnumerator ConfirmEnum()
    {
        if(currentSkill.targetAlly&&!currentSkill.noTarget)
        {
            currentSkill.user = currentTurn;
            currentSkill.target = party.myParty[allyTarget.value];
        }
        else if(!currentSkill.targetAlly&&!currentSkill.noTarget)
        {
            currentSkill.eTarget = group.enemyGroup[enemyTarget.value];
            currentSkill.user = currentTurn;
            currentSkill.group = group;
        }
        else
        {
            currentSkill.user = currentTurn;
        }
        currentSkill.Use();
        //convert to enum dispaly skill effect        
        targetWindowAlly.gameObject.SetActive(false);
        targetWindowEnemy.gameObject.SetActive(false);
        handler.genBox.gameObject.SetActive(true);
        handler.normalAttack.gameObject.SetActive(false);
        handler.skills.gameObject.SetActive(false);
        handler.items.gameObject.SetActive(false);
        handler.run.gameObject.SetActive(false);
        handler.reload.gameObject.SetActive(false);
        handler.UpdateHP();
        /*GameObject temp = Instantiate(currentSkill.animation, inventoryPanel.transform.position, Quaternion.identity);
        temp.transform.SetParent(inventoryPanel.transform); use this to play anim?*/
        handler.boxText.text = currentSkill.boxText;
        yield return new WaitForSeconds(2f);
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

    public void Cancel()
    {
        targetWindowAlly.gameObject.SetActive(false);
        targetWindowEnemy.gameObject.SetActive(false);
        skillMenu.gameObject.SetActive(true);
    }
}
