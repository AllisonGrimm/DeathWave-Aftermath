using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public static void DamageTarget(int damMin, int damMax, EnemyStats enemyTarget, Stats playerTarget, EnemyStats enemyAttacker, Stats playerAttacker)
    {
        int damAmount = Random.Range(damMin, damMax);
        //implement damage buff 

        //might have to have different statements for different damage types due to conditional dv from armor
        if(enemyTarget)
        {
            //implement dodge stuff also where the dodge buff will be calculated
            //broken implementation
            enemyTarget.hpCurrent -= Mathf.RoundToInt(damAmount - (damAmount * enemyTarget.defenceValue));
        }
        else if(playerTarget)
        {
            //implement dodge stuff also where the dodge buff will be calculated
            //broken implementation
            playerTarget.hpCurrent -= Mathf.RoundToInt(damAmount - (damAmount * playerTarget.defenceValue));
        }
    }
}
