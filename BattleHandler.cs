using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleHandler : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject itemCanvas;
    [SerializeField] private GameObject skillCanvas;

    [Header("Status Effects")]
    [SerializeField] private GameObject adrenaline;
    [SerializeField] private GameObject bleed;
    [SerializeField] private GameObject blind;
    [SerializeField] private GameObject braced;
    [SerializeField] private GameObject broken;
    [SerializeField] private GameObject impervious;
    [SerializeField] private GameObject poison;
    [SerializeField] private GameObject regen;
    [SerializeField] private GameObject strengthened;
    [SerializeField] private GameObject stunned;
    [SerializeField] private GameObject weakened;

    [Header("Enemy Fields")]
    //list of possible spawn posistions for sprites
    [SerializeField] private SpriteRenderer enemyPos1;//only used if there is one enemy
    private EnemyStats enemy1;
    [SerializeField] private GameObject hpBar1;
    [SerializeField] private GameObject hpHighlight1;
    [SerializeField] private GameObject hpHighlightT1;
    [SerializeField] private TextMeshProUGUI eHP1;
    [SerializeField] private TextMeshProUGUI eName1;
    [SerializeField] private GameObject e1Status;
    [SerializeField] private SpriteRenderer enemyPos2;
    private EnemyStats enemy2;
    [SerializeField] private GameObject hpBar2;
    [SerializeField] private GameObject hpHighlight2;
    [SerializeField] private GameObject hpHighlightT2;
    [SerializeField] private TextMeshProUGUI eHP2;
    [SerializeField] private TextMeshProUGUI eName2;
    [SerializeField] private GameObject e2Status;
    [SerializeField] private SpriteRenderer enemyPos3;
    private EnemyStats enemy3;
    [SerializeField] private GameObject hpBar3;
    [SerializeField] private GameObject hpHighlight3;
    [SerializeField] private GameObject hpHighlightT3;
    [SerializeField] private TextMeshProUGUI eHP3;
    [SerializeField] private TextMeshProUGUI eName3;
    [SerializeField] private GameObject e3Status;
    [SerializeField] private SpriteRenderer enemyPos4;
    private EnemyStats enemy4;
    [SerializeField] private GameObject hpBar4;
    [SerializeField] private GameObject hpHighlight4;
    [SerializeField] private GameObject hpHighlightT4;
    [SerializeField] private TextMeshProUGUI eHP4;
    [SerializeField] private TextMeshProUGUI eName4;
    [SerializeField] private GameObject e4Status;
    [SerializeField] private SpriteRenderer enemyPos5;
    private EnemyStats enemy5;
    [SerializeField] private GameObject hpBar5;
    [SerializeField] private GameObject hpHighlight5;
    [SerializeField] private GameObject hpHighlightT5;
    [SerializeField] private TextMeshProUGUI eHP5;
    [SerializeField] private TextMeshProUGUI eName5;
    [SerializeField] private GameObject e5Status;

    [Header("Player Fields")]
    [SerializeField] private SpriteRenderer playerPos1;//only used if there is one one party memeber
    [SerializeField] private GameObject eHPBar1;
    [SerializeField] private TextMeshProUGUI pHP1;
    [SerializeField] private TextMeshProUGUI pNRG1;
    [SerializeField] private TextMeshProUGUI ammo1;
    [SerializeField] private GameObject p1Status;
    private Stats p1;
    [SerializeField] private SpriteRenderer playerPos2;
    [SerializeField] private GameObject eHPBar2;
    [SerializeField] private TextMeshProUGUI pHP2;
    [SerializeField] private TextMeshProUGUI pNRG2;
    [SerializeField] private TextMeshProUGUI ammo2;
    [SerializeField] private GameObject p2Status;
    private Stats p2;
    [SerializeField] private SpriteRenderer playerPos3;
    [SerializeField] private GameObject eHPBar3;
    [SerializeField] private TextMeshProUGUI pHP3;
    [SerializeField] private TextMeshProUGUI pNRG3;
    [SerializeField] private TextMeshProUGUI ammo3;
    [SerializeField] private GameObject p3Status;
    private Stats p3;
    [SerializeField] private SpriteRenderer playerPos4;
    [SerializeField] private GameObject eHPBar4;
    [SerializeField] private TextMeshProUGUI pHP4;
    [SerializeField] private TextMeshProUGUI pNRG4;
    [SerializeField] private TextMeshProUGUI ammo4;
    [SerializeField] private GameObject p4Status;
    private Stats p4;
    [SerializeField] private SpriteRenderer playerPos5;
    [SerializeField] private GameObject eHPBar5;
    [SerializeField] private TextMeshProUGUI pHP5;
    [SerializeField] private TextMeshProUGUI pNRG5;
    [SerializeField] private TextMeshProUGUI ammo5;
    [SerializeField] private GameObject p5Status;
    private Stats p5;

    [Header("Group Data")]
    [SerializeField] public CurrentParty party;
    private int KnockedAmount = 0;
    [SerializeField] public EnemyGroup group;
    [SerializeField] private EnemyLoad load;
    private int KilledAmount = 0;
    private bool gameOver = false;

    private int xpGiven = 0;//add to count whenever an enemy dies
    private int monReward = 0;

    [Header("Text Box")]
    [SerializeField] public GameObject genBox;
    [SerializeField] public GameObject normalAttack;
    [SerializeField] public GameObject skills;
    [SerializeField] public GameObject items;
    [SerializeField] public GameObject run;
    [SerializeField] public GameObject reload;
    [SerializeField] public TextMeshProUGUI boxText;
    [SerializeField] private GameObject endTextBox;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private BattleResults results;

    public Stats highPlay = null;
    public EnemyStats highEnemy = null;
    public EnemyStats target = null;
    public Stats playTarget = null;

    // remember to reset all the has gones on party when battle done
    void Start()
    {
        group = load.group;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetupBattle();
    }

    void SetupBattle()
    {
        PlayerPartySetup();
        EnemyGroupSetup();

        DetermineTurn();
    }

    private void PlayerPartySetup()
    {//needs check if any are ko'd
        for (int i = 0; i < party.myParty.Count; i++)//sets posistions of player party and hp/energy bar with values
        {
            if (party.myParty.Count == 1)
            {
                p1 = party.myParty[i];
                playerPos1.sprite = party.myParty[i].battleSprite;
                eHPBar1.gameObject.SetActive(true);
                pHP1.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                pNRG1.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
                ammo1.text = party.myParty[i].magCurrent + "\n/ \n" + party.myParty[i].magSize;
                party.myParty[i].currentSpeed = Random.Range(1, 20) + party.myParty[i].speed;
                //if only one party member
            }
            else
            {
                if (i == 0)
                {
                    p2 = party.myParty[i];
                    playerPos2.sprite = party.myParty[i].battleSprite;
                    eHPBar2.gameObject.SetActive(true);
                    pHP2.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                    pNRG2.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
                    ammo2.text = party.myParty[i].magCurrent + "\n/ \n" + party.myParty[i].magSize;
                    party.myParty[i].currentSpeed = Random.Range(1, 20) + party.myParty[i].speed;
                }
                else if (i == 1)
                {
                    p3 = party.myParty[i];
                    playerPos3.sprite = party.myParty[i].battleSprite;
                    eHPBar3.gameObject.SetActive(true);
                    pHP3.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                    pNRG3.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
                    ammo3.text = party.myParty[i].magCurrent + "\n/ \n" + party.myParty[i].magSize;
                    party.myParty[i].currentSpeed = Random.Range(1, 20) + party.myParty[i].speed;
                }
                else if (i == 2)
                {
                    p4 = party.myParty[i];
                    playerPos4.sprite = party.myParty[i].battleSprite;
                    eHPBar4.gameObject.SetActive(true);
                    pHP4.text = "Hp " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                    pNRG4.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
                    ammo4.text = party.myParty[i].magCurrent + "\n/ \n" + party.myParty[i].magSize;
                    party.myParty[i].currentSpeed = Random.Range(1, 20) + party.myParty[i].speed;
                }
                else
                {
                    p5 = party.myParty[i];
                    playerPos5.sprite = party.myParty[i].battleSprite;
                    eHPBar5.gameObject.SetActive(true);
                    pHP5.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                    pNRG5.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
                    ammo5.text = party.myParty[i].magCurrent + "\n/ \n" + party.myParty[i].magSize;
                    party.myParty[i].currentSpeed = Random.Range(1, 20) + party.myParty[i].speed;
                }
            }
        }

    }

    private void EnemyGroupSetup()
    {
        for (int i = 0; i < group.enemyGroup.Count; i++)//sets enemy positions and hp bar with values
        {
            xpGiven += group.enemyGroup[i].xpAmount;
            if (group.enemyGroup.Count == 1)
            {
                enemyPos1.sprite = group.enemyGroup[i].enemySprite;
                enemy1 = group.enemyGroup[i];
                hpBar1.gameObject.SetActive(true);
                eHP1.text = "HP " + group.enemyGroup[i].hpCurrent + "/" + group.enemyGroup[i].hpMax;
                eName1.text = group.enemyGroup[i].EnemyName;
                group.enemyGroup[i].currentSpeed = Random.Range(1, 20) + group.enemyGroup[i].speed;
                //if only one enemy
            }
            else
            {
                if (i == 0)
                {
                    enemyPos2.sprite = group.enemyGroup[i].enemySprite;
                    enemy2 = group.enemyGroup[i];
                    hpBar2.gameObject.SetActive(true);
                    eHP2.text = "HP " + group.enemyGroup[i].hpCurrent + "/" + group.enemyGroup[i].hpMax;
                    eName2.text = group.enemyGroup[i].EnemyName;
                    group.enemyGroup[i].currentSpeed = Random.Range(1, 20) + group.enemyGroup[i].speed;
                }
                else if (i == 1)
                {
                    enemyPos3.sprite = group.enemyGroup[i].enemySprite;
                    enemy3 = group.enemyGroup[i];
                    hpBar3.gameObject.SetActive(true);
                    eHP3.text = "HP " + group.enemyGroup[i].hpCurrent + "/" + group.enemyGroup[i].hpMax;
                    eName3.text = group.enemyGroup[i].EnemyName;
                    group.enemyGroup[i].currentSpeed = Random.Range(1, 20) + group.enemyGroup[i].speed;
                }
                else if (i == 2)
                {
                    enemyPos4.sprite = group.enemyGroup[i].enemySprite;
                    enemy4 = group.enemyGroup[i];
                    hpBar4.gameObject.SetActive(true);
                    eHP4.text = "HP " + group.enemyGroup[i].hpCurrent + "/" + group.enemyGroup[i].hpMax;
                    eName4.text = group.enemyGroup[i].EnemyName;
                    group.enemyGroup[i].currentSpeed = Random.Range(1, 20) + group.enemyGroup[i].speed;
                }
                else
                {
                    enemyPos5.sprite = group.enemyGroup[i].enemySprite;
                    enemy5 = group.enemyGroup[i];
                    hpBar5.gameObject.SetActive(true);
                    eHP5.text = "HP " + group.enemyGroup[i].hpCurrent + "/" + group.enemyGroup[i].hpMax;
                    eName5.text = group.enemyGroup[i].EnemyName;
                    group.enemyGroup[i].currentSpeed = Random.Range(1, 20) + group.enemyGroup[i].speed;
                }
            }
        }

    }

    public void DetermineTurn()
    {
        UpdateHP();
        for (int i = 0; i < party.myParty.Count; i++)
        {
            if (party.myParty[i].hpCurrent == 0)
            {
                PlayerDeath(i);
            }
        }
        for (int i = 0; i < group.enemyGroup.Count; i++)
        {
            if (group.enemyGroup[i].hpCurrent == 0)
            {
                EnemyDeath(i);
            }
        }
        int highestPlay = 0;
        int highestEnemy = 0;
        genBox.gameObject.SetActive(true);
        normalAttack.gameObject.SetActive(false);
        skills.gameObject.SetActive(false);
        items.gameObject.SetActive(false);
        run.gameObject.SetActive(false);
        reload.gameObject.SetActive(false);
        hpHighlight1.gameObject.SetActive(false);
        hpHighlight2.gameObject.SetActive(false);
        hpHighlight3.gameObject.SetActive(false);
        hpHighlight4.gameObject.SetActive(false);
        hpHighlight5.gameObject.SetActive(false);
        target = null;
        for (int i = 0; i < party.myParty.Count; i++)//Finds the party member with the highest speed who hasn't gone
        {
            if (party.myParty[i].currentSpeed > highestPlay && !party.myParty[i].hasGone && !party.myParty[i].knockedOut)
            {
                highestPlay = party.myParty[i].currentSpeed;
                highPlay = party.myParty[i];
            }
        }
        for (int i = 0; i < group.enemyGroup.Count; i++)//Finds the enemy with the highest speed who hasn't gone
        {
            if (group.enemyGroup[i].currentSpeed > highestEnemy && !group.enemyGroup[i].hasGone && !group.enemyGroup[i].dead)
            {
                highestEnemy = group.enemyGroup[i].currentSpeed;
                highEnemy = group.enemyGroup[i];
            }
        }
        if (highestPlay == 0 && highestEnemy == 0)//resets turn order if all have gone
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
        if (highestEnemy > highestPlay)//Determines who goes based on comparing the party member and enemy
        {
            normalAttack.gameObject.SetActive(false);
            skills.gameObject.SetActive(false);
            items.gameObject.SetActive(false);
            run.gameObject.SetActive(false);
            reload.gameObject.SetActive(false);
            //disable player input
            //damage/healing status effects
            highEnemy.hasGone = true;
            StartCoroutine(EnemyTurnStart());
        }
        else
        {
            if (highPlay.energyCurrent < highPlay.energyMax)
            {
                highPlay.energyCurrent += highPlay.energyRegen;
                if (highPlay.energyCurrent > highPlay.energyMax)
                {
                    highPlay.energyCurrent = Mathf.RoundToInt(highPlay.energyMax);
                }
            }
            StartCoroutine(PlayerTurnStart());
        }
    }

    private void UpdateStatusEffects()
    {
        TextMeshProUGUI text;
        if (enemy1 != null)
        {
            for (int i = 0; i < e1Status.transform.childCount; i++)
            {
                Destroy(e1Status.transform.GetChild(i).gameObject);
            }
            if (enemy1.adrenalin)
            {
                GameObject temp = Instantiate(adrenaline, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.adrenStacks.ToString();
            }
            if (enemy1.bleed)
            {
                GameObject temp = Instantiate(bleed, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.bleedStacks.ToString();
            }
            if (enemy1.blind)
            {
                GameObject temp = Instantiate(blind, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.blindStacks.ToString();
            }
            if (enemy1.braced)
            {
                GameObject temp = Instantiate(braced, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.bracedStacks.ToString();
            }
            if (enemy1.broken)
            {
                GameObject temp = Instantiate(broken, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.brokenStacks.ToString();
            }
            if (enemy1.impervious)
            {
                GameObject temp = Instantiate(impervious, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.impervStacks.ToString();
            }
            if (enemy1.poison)
            {
                GameObject temp = Instantiate(poison, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.poisonAmount.ToString();
            }
            if (enemy1.regen)
            {
                GameObject temp = Instantiate(regen, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.regenStacks.ToString();
            }
            if (enemy1.strengthened)
            {
                GameObject temp = Instantiate(strengthened, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.strStacks.ToString();
            }
            if (enemy1.stun)
            {
                GameObject temp = Instantiate(stunned, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.stunStacks.ToString();
            }
            if (enemy1.weak)
            {
                GameObject temp = Instantiate(weakened, e1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy1.weakStacks.ToString();
            }
        }
        if (enemy2 != null)
        {
            for (int i = 0; i < e2Status.transform.childCount; i++)
            {
                Destroy(e2Status.transform.GetChild(i).gameObject);
            }
            if (enemy2.adrenalin)
            {
                GameObject temp = Instantiate(adrenaline, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.adrenStacks.ToString();
            }
            if (enemy2.bleed)
            {
                GameObject temp = Instantiate(bleed, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.bleedStacks.ToString();
            }
            if (enemy2.blind)
            {
                GameObject temp = Instantiate(blind, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.blindStacks.ToString();
            }
            if (enemy2.braced)
            {
                GameObject temp = Instantiate(braced, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.bracedStacks.ToString();
            }
            if (enemy2.broken)
            {
                GameObject temp = Instantiate(broken, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.brokenStacks.ToString();
            }
            if (enemy2.impervious)
            {
                GameObject temp = Instantiate(impervious, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.impervStacks.ToString();
            }
            if (enemy2.poison)
            {
                GameObject temp = Instantiate(poison, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.poisonAmount.ToString();
            }
            if (enemy2.regen)
            {
                GameObject temp = Instantiate(regen, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.regenStacks.ToString();
            }
            if (enemy2.strengthened)
            {
                GameObject temp = Instantiate(strengthened, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.strStacks.ToString();
            }
            if (enemy2.stun)
            {
                GameObject temp = Instantiate(stunned, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.stunStacks.ToString();
            }
            if (enemy2.weak)
            {
                GameObject temp = Instantiate(weakened, e2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy2.weakStacks.ToString();
            }
        }
        if (enemy3 != null)
        {
            for (int i = 0; i < e3Status.transform.childCount; i++)
            {
                Destroy(e3Status.transform.GetChild(i).gameObject);
            }
            if (enemy3.adrenalin)
            {
                GameObject temp = Instantiate(adrenaline, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.adrenStacks.ToString();
            }
            if (enemy3.bleed)
            {
                GameObject temp = Instantiate(bleed, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.bleedStacks.ToString();
            }
            if (enemy3.blind)
            {
                GameObject temp = Instantiate(blind, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.blindStacks.ToString();
            }
            if (enemy3.braced)
            {
                GameObject temp = Instantiate(braced, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.bracedStacks.ToString();
            }
            if (enemy3.broken)
            {
                GameObject temp = Instantiate(broken, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.brokenStacks.ToString();
            }
            if (enemy3.impervious)
            {
                GameObject temp = Instantiate(impervious, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.impervStacks.ToString();
            }
            if (enemy3.poison)
            {
                GameObject temp = Instantiate(poison, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.poisonAmount.ToString();
            }
            if (enemy3.regen)
            {
                GameObject temp = Instantiate(regen, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.regenStacks.ToString();
            }
            if (enemy3.strengthened)
            {
                GameObject temp = Instantiate(strengthened, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.strStacks.ToString();
            }
            if (enemy3.stun)
            {
                GameObject temp = Instantiate(stunned, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.stunStacks.ToString();
            }
            if (enemy3.weak)
            {
                GameObject temp = Instantiate(weakened, e3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy3.weakStacks.ToString();
            }
        }
        if (enemy4 != null)
        {
            for (int i = 0; i < e4Status.transform.childCount; i++)
            {
                Destroy(e4Status.transform.GetChild(i).gameObject);
            }
            if (enemy4.adrenalin)
            {
                GameObject temp = Instantiate(adrenaline, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.adrenStacks.ToString();
            }
            if (enemy4.bleed)
            {
                GameObject temp = Instantiate(bleed, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.bleedStacks.ToString();
            }
            if (enemy4.blind)
            {
                GameObject temp = Instantiate(blind, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.blindStacks.ToString();
            }
            if (enemy4.braced)
            {
                GameObject temp = Instantiate(braced, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.bracedStacks.ToString();
            }
            if (enemy4.broken)
            {
                GameObject temp = Instantiate(broken, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.brokenStacks.ToString();
            }
            if (enemy4.impervious)
            {
                GameObject temp = Instantiate(impervious, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.impervStacks.ToString();
            }
            if (enemy4.poison)
            {
                GameObject temp = Instantiate(poison, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.poisonAmount.ToString();
            }
            if (enemy4.regen)
            {
                GameObject temp = Instantiate(regen, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.regenStacks.ToString();
            }
            if (enemy4.strengthened)
            {
                GameObject temp = Instantiate(strengthened, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.strStacks.ToString();
            }
            if (enemy4.stun)
            {
                GameObject temp = Instantiate(stunned, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.stunStacks.ToString();
            }
            if (enemy4.weak)
            {
                GameObject temp = Instantiate(weakened, e4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy4.weakStacks.ToString();
            }
        }
        if (enemy5 != null)
        {
            for (int i = 0; i < e5Status.transform.childCount; i++)
            {
                Destroy(e5Status.transform.GetChild(i).gameObject);
            }
            if (enemy5.adrenalin)
            {
                GameObject temp = Instantiate(adrenaline, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.adrenStacks.ToString();
            }
            if (enemy5.bleed)
            {
                GameObject temp = Instantiate(bleed, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.bleedStacks.ToString();
            }
            if (enemy5.blind)
            {
                GameObject temp = Instantiate(blind, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.blindStacks.ToString();
            }
            if (enemy5.braced)
            {
                GameObject temp = Instantiate(braced, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.bracedStacks.ToString();
            }
            if (enemy5.broken)
            {
                GameObject temp = Instantiate(broken, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.brokenStacks.ToString();
            }
            if (enemy5.impervious)
            {
                GameObject temp = Instantiate(impervious, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.impervStacks.ToString();
            }
            if (enemy5.poison)
            {
                GameObject temp = Instantiate(poison, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.poisonAmount.ToString();
            }
            if (enemy5.regen)
            {
                GameObject temp = Instantiate(regen, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.regenStacks.ToString();
            }
            if (enemy5.strengthened)
            {
                GameObject temp = Instantiate(strengthened, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.strStacks.ToString();
            }
            if (enemy5.stun)
            {
                GameObject temp = Instantiate(stunned, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.stunStacks.ToString();
            }
            if (enemy5.weak)
            {
                GameObject temp = Instantiate(weakened, e5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(e5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = enemy5.weakStacks.ToString();
            }
        }
        if (p1 != null)
        {
            for (int i = 0; i < p1Status.transform.childCount; i++)
            {
                Destroy(p1Status.transform.GetChild(i).gameObject);
            }
            if (p1.adrenalin)
            {
                GameObject temp = Instantiate(adrenaline, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.adrenStacks.ToString();
            }
            if (p1.bleed)
            {
                GameObject temp = Instantiate(bleed, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.bleedStacks.ToString();
            }
            if (p1.blind)
            {
                GameObject temp = Instantiate(blind, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.blindStacks.ToString();
            }
            if (p1.braced)
            {
                GameObject temp = Instantiate(braced, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.bracedStacks.ToString();
            }
            if (p1.broken)
            {
                GameObject temp = Instantiate(broken, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.brokenStacks.ToString();
            }
            if (p1.impervious)
            {
                GameObject temp = Instantiate(impervious, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.impervStacks.ToString();
            }
            if (p1.poison)
            {
                GameObject temp = Instantiate(poison, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.poisonAmount.ToString();
            }
            if (p1.regen)
            {
                GameObject temp = Instantiate(regen, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.regenStacks.ToString();
            }
            if (p1.strengthened)
            {
                GameObject temp = Instantiate(strengthened, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.strStacks.ToString();
            }
            if (p1.stun)
            {
                GameObject temp = Instantiate(stunned, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.stunStacks.ToString();
            }
            if (p1.weak)
            {
                GameObject temp = Instantiate(weakened, p1Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p1Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p1.weakStacks.ToString();
            }
        }
        if (p2 != null)
        {
            for (int i = 0; i < p2Status.transform.childCount; i++)
            {
                Destroy(p2Status.transform.GetChild(i).gameObject);
            }
            if (p2.adrenalin)
            {
                GameObject temp = Instantiate(adrenaline, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.adrenStacks.ToString();
            }
            if (p2.bleed)
            {
                GameObject temp = Instantiate(bleed, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.bleedStacks.ToString();
            }
            if (p2.blind)
            {
                GameObject temp = Instantiate(blind, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.blindStacks.ToString();
            }
            if (p2.braced)
            {
                GameObject temp = Instantiate(braced, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.bracedStacks.ToString();
            }
            if (p2.broken)
            {
                GameObject temp = Instantiate(broken, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.brokenStacks.ToString();
            }
            if (p2.impervious)
            {
                GameObject temp = Instantiate(impervious, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.impervStacks.ToString();
            }
            if (p2.poison)
            {
                GameObject temp = Instantiate(poison, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.poisonAmount.ToString();
            }
            if (p2.regen)
            {
                GameObject temp = Instantiate(regen, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.regenStacks.ToString();
            }
            if (p2.strengthened)
            {
                GameObject temp = Instantiate(strengthened, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.strStacks.ToString();
            }
            if (p2.stun)
            {
                GameObject temp = Instantiate(stunned, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.stunStacks.ToString();
            }
            if (p2.weak)
            {
                GameObject temp = Instantiate(weakened, p2Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p2Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p2.weakStacks.ToString();
            }
        }
        if (p3 != null)
        {
            for (int i = 0; i < p3Status.transform.childCount; i++)
            {
                Destroy(p3Status.transform.GetChild(i).gameObject);
            }
            if (p3.adrenalin)
            {
                GameObject temp = Instantiate(adrenaline, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.adrenStacks.ToString();
            }
            if (p3.bleed)
            {
                GameObject temp = Instantiate(bleed, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.bleedStacks.ToString();
            }
            if (p3.blind)
            {
                GameObject temp = Instantiate(blind, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.blindStacks.ToString();
            }
            if (p3.braced)
            {
                GameObject temp = Instantiate(braced, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.bracedStacks.ToString();
            }
            if (p3.broken)
            {
                GameObject temp = Instantiate(broken, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.brokenStacks.ToString();
            }
            if (p3.impervious)
            {
                GameObject temp = Instantiate(impervious, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.impervStacks.ToString();
            }
            if (p3.poison)
            {
                GameObject temp = Instantiate(poison, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.poisonAmount.ToString();
            }
            if (p3.regen)
            {
                GameObject temp = Instantiate(regen, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.regenStacks.ToString();
            }
            if (p3.strengthened)
            {
                GameObject temp = Instantiate(strengthened, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.strStacks.ToString();
            }
            if (p3.stun)
            {
                GameObject temp = Instantiate(stunned, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.stunStacks.ToString();
            }
            if (p3.weak)
            {
                GameObject temp = Instantiate(weakened, p3Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p3Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p3.weakStacks.ToString();
            }
        }
        if (p4 != null)
        {
            for (int i = 0; i < p4Status.transform.childCount; i++)
            {
                Destroy(p4Status.transform.GetChild(i).gameObject);
            }
            if (p4.adrenalin)
            {
                GameObject temp = Instantiate(adrenaline, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.adrenStacks.ToString();
            }
            if (p4.bleed)
            {
                GameObject temp = Instantiate(bleed, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.bleedStacks.ToString();
            }
            if (p4.blind)
            {
                GameObject temp = Instantiate(blind, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.blindStacks.ToString();
            }
            if (p4.braced)
            {
                GameObject temp = Instantiate(braced, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.bracedStacks.ToString();
            }
            if (p4.broken)
            {
                GameObject temp = Instantiate(broken, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.brokenStacks.ToString();
            }
            if (p4.impervious)
            {
                GameObject temp = Instantiate(impervious, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.impervStacks.ToString();
            }
            if (p4.poison)
            {
                GameObject temp = Instantiate(poison, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.poisonAmount.ToString();
            }
            if (p4.regen)
            {
                GameObject temp = Instantiate(regen, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.regenStacks.ToString();
            }
            if (p4.strengthened)
            {
                GameObject temp = Instantiate(strengthened, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.strStacks.ToString();
            }
            if (p4.stun)
            {
                GameObject temp = Instantiate(stunned, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.stunStacks.ToString();
            }
            if (p4.weak)
            {
                GameObject temp = Instantiate(weakened, p4Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p4Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p4.weakStacks.ToString();
            }
        }
        if (p5 != null)
        {
            for (int i = 0; i < p5Status.transform.childCount; i++)
            {
                Destroy(p5Status.transform.GetChild(i).gameObject);
            }
            if (p5.adrenalin)
            {
                GameObject temp = Instantiate(adrenaline, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.adrenStacks.ToString();
            }
            if (p5.bleed)
            {
                GameObject temp = Instantiate(bleed, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.bleedStacks.ToString();
            }
            if (p5.blind)
            {
                GameObject temp = Instantiate(blind, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.blindStacks.ToString();
            }
            if (p5.braced)
            {
                GameObject temp = Instantiate(braced, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.bracedStacks.ToString();
            }
            if (p5.broken)
            {
                GameObject temp = Instantiate(broken, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.brokenStacks.ToString();
            }
            if (p5.impervious)
            {
                GameObject temp = Instantiate(impervious, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.impervStacks.ToString();
            }
            if (p5.poison)
            {
                GameObject temp = Instantiate(poison, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.poisonAmount.ToString();
            }
            if (p5.regen)
            {
                GameObject temp = Instantiate(regen, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.regenStacks.ToString();
            }
            if (p5.strengthened)
            {
                GameObject temp = Instantiate(strengthened, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.strStacks.ToString();
            }
            if (p5.stun)
            {
                GameObject temp = Instantiate(stunned, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.stunStacks.ToString();
            }
            if (p5.weak)
            {
                GameObject temp = Instantiate(weakened, p5Status.transform.position, Quaternion.identity);
                temp.transform.SetParent(p5Status.transform);
                text = temp.GetComponentInChildren<TextMeshProUGUI>();
                text.text = p5.weakStacks.ToString();
            }
        }
    }

    IEnumerator PlayerStun()
    {
        boxText.text = highPlay.memberName + " is stunned.";
        yield return new WaitForSeconds(2f);
        highPlay.stunStacks -= 1;
        if (highPlay.stunStacks == 0)
        {
            highPlay.stun = false;
        }
        Debug.Log("stunned");
        LowerDebuffs();
        UpdateStatusEffects();
        DetermineTurn();
    }

    IEnumerator EnemyStun()
    {
        boxText.text = highEnemy.EnemyName + " is stunned.";
        yield return new WaitForSeconds(2f);
        highEnemy.stunStacks -= 1;
        if (highEnemy.stunStacks == 0)
        {
            highEnemy.stun = false;
        }
        UpdateStatusEffects();
    }

    IEnumerator PlayerTurnStart()
    {
        UpdateStatusEffects();
        if (highPlay.regen)
        {
            highPlay.hpCurrent += highPlay.regenAmount;
            if (highPlay.hpCurrent > highPlay.hpMax)
            {
                highPlay.hpCurrent = Mathf.RoundToInt(highPlay.hpMax);
            }
            UpdateHP();
            boxText.text = highPlay.memberName + " regens " + highPlay.regenAmount + " hp";
            yield return new WaitForSeconds(2f);
            if (highPlay.hpCurrent > Mathf.RoundToInt(highPlay.hpMax))
            {
                highPlay.hpCurrent = Mathf.RoundToInt(highPlay.hpMax);
            }
            highPlay.regenStacks -= 1;
            if (highPlay.regenStacks == 0)
            {
                highPlay.regen = false;
            }
            UpdateStatusEffects();
        }
        if (highPlay.bleed)
        {
            highPlay.hpCurrent -= highPlay.bleedAmount;
            UpdateHP();
            boxText.text = highPlay.memberName + " bleeds for " + highPlay.bleedAmount + " damage";
            yield return new WaitForSeconds(2f);
            highPlay.bleedStacks -= 1;
            if (highPlay.bleedStacks == 0)
            {
                highPlay.bleed = false;
            }
            UpdateStatusEffects();
        }
        if (highPlay.poison)
        {
            highPlay.hpCurrent -= highPlay.poisonAmount;
            UpdateHP();
            boxText.text = highPlay.memberName + " is poisoned for " + highPlay.poisonAmount + " damage";
            yield return new WaitForSeconds(2f);
            highPlay.poisonAmount -= 1;
            if (highPlay.poisonAmount == 0)
            {
                highPlay.poison = false;
            }
            UpdateStatusEffects();
        }
        for (int i = 0; i < party.myParty.Count; i++)
        {
            if (party.myParty[i].hpCurrent == 0)
            {
                PlayerDeath(i);
            }
        }
        //damage/healing status effects
        highPlay.hasGone = true;
        if (highPlay.stun)//is somehow broken
        {
            StartCoroutine(PlayerStun());

        }
        else if (!gameOver)
        {
            PlayerTurn();
        }
    }

    IEnumerator EnemyTurnStart()
    {
        UpdateStatusEffects();
        if (highEnemy.regen)
        {
            highEnemy.hpCurrent += highEnemy.regenAmount;
            boxText.text = highEnemy.EnemyName + " regens " + highEnemy.regenAmount + " hp";
            yield return new WaitForSeconds(2f);
            if (highEnemy.hpCurrent > Mathf.RoundToInt(highEnemy.hpMax))
            {
                highEnemy.hpCurrent = Mathf.RoundToInt(highEnemy.hpMax);
            }
            highEnemy.regenStacks -= 1;
            if (highEnemy.regenStacks == 0)
            {
                highEnemy.regen = false;
            }
            UpdateStatusEffects();
        }
        if (highEnemy.bleed)
        {
            highEnemy.hpCurrent -= highEnemy.bleedAmount;
            boxText.text = highEnemy.EnemyName + " bleeds for " + highEnemy.bleedAmount + " damage";
            yield return new WaitForSeconds(2f);
            highEnemy.bleedStacks -= 1;
            if (highEnemy.bleedStacks == 0)
            {
                highEnemy.bleed = false;
            }
            UpdateStatusEffects();
        }
        if (highEnemy.poison)
        {
            highEnemy.hpCurrent -= highEnemy.poisonAmount;
            boxText.text = highEnemy.EnemyName + " is poisoned for " + highEnemy.poisonAmount + " damage";
            yield return new WaitForSeconds(2f);
            highEnemy.poisonAmount -= 1;
            if (highEnemy.poisonAmount == 0)
            {
                highEnemy.poison = false;
            }
            UpdateStatusEffects();
        }
        for (int i = 0; i < group.enemyGroup.Count; i++)
        {
            if (group.enemyGroup[i].hpCurrent == 0)
            {
                EnemyDeath(i);
            }
        }
        //kill stuff and end battle stuff if no enemies left
        //lower braced buff stacks
        highEnemy.hasGone = true;
        if (highEnemy.stun)
        {
            StartCoroutine(EnemyStun());//probably also broken
        }
        else if (!gameOver)
        {
            if (highEnemy == enemy1)
            {
                hpHighlightT1.gameObject.SetActive(true);
            }
            else if (highEnemy == enemy2)
            {
                hpHighlightT2.gameObject.SetActive(true);
            }
            else if (highEnemy == enemy3)
            {
                hpHighlightT3.gameObject.SetActive(true);
            }
            else if (highEnemy == enemy4)
            {
                hpHighlightT4.gameObject.SetActive(true);
            }
            else
            {
                hpHighlightT5.gameObject.SetActive(true);
            }
            boxText.text = highEnemy.EnemyName + "'s turn.";
            yield return new WaitForSeconds(1f);
            highEnemy.BattleFunction();
            UpdateHP();
            boxText.text = highEnemy.boxText;
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < party.myParty.Count; i++)
            {
                if (party.myParty[i].hpCurrent == 0)
                {
                    PlayerDeath(i);
                }
            }
            hpHighlightT1.gameObject.SetActive(false);
            hpHighlightT2.gameObject.SetActive(false);
            hpHighlightT3.gameObject.SetActive(false);
            hpHighlightT4.gameObject.SetActive(false);
            hpHighlightT5.gameObject.SetActive(false);
            //highlights the enemy that is going
        }
        if (highEnemy.weak)
        {
            highEnemy.weakStacks -= 1;
            if (highEnemy.weakStacks == 0)
            {
                highEnemy.weak = false;
            }
            UpdateStatusEffects();
        }
        if (highEnemy.broken)
        {
            highEnemy.brokenStacks -= 1;
            if (highEnemy.brokenStacks == 0)
            {
                highEnemy.broken = false;
            }
            UpdateStatusEffects();
        }
        if (highEnemy.blind)
        {
            highEnemy.blindStacks -= 1;
            if (highEnemy.blindStacks == 0)
            {
                highEnemy.blind = false;
            }
            UpdateStatusEffects();
        }
        normalAttack.gameObject.SetActive(true);
        skills.gameObject.SetActive(true);
        items.gameObject.SetActive(true);
        run.gameObject.SetActive(true);
        if (!gameOver)
        {
            DetermineTurn();
        }
    }

    public void PlayerDeath(int i)
    {
        //Clear sprites
        //clear all buffs and debuffs
        if (party.myParty[i] = p1)
        {
            eHPBar1.gameObject.SetActive(false);
        }
        else if (party.myParty[i] = p2)
        {
            eHPBar2.gameObject.SetActive(false);
        }
        else if (party.myParty[i] = p3)
        {
            eHPBar3.gameObject.SetActive(false);
        }
        else if (party.myParty[i] = p4)
        {
            eHPBar4.gameObject.SetActive(false);
        }
        else if (party.myParty[i] = p5)
        {
            eHPBar5.gameObject.SetActive(false);
        }
        party.myParty[i].knockedOut = true;
        KnockedAmount += 1;
        if (KnockedAmount == party.myParty.Count)
        {
            gameOver = true;
            BattleLost();
        }

    }

    public void EnemyDeath(int i)
    {
        //clear sprite
        //clear all buffs and debuffs
        xpGiven += target.xpAmount;//needs to be fixed apparently
        monReward += Random.Range(group.enemyGroup[i].monMin, group.enemyGroup[i].monMax);
        target.dead = true;
        if (group.enemyGroup[i] == enemy1)
        {
            hpBar1.gameObject.SetActive(false);
            hpHighlight1.gameObject.SetActive(false);
            hpHighlightT1.gameObject.SetActive(false);
        }
        else if (group.enemyGroup[i] == enemy2)
        {
            hpBar2.gameObject.SetActive(false);
            hpHighlight2.gameObject.SetActive(false);
            hpHighlightT2.gameObject.SetActive(false);
        }
        else if (group.enemyGroup[i] == enemy3)
        {
            hpBar3.gameObject.SetActive(false);
            hpHighlight3.gameObject.SetActive(false);
            hpHighlightT3.gameObject.SetActive(false);
        }
        else if (group.enemyGroup[i] == enemy4)
        {
            hpBar4.gameObject.SetActive(false);
            hpHighlight4.gameObject.SetActive(false);
            hpHighlightT4.gameObject.SetActive(false);
        }
        else if (group.enemyGroup[i] == enemy5)
        {
            hpBar5.gameObject.SetActive(false);
            hpHighlight5.gameObject.SetActive(false);
            hpHighlightT5.gameObject.SetActive(false);
        }
        KilledAmount += 1;
        if (KilledAmount == group.enemyGroup.Count)
        {
            gameOver = true;
            BattleWon();
        }
    }

    public void UpdateHP()
    {
        if (enemy1)
        {
            eHP1.text = "HP " + enemy1.hpCurrent + "/" + enemy1.hpMax;
        }
        if (enemy2)
        {
            eHP2.text = "HP " + enemy2.hpCurrent + "/" + enemy2.hpMax;
        }
        if (enemy3)
        {
            eHP3.text = "HP " + enemy3.hpCurrent + "/" + enemy3.hpMax;
        }
        if (enemy4)
        {
            eHP4.text = "HP " + enemy4.hpCurrent + "/" + enemy4.hpMax;
        }
        if (enemy5)
        {
            eHP5.text = "HP " + enemy5.hpCurrent + "/" + enemy5.hpMax;
        }
        for (int i = 0; i < party.myParty.Count; i++)
        {
            if (party.myParty.Count == 1)
            {
                pHP1.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                pNRG1.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
            }
            else if (i == 0)
            {
                pHP2.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                pNRG2.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
            }
            else if (i == 1)
            {
                pHP3.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                pNRG3.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
            }
            else if (i == 2)
            {
                pHP4.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                pNRG4.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
            }
            else if (i == 3)
            {
                pHP5.text = "HP " + party.myParty[i].hpCurrent + "/" + party.myParty[i].hpMax;
                pNRG5.text = "Energy " + party.myParty[i].energyCurrent + "/" + party.myParty[i].energyMax;
            }
        }
    }

    private void PlayerTurn()
    {
        //highlights the character who's turn it is
        normalAttack.gameObject.SetActive(true);
        skills.gameObject.SetActive(true);
        items.gameObject.SetActive(true);
        if (!load.dugeonFight)
        {
            run.gameObject.SetActive(true);
        }
        if (highPlay.magCurrent < highPlay.magSize)
        {
            reload.gameObject.SetActive(true);
        }
        boxText.text = highPlay.memberName + "'s turn choose your action:";
        //skills will open a full selection menu
    }

    public void TargetSelect(int enemyNum)
    {
        hpHighlight1.gameObject.SetActive(false);
        hpHighlight2.gameObject.SetActive(false);
        hpHighlight3.gameObject.SetActive(false);
        hpHighlight4.gameObject.SetActive(false);
        hpHighlight5.gameObject.SetActive(false);
        if (enemyNum == 1)
        {
            hpHighlight1.gameObject.SetActive(true);
            target = enemy1;
        }
        else if (enemyNum == 2)
        {
            hpHighlight2.gameObject.SetActive(true);
            target = enemy2;
        }
        else if (enemyNum == 3)
        {
            hpHighlight3.gameObject.SetActive(true);
            target = enemy3;
        }
        else if (enemyNum == 4)
        {
            hpHighlight4.gameObject.SetActive(true);
            target = enemy4;
        }
        else
        {
            hpHighlight5.gameObject.SetActive(true);
            target = enemy5;
        }
    }

    private void LowerDebuffs()
    {
        if (highPlay.broken)
        {
            highPlay.brokenStacks -= 1;
            if (highPlay.brokenStacks == 0)
            {
                highPlay.broken = false;
            }
        }
        if (highPlay.blind)
        {
            highPlay.blindStacks -= 1;
            if (highPlay.blindStacks == 0)
            {
                highPlay.blind = false;
            }
        }
        if (highPlay.weak)
        {
            highPlay.weakStacks -= 1;
            if (highPlay.weakStacks == 0)
            {
                highPlay.weak = false;
            }
        }
        UpdateStatusEffects();
    }

    public void NormalAttack()
    {
        StartCoroutine(NormalAttackEnum());
    }

    IEnumerator NormalAttackEnum()
    {
        if (target)
        {
            if (highPlay.ammoPerShot > 0 && highPlay.magCurrent > 0)
            {
                highPlay.magCurrent -= highPlay.ammoPerShot;
                if (highPlay.magCurrent <= 0)
                {
                    highPlay.magCurrent = 0;
                }
                Damage.DamageTarget(highPlay.damMin, highPlay.damMax, target, null, null, highPlay, results);
                UpdateHP();
                if (results.damAmount > 0)
                {
                    boxText.text = target.EnemyName + " takes " + results.damAmount + " damage";
                }
                else
                {
                    boxText.text = target.EnemyName + " dodges the attack!";
                }
                normalAttack.gameObject.SetActive(false);
                skills.gameObject.SetActive(false);
                items.gameObject.SetActive(false);
                run.gameObject.SetActive(false);
                yield return new WaitForSeconds(2f);
                normalAttack.gameObject.SetActive(true);
                skills.gameObject.SetActive(true);
                items.gameObject.SetActive(true);
                run.gameObject.SetActive(true);
            }
            else if (highPlay.ammoPerShot > 0 && highPlay.magCurrent == 0)
            {
                boxText.text = "Magazine empty please reload to use normal attack.";
            }
            else
            {
                Damage.DamageTarget(highPlay.damMin, highPlay.damMax, target, null, null, highPlay, results);
                UpdateHP();
                if (results.damAmount > 0)
                {
                    boxText.text = target.EnemyName + " takes " + results.damAmount + " damage";
                }
                else
                {
                    boxText.text = target.EnemyName + " dodges the attack!";
                }
                normalAttack.gameObject.SetActive(false);
                skills.gameObject.SetActive(false);
                items.gameObject.SetActive(false);
                run.gameObject.SetActive(false);
                yield return new WaitForSeconds(2f);
                normalAttack.gameObject.SetActive(true);
                skills.gameObject.SetActive(true);
                items.gameObject.SetActive(true);
                run.gameObject.SetActive(true);
            }
            LowerDebuffs();
            for (int i = 0; i < group.enemyGroup.Count; i++)
            {
                if (group.enemyGroup[i].hpCurrent == 0)
                {
                    EnemyDeath(i);
                }
            }
            if (!gameOver)
            {
                DetermineTurn();
            }
        }
        else
        {
            boxText.text = "Please select a target.";
        }
    }

    public void Skills()
    {
        skillCanvas.gameObject.SetActive(true);
        genBox.gameObject.SetActive(false);
        if(enemy1)
        {
            e1Status.gameObject.SetActive(false);
        }
        if(enemy2)
        {
            e2Status.gameObject.SetActive(false);
        }
        if(enemy3)
        {
            e3Status.gameObject.SetActive(false);
        }
        if (p1)
        {
            p1Status.gameObject.SetActive(false);
            eHPBar1.gameObject.SetActive(false);
        }
        if (p2)
        {
            p2Status.gameObject.SetActive(false);
            eHPBar2.gameObject.SetActive(false);
        }
        if (p2)
        {
            p2Status.gameObject.SetActive(false);
            eHPBar3.gameObject.SetActive(false);
        }
    }

    public void Items()
    {
        itemCanvas.gameObject.SetActive(true);
        genBox.gameObject.SetActive(false);
        if (enemy1)
        {
            e1Status.gameObject.SetActive(false);
        }
        if (enemy2)
        {
            e2Status.gameObject.SetActive(false);
        }
        if (enemy3)
        {
            e3Status.gameObject.SetActive(false);
        }
        if(p1)
        {
            p1Status.gameObject.SetActive(false);
            eHPBar1.gameObject.SetActive(false);
        }
        if(p2)
        {
            p2Status.gameObject.SetActive(false);
            eHPBar2.gameObject.SetActive(false);
        }
        if(p2)
        {
            p2Status.gameObject.SetActive(false);
            eHPBar3.gameObject.SetActive(false);
        }
    }

    public void CloseMenu()
    {
        if (enemy1)
        {
            e1Status.gameObject.SetActive(true);
        }
        if (enemy2)
        {
            e2Status.gameObject.SetActive(true);
        }
        if (enemy3)
        {
            e3Status.gameObject.SetActive(true);
        }
        if (p1)
        {
            p1Status.gameObject.SetActive(true);
            eHPBar1.gameObject.SetActive(true);
        }
        if (p2)
        {
            p2Status.gameObject.SetActive(true);
            eHPBar2.gameObject.SetActive(true);
        }
        if (p2)
        {
            p2Status.gameObject.SetActive(true);
            eHPBar3.gameObject.SetActive(true);
        }
        itemCanvas.gameObject.SetActive(false);
        skillCanvas.gameObject.SetActive(false);
        genBox.gameObject.SetActive(true);
        //makes player buttons visible again and closes the open menu
    }

    public void RunAway()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        int runSpeed = Random.Range(1, 20);
        runSpeed += highPlay.speed;
        if (runSpeed > 10)
        {
            genBox.gameObject.SetActive(false);
            RunSuccess();
        }
        else
        {

            boxText.text = "Unable to run away";
            normalAttack.gameObject.SetActive(false);
            skills.gameObject.SetActive(false);
            items.gameObject.SetActive(false);
            run.gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            normalAttack.gameObject.SetActive(true);
            skills.gameObject.SetActive(true);
            items.gameObject.SetActive(true);
            run.gameObject.SetActive(true);
            LowerDebuffs();
            DetermineTurn();
        }
    }

    public void Reload()
    {
        StartCoroutine(ReloadEnum());
    }

    IEnumerator ReloadEnum()
    {
        highPlay.magCurrent = highPlay.magSize;
        if (highPlay.freeReload)
        {
            boxText.text = "Magazine reloaded";
            highPlay.freeReloadTurns -= 1;
            if (highPlay.freeReloadTurns <= 0)
            {
                highPlay.freeReload = false;
            }
            reload.gameObject.SetActive(false);
        }
        else
        {
            boxText.text = "Magazine reloaded";
            yield return new WaitForSeconds(2f);
            LowerDebuffs();
            DetermineTurn();
        }
    }

    private void BattleWon()
    {
        for (int i = 0; i < party.myParty.Count; i++)
        {
            party.myParty[i].exp += xpGiven;
            if (party.myParty[i].exp >= party.myParty[i].expNext)
            {
                LevelUp.LevelUpTarget(party.myParty[i]);
            }
        }
        party.myParty[0].credits += monReward;
        //display xp given and wait for player okay
        genBox.gameObject.SetActive(false);
        endTextBox.gameObject.SetActive(true);
        xpText.text = "Victory!\nParty earns " + xpGiven + " xp.\nAnd " + monReward + " credits";
    }

    private void BattleLost()
    {
        genBox.SetActive(false);
        endTextBox.SetActive(true);
        xpText.text = "You all died!\n Reload save?";
        //active yes vs no button yes opens up load menu
    }

    private void RunSuccess()
    {
        gameOver = true;
        genBox.gameObject.SetActive(false);
        eHPBar1.gameObject.SetActive(false);
        eHPBar2.gameObject.SetActive(false);
        eHPBar3.gameObject.SetActive(false);
        playerPos1.gameObject.SetActive(false);
        playerPos2.gameObject.SetActive(false);
        playerPos3.gameObject.SetActive(false);
        for (int i = 0; i < party.myParty.Count; i++)
        {
            party.myParty[i].exp += xpGiven;
            if (party.myParty[i].exp >= party.myParty[i].expNext)
            {
                LevelUp.LevelUpTarget(party.myParty[i]);
            }
        }
        endTextBox.gameObject.SetActive(true);
        xpText.text = "You successfully run away.\nParty earns " + xpGiven + " xp.";
    }

    public void ConfirmEnd()
    {
        endTextBox.gameObject.SetActive(false);
        genBox.gameObject.SetActive(true);
        playerPos1.gameObject.SetActive(true);
        playerPos2.gameObject.SetActive(true);
        playerPos3.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(load.lastScene);
    }
}
