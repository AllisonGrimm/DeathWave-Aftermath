using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "Stats/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    //normal stats
    public string EnemyName;
    public Sprite enemySprite;
    public float hpMax;
    public int hpCurrent;
    public float dodge;
    public float effectRes;
    public float defenceValue;
    public float accuracy;
    public int speed;
    public int damMin;
    public int damMax;

    //status effects
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

    //turn order stats
    public int currentSpeed;
    public bool hasGone;

    //battle stats
    public CurrentParty party;
    public EnemyGroup group;
    public UnityEvent thisEvent; //will need to pass the current enemy stats into the event
    //some sort of way to determine how the enemy acts in combat maybe function
   
    public void BattleFunction()//Calls the battle ai
    {
        thisEvent.Invoke();
    }
}
