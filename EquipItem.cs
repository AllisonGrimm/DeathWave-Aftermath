using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    static public void Equip(InventoryItem newEquipment)
    {
        PlayerInventory inventory = newEquipment.storage;
        Stats person;
        for (int i = 0; i<inventory.myInven.Count; i ++)//need to make sure the equipment wouldn't exceed the weight limit
        {
            if(inventory.myInven[i].isEquiped && inventory.myInven[i].itemType == newEquipment.itemType && inventory.myInven[i].target == newEquipment.target)
            {
                person = inventory.myInven[i].target;
                person.currentWeight -= inventory.myInven[i].itemWeight;
                person.damMin = 0;
                person.damMax = 0;
                person.defenceValue -= inventory.myInven[i].defenceValue;
                person.speed -= inventory.myInven[i].speedUp;
                person.dodge -= inventory.myInven[i].dodgeUp;
                person.effectRes -= inventory.myInven[i].resUp;
                person.energyMax -= inventory.myInven[i].energyUp;
                person.hpMax -= inventory.myInven[i].healthUp;
                if(person.setBonus)
                {
                    person.setBonus = false;
                    person.hpMax -= inventory.myInven[i].healthUpSet;
                    person.defenceValue -= inventory.myInven[i].defenceValueSet;
                    person.speed -= inventory.myInven[i].speedUpSet;
                    person.dodge -= inventory.myInven[i].dodgeUpSet;
                    person.effectRes -= inventory.myInven[i].resUpSet;
                    person.energyMax -= inventory.myInven[i].energyUpSet;
                }
                if(person.hpCurrent>person.hpMax)
                {
                    person.hpCurrent = Mathf.RoundToInt(person.hpMax);
                }
                if(person.energyCurrent>person.energyMax)
                {
                    person.energyCurrent = Mathf.RoundToInt(person.energyMax);
                }
                inventory.myInven[i].isEquiped = false;
            }
        }

        //check for weapon tye and apply damage increase based on stats
        person = newEquipment.target;
        person.setNum = newEquipment.setNum;
        for(int i = 0; i<inventory.myInven.Count;i++)
        {
            if(inventory.myInven[i].isEquiped && inventory.myInven[i].setNum != 0)
            {
                if(person.setNum == inventory.myInven[i].setNum)
                {
                    person.defenceValue += newEquipment.defenceValueSet;
                    person.speed += newEquipment.speedUpSet;
                    person.dodge += newEquipment.dodgeUpSet;
                    person.effectRes += newEquipment.resUpSet;
                    person.energyMax += newEquipment.energyUpSet;
                    person.hpMax += newEquipment.healthUpSet;
                    person.setBonus = true;
                }
            }
        }
        person.currentWeight += newEquipment.itemWeight;
        if(newEquipment.weaponType == "Gun")
        {
            person.damMin += Mathf.RoundToInt(newEquipment.dmgMin*person.damGun);
            person.damMax += Mathf.RoundToInt(newEquipment.dmgMax*person.damGun);
        }
        else if(newEquipment.weaponType == "Melee")
        {
            person.damMin += Mathf.RoundToInt(newEquipment.dmgMin*person.damMelee);
            person.damMax += Mathf.RoundToInt(newEquipment.dmgMax*person.damMelee);
        }
        else if(newEquipment.weaponType == "Mech")
        {
            person.damMin += Mathf.RoundToInt(newEquipment.dmgMin*person.damMech);
            person.damMax += Mathf.RoundToInt(newEquipment.dmgMax*person.damMech);
        }   
        else if(newEquipment.weaponType == "Tech")
        {
            person.damMin += Mathf.RoundToInt(newEquipment.dmgMin*person.damTech);
            person.damMax += Mathf.RoundToInt(newEquipment.dmgMax*person.damTech);
        }
        person.defenceValue += newEquipment.defenceValue;
        person.speed += newEquipment.speedUp;
        person.dodge += newEquipment.dodgeUp;
        person.effectRes += newEquipment.resUp;
        person.energyMax += newEquipment.energyUp;
        person.hpMax += newEquipment.healthUp;
        newEquipment.isEquiped = true;
    }
}
