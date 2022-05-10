using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRecovery : MonoBehaviour
{
    static public void EnergyRestore(InventoryItem consumable)//pass in the consumable being used
    {
        int restoreAmount;
        float restorPercent = consumable.restorePercent;//Get the amount and who is going restored to
        Stats target = consumable.target;

        restoreAmount = Mathf.RoundToInt(target.hpMax * restorPercent);

        target.hpCurrent += restoreAmount;

        if (target.energyCurrent > target.energyMax)//If it restores more than max set to max
        {
            target.energyCurrent = Mathf.RoundToInt(target.energyMax);
        }
    }
}
