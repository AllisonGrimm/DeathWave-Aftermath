using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    [TextArea(5, 10)]
    public string itemDescription;
    public string boxText;
    public Sprite itemImage;
    public GameObject anim;
    public int numHeld;
    public int setNum;
    public string weaponType;

    public bool isEquiped;
    public bool isConsumable;
    public bool isEquipable;
    public bool isFightExclusive;

    public int itemType;
    public float itemWeight;
    public int cost;

    public int lvlReq;
    public int powReq;
    public int intReq;
    public int dexReq;
    public int affReq;

    public int dmgMin;
    public int dmgMax;
    public int magSize;

    public int defenceValue;
    public int healthUp;
    public int energyUp;
    public int dodgeUp;
    public int speedUp;
    public int resUp;

    public int defenceValueSet;
    public int healthUpSet;
    public int energyUpSet;
    public int dodgeUpSet;
    public int speedUpSet;
    public int resUpSet;

    public float restorePercent;
    public Stats target;
    public EnemyGroup group;

    public BattleResults results;
    public PlayerInventory storage;
    public UnityEvent thisEvent;

    public void Use(Stats newTarget)
    {
        target = newTarget;
        thisEvent.Invoke();
    }

    public void DecreaseAmount(int amountDecrease)
    {
        numHeld -= amountDecrease;
        if(numHeld<0)
        {
            numHeld = 0;
        }

    }
    //need to somehow implement requirements, skill don't know if that will be done here
}
