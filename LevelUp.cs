using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
   public static void LevelUpTarget(Stats target)//check for level up whenever xp gained
    {
        target.statPoints += 2;
        target.level += 1;        
        target.hpMax += 5;
        target.energyMax += 2;
        target.hpCurrent = Mathf.RoundToInt(target.hpMax);
        target.energyCurrent = Mathf.RoundToInt(target.energyMax);

        XpLevels.UpdateXp(target);
        //might need to play a sound and display target leveled up or something
    }
}
