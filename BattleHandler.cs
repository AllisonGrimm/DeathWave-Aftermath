using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHandler : MonoBehaviour
{
    //list of possible spawn posistions for sprites
    [SerializeField] private SpriteRenderer enemyPos1;//only used if there is one enemy
    [SerializeField] private GameObject hpBar1;
    [SerializeField] private TextMeshProUGUI eHP1;
    [SerializeField] private SpriteRenderer enemyPos2;
    [SerializeField] private GameObject hpBar2;
    [SerializeField] private TextMeshProUGUI eHP2;
    [SerializeField] private SpriteRenderer enemyPos3;
    [SerializeField] private GameObject hpBar3;
    [SerializeField] private TextMeshProUGUI eHP3;
    [SerializeField] private SpriteRenderer enemyPos4;
    [SerializeField] private GameObject hpBar4;
    [SerializeField] private TextMeshProUGUI eHP4;
    [SerializeField] private SpriteRenderer enemyPos5;
    [SerializeField] private GameObject hpBar5;
    [SerializeField] private TextMeshProUGUI eHP5;

    [SerializeField] private SpriteRenderer playerPos1;//only used if there is one one party memeber
    [SerializeField] private GameObject eHPBar1;
    [SerializeField] private TextMeshProUGUI pHP1;
    [SerializeField] private TextMeshProUGUI pNRG1;
    [SerializeField] private SpriteRenderer playerPos2;
    [SerializeField] private GameObject eHPBar2;
    [SerializeField] private TextMeshProUGUI pHP2;
    [SerializeField] private TextMeshProUGUI pNRG2;
    [SerializeField] private SpriteRenderer playerPos3;
    [SerializeField] private GameObject eHPBar3;
    [SerializeField] private TextMeshProUGUI pHP3;
    [SerializeField] private TextMeshProUGUI pNRG3;
    [SerializeField] private SpriteRenderer playerPos4;
    [SerializeField] private GameObject eHPBar4;
    [SerializeField] private TextMeshProUGUI pHP4;
    [SerializeField] private TextMeshProUGUI pNRG4;
    [SerializeField] private SpriteRenderer playerPos5;
    [SerializeField] private GameObject eHPBar5;
    [SerializeField] private TextMeshProUGUI pHP5;
    [SerializeField] private TextMeshProUGUI pNRG5;

    [SerializeField] private CurrentParty party;
    [SerializeField] private EnemyGroup group;

    [SerializeField] private GameObject normalAttack;
    [SerializeField] private GameObject skills;
    [SerializeField] private GameObject items;
    [SerializeField] private GameObject run;
    [SerializeField] private TextMeshProUGUI boxText;

    // Start is called before the first frame update
    void Start()
    {
        SetupBattle();
    }

    void SetupBattle()
    {
        PlayerPartySetup();
        EnemyGroupSetup();

        DetermineTurn();
        //go to first turn
    }

    private void PlayerPartySetup()
    {
        for (int i = 0; i < party.myParty.Count; i++)//sets posistions of player party and hp/energy bar with values
        {
            if (party.myParty.Count == 1)
            {
                playerPos1.sprite = party.myParty[i].battleSprite;
                eHPBar1.gameObject.SetActive(true);
                pHP1.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                pNRG1.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
                party.myParty[i].currentSpeed = Random.Range(1, 20) + party.myParty[i].speed;
                //if only one party member
            }
            else
            {
                if (i == 0)
                {
                    playerPos2.sprite = party.myParty[i].battleSprite;
                    eHPBar2.gameObject.SetActive(true);
                    pHP2.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                    pNRG2.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
                    party.myParty[i].currentSpeed = Random.Range(1, 20) + party.myParty[i].speed;
                }
                else if (i == 1)
                {
                    playerPos3.sprite = party.myParty[i].battleSprite;
                    eHPBar3.gameObject.SetActive(true);
                    pHP3.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                    pNRG3.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
                    party.myParty[i].currentSpeed = Random.Range(1, 20) + party.myParty[i].speed;
                }
                else if (i == 2)
                {
                    playerPos4.sprite = party.myParty[i].battleSprite;
                    eHPBar4.gameObject.SetActive(true);
                    pHP4.text = "Hp " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                    pNRG4.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
                    party.myParty[i].currentSpeed = Random.Range(1, 20) + party.myParty[i].speed;
                }
                else
                {
                    playerPos5.sprite = party.myParty[i].battleSprite;
                    eHPBar5.gameObject.SetActive(true);
                    pHP5.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                    pNRG5.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
                    party.myParty[i].currentSpeed = Random.Range(1, 20) + party.myParty[i].speed;
                }
            }
        }

    }

    private void EnemyGroupSetup()
    {
        for (int i = 0; i < group.enemyGroup.Count; i++)//sets enemy positions and hp bar with values
        {
            if (group.enemyGroup.Count == 1)
            {
                enemyPos1.sprite = group.enemyGroup[i].enemySprite;
                hpBar1.gameObject.SetActive(true);
                eHP1.text = "HP " + group.enemyGroup[i].hpCurrent + "/" + group.enemyGroup[i].hpMax;
                group.enemyGroup[i].currentSpeed = Random.Range(1, 20) + group.enemyGroup[i].speed;
                //if only one enemy
            }
            else
            {
                if (i == 0)
                {
                    enemyPos2.sprite = group.enemyGroup[i].enemySprite;
                    hpBar2.gameObject.SetActive(true);
                    eHP2.text = "HP " + group.enemyGroup[i].hpCurrent + "/" + group.enemyGroup[i].hpMax;
                    group.enemyGroup[i].currentSpeed = Random.Range(1, 20) + group.enemyGroup[i].speed;
                }
                else if (i == 1)
                {
                    enemyPos3.sprite = group.enemyGroup[i].enemySprite;
                    hpBar3.gameObject.SetActive(true);
                    eHP3.text = "HP " + group.enemyGroup[i].hpCurrent + "/" + group.enemyGroup[i].hpMax;
                    group.enemyGroup[i].currentSpeed = Random.Range(1, 20) + group.enemyGroup[i].speed;
                }
                else if (i == 2)
                {
                    enemyPos4.sprite = group.enemyGroup[i].enemySprite;
                    hpBar4.gameObject.SetActive(true);
                    eHP4.text = "HP " + group.enemyGroup[i].hpCurrent + "/" + group.enemyGroup[i].hpMax;
                    group.enemyGroup[i].currentSpeed = Random.Range(1, 20) + group.enemyGroup[i].speed;
                }
                else
                {
                    enemyPos5.sprite = group.enemyGroup[i].enemySprite;
                    hpBar5.gameObject.SetActive(true);
                    eHP5.text = "HP " + group.enemyGroup[i].hpCurrent + "/" + group.enemyGroup[i].hpMax;
                    group.enemyGroup[i].currentSpeed = Random.Range(1, 20) + group.enemyGroup[i].speed;
                }
            }
        }

    }

    private void DetermineTurn()
    {
        int highestPlay = 0;
        int highestEnemy = 0;
        Stats highPlay = null;
        EnemyStats highEnemy = null;
        for(int i = 0;i<party.myParty.Count;i++)//Finds the party member with the highest speed who hasn't gone
        {
            if(party.myParty[i].currentSpeed>highestPlay&&!party.myParty[i].hasGone)
            {
                highestPlay = party.myParty[i].currentSpeed;
                highPlay = party.myParty[i];
            }
        }
        for(int i = 0; i<group.enemyGroup.Count;i++)//Finds the enemy with the highest speed who hasn't gone
        {
            if(group.enemyGroup[i].currentSpeed>highestEnemy&&!group.enemyGroup[i].hasGone)
            {
                highestEnemy = group.enemyGroup[i].currentSpeed;
                highEnemy = group.enemyGroup[i];
            }
        }
        if(highestPlay == 0 && highestEnemy == 0)//resets turn order if all have gone
        {
            for (int i = 0; i < party.myParty.Count; i++)
            {
                party.myParty[i].hasGone = false;
            }
            for (int i = 0; i < group.enemyGroup.Count; i++)
            {
                group.enemyGroup[i].hasGone = false;
            }
            for (int i = 0; i < party.myParty.Count; i++)
            {
                if (party.myParty[i].currentSpeed > highestPlay && !party.myParty[i].hasGone)
                {
                    highestPlay = party.myParty[i].currentSpeed;
                    highPlay = party.myParty[i];
                }
            }
            for (int i = 0; i < group.enemyGroup.Count; i++)
            {
                if (group.enemyGroup[i].currentSpeed > highestEnemy && !group.enemyGroup[i].hasGone)
                {
                    highestEnemy = group.enemyGroup[i].currentSpeed;
                    highEnemy = group.enemyGroup[i];
                }
            }
        }
        if(highestEnemy>highestPlay)//Determines who goes based on comparing the party member and enemy
        {
            //damage/healing status effects
            if(highEnemy.regen)
            {
                highEnemy.hpCurrent += highEnemy.regenAmount;
                if(highEnemy.hpCurrent > Mathf.RoundToInt(highEnemy.hpMax))
                {
                    highEnemy.hpCurrent = Mathf.RoundToInt(highEnemy.hpMax);
                }
                highEnemy.regenStacks -= 1;
                if(highEnemy.regenStacks == 0)
                {
                    highEnemy.regen = false;
                }
            }
            if(highEnemy.bleed)
            {
                highEnemy.hpCurrent -= highEnemy.bleedAmount;
                highEnemy.bleedStacks -= 1;
                if(highEnemy.bleedStacks == 0)
                {
                    highEnemy.bleed = false;
                }
            }
            if(highEnemy.poison)
            {
                highEnemy.hpCurrent -= highEnemy.poisonAmount;
                highEnemy.poisonAmount -= 1;
                if(highEnemy.poisonAmount == 0)
                {
                    highEnemy.poison = false;
                }
            }
            //kill stuff and end battle stuff if no enemies left
            //lower braced buff stacks
            highEnemy.hasGone = true;
            if(highEnemy.stun)
            {
                //display x is stunned
                highEnemy.stunStacks -= 1;
                if(highEnemy.stunStacks == 0)
                {
                    highEnemy.stun = false;
                }
            }
            else
            {
                highEnemy.BattleFunction();
                //highlights the enemy that is going
            }
            //lower debuff stacks
        }
        else
        {
            if (highPlay.regen)
            {
                highPlay.hpCurrent += highPlay.regenAmount;
                if (highPlay.hpCurrent > Mathf.RoundToInt(highPlay.hpMax))
                {
                    highPlay.hpCurrent = Mathf.RoundToInt(highPlay.hpMax);
                }
                highPlay.regenStacks -= 1;
                if (highPlay.regenStacks == 0)
                {
                    highPlay.regen = false;
                }
            }
            if (highPlay.bleed)
            {
                highPlay.hpCurrent -= highPlay.bleedAmount;
                highPlay.bleedStacks -= 1;
                if (highPlay.bleedStacks == 0)
                {
                    highPlay.bleed = false;
                }
            }
            if (highPlay.poison)
            {
                highPlay.hpCurrent -= highPlay.poisonAmount;
                highPlay.poisonAmount -= 1;
                if (highPlay.poisonAmount == 0)
                {
                    highPlay.poison = false;
                }
            }
            //needs death stuff
            //damage/healing status effects
            highPlay.hasGone = true;
            if (highPlay.stun)
            {
                //display x is stunned
                highPlay.stunStacks -= 1;
                if(highPlay.stunStacks == 0)
                {
                    highPlay.stun = false;
                }
            }
            else
            {
                PlayerTurn(highPlay);
            }
        }
    }

    private void PlayerTurn(Stats currentTurn)
    {
        //highlights the character who's turn it is
        normalAttack.gameObject.SetActive(true);
        skills.gameObject.SetActive(true);
        items.gameObject.SetActive(true);
        run.gameObject.SetActive(true);
        boxText.text = currentTurn.memberName + "'s turn choose your action:";
        //skills will open a full selection menu
    }
}
