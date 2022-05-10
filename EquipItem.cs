using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    static public void Equip(InventoryItem newEquipment)
    {
        PlayerInventory inventory = newEquipment.storage;
        Stats person;
        for (int i = 0; i<inventory.myInven.Count; i ++)
        {
            if(inventory.myInven[i].isEquiped && inventory.myInven[i].itemType == newEquipment.itemType && inventory.myInven[i].target == newEquipment.target)
            {
                person = inventory.myInven[i].target;
                person.currentWeight -= inventory.myInven[i].itemWeight;
                person.damMin -= inventory.myInven[i].dmgMin;
                person.damMax -= inventory.myInven[i].dmgMax;
                person.defenceValue -= inventory.myInven[i].defenceValue;
                person.speed -= inventory.myInven[i].speedUp;
                person.dodge -= inventory.myInven[i].dodgeUp;
                person.effectRes -= inventory.myInven[i].resUp;
                person.energyMax -= inventory.myInven[i].energyUp;
                person.hpMax -= inventory.myInven[i].healthUp;
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
        person = newEquipment.target;
        person.currentWeight += newEquipment.itemWeight;
        person.damMin += newEquipment.dmgMin;
        person.damMax += newEquipment.dmgMax;
        person.defenceValue += newEquipment.defenceValue;
        person.speed += newEquipment.speedUp;
        person.dodge += newEquipment.dodgeUp;
        person.effectRes += newEquipment.resUp;
        person.energyMax += newEquipment.energyUp;
        person.hpMax += newEquipment.healthUp;
        newEquipment.isEquiped = true;
    }
}
