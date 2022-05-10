using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRecovery : MonoBehaviour
{
   
    static public void Heal(InventoryItem consumable)
    {
        int healAmount;
        float healPercent = consumable.restorePercent;
        Stats target = consumable.target;

        healAmount = Mathf.RoundToInt( target.hpMax * healPercent);

        target.hpCurrent += healAmount;

        if(target.hpCurrent > target.hpMax)
        {
            target.hpCurrent = Mathf.RoundToInt(target.hpMax);
        }
    }
}
