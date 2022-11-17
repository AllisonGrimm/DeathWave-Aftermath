using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "Stats/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [Header("Stats")]
    public string EnemyName;
    public Sprite enemySprite;
    public float hpMax;
    public int hpCurrent;
    public int dodge;
    public int effectRes;
    public float defenceValue;
    public int accuracy;
    public int speed;
    public int damMin;
    public int damMax;

    public int xpAmount;
    public int monMin;
    public int monMax;

    //something about stealable thing
    //Something about dropable items

    [Header("Status Effects")]
    public bool broken;
    public int brokenStacks;
    public bool bleed;
    public int bleedAmount;
    public int bleedStacks;
    public bool poison;
    public int poisonAmount;
    public bool blind;
    public int blindStacks;
    public bool weak;
    public int weakStacks;
    public bool stun;
    public int stunStacks;
    public bool adrenalin;
    public int adrenStacks;
    public bool impervious;
    public int impervStacks;
    public bool regen;
    public int regenAmount;
    public int regenStacks;
    public bool strengthened;
    public int strStacks;
    public bool braced;
    public int bracedStacks;

    [Header("Battle")]
    public int currentSpeed;
    public bool hasGone;
    public bool dead;
    public BattleResults results;
    public string boxText;
    public VectorValue position;

    public CurrentParty party;
    public EnemyGroup group;
    public UnityEvent thisEvent; //will need to pass the current enemy stats into the event
   
    public void BattleFunction()//Calls the battle ai
    {
        thisEvent.Invoke();
    }
}
