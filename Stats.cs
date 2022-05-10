using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Stats/PlayerStats")]
public class Stats : ScriptableObject
{
    public string memberName;
    public Sprite battleSprite;
    public int level;
    public int exp;
    public int expNext;
    public int statPoints;
    public float hpMax;
    public int hpCurrent;
    public float energyMax;
    public int energyRegen;
    public int energyCurrent;
    public int power;
    public int tenacity;
    public int intellect;
    public int dexterity;
    public int affinity;
    public float dodge;
    public float effectRes;
    public int defenceValue;
    public int speed;
    public float currentWeight;
    public float maxWeight;
    public int ammoPerShot;
    public int magSize;
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
}
