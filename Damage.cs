using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour
{
    public static void DamageTarget(int damMin, int damMax, EnemyStats enemyTarget, Stats playerTarget, EnemyStats enemyAttacker, Stats playerAttacker, BattleResults results)
    {
        int damAmount;
        bool wasBlind = false;
        bool wasAdren = false;

        //prolly need to redo dodge with floats

        if(enemyTarget)
        {
            if(playerAttacker.strengthened&&playerAttacker.weak)
            {
                damAmount = Random.Range(damMin, damMax);
                playerAttacker.strStacks -= 1;
                if (playerAttacker.strStacks == 0)
                {
                    playerAttacker.strengthened = false;
                }
            }
            else if (playerAttacker.strengthened)
            {
                damAmount = Random.Range(Mathf.RoundToInt(damMin * 1.25f), Mathf.RoundToInt(damMax * 1.25f)+1);
                playerAttacker.strStacks -= 1;
                if (playerAttacker.strStacks == 0)
                {
                    playerAttacker.strengthened = false;
                }
            }
            else if(playerAttacker.weak)
            {
                damAmount = Random.Range(Mathf.RoundToInt(damMin * .75f), Mathf.RoundToInt(damMax * .75f)+1);
            }
            else
            {
                damAmount = Random.Range(damMin, damMax+1);
            }
            if(playerAttacker.blind)
            {
                playerAttacker.accuracy -= 25;
                wasBlind = true;
            }
            float hitChance = Random.Range(0, playerAttacker.accuracy+1);
            if(enemyTarget.adrenalin)
            {
                enemyTarget.dodge += 25;
                enemyTarget.adrenStacks -= 1;
                wasAdren = true;
                if(enemyTarget.adrenStacks == 0)
                {
                    enemyTarget.adrenalin = false;
                }
            }
            if(hitChance>enemyTarget.dodge&&!enemyTarget.braced&&!enemyTarget.broken)
            {
                enemyTarget.hpCurrent -= Mathf.RoundToInt(damAmount - (damAmount * (.1f *enemyTarget.defenceValue)));
                if (enemyTarget.hpCurrent < 0)
                {
                    enemyTarget.hpCurrent = 0;
                }
                results.damAmount =  Mathf.RoundToInt(damAmount - (damAmount * (.1f * enemyTarget.defenceValue)));
            }
            else if(hitChance>enemyTarget.dodge&&enemyTarget.braced&&!enemyTarget.broken)
            {
                enemyTarget.bracedStacks -= 1;
                if (enemyTarget.bracedStacks == 0)
                {
                    enemyTarget.braced = false;
                }
                enemyTarget.hpCurrent -= Mathf.RoundToInt(damAmount - (damAmount * (.1f * (enemyTarget.defenceValue * 1.5f))));
                if (enemyTarget.hpCurrent < 0)
                {
                    enemyTarget.hpCurrent = 0;
                }
                results.damAmount = Mathf.RoundToInt(damAmount - (damAmount * (.1f * (enemyTarget.defenceValue * 1.5f))));
            }
            else if(hitChance > enemyTarget.dodge && enemyTarget.braced && enemyTarget.broken)
            {
                enemyTarget.bracedStacks -= 1;
                if (enemyTarget.bracedStacks == 0)
                {
                    enemyTarget.braced = false;
                }
                enemyTarget.hpCurrent -= Mathf.RoundToInt(damAmount - (damAmount * (.1f * enemyTarget.defenceValue)));
                if (enemyTarget.hpCurrent < 0)
                {
                    enemyTarget.hpCurrent = 0;
                }
                results.damAmount = Mathf.RoundToInt(damAmount - (damAmount * (.1f * enemyTarget.defenceValue)));
            }
            else if(hitChance > enemyTarget.dodge && !enemyTarget.braced && enemyTarget.broken)
            {
                enemyTarget.hpCurrent -= Mathf.RoundToInt(damAmount - (damAmount * (.1f * (enemyTarget.defenceValue * .5f))));
                if (enemyTarget.hpCurrent < 0)
                {
                    enemyTarget.hpCurrent = 0;
                }
                results.damAmount = Mathf.RoundToInt(damAmount - (damAmount * (.1f * (enemyTarget.defenceValue * .5f))));
            }
            else
            {
                results.damAmount = 0;
            }    
            if(wasBlind)
            {
                playerAttacker.accuracy += 25;
            }
            if(wasAdren)
            {
                enemyTarget.dodge -= 25;
            }
        }
        else if(playerTarget)
        {
            if(enemyAttacker.strengthened&&enemyAttacker.weak)
            {
                damAmount = Random.Range(damMin, damMax+1);
                enemyAttacker.strStacks -= 1;
                if(enemyAttacker.strStacks == 0)
                {
                    enemyAttacker.strengthened = false;
                }
            }
            else if(enemyAttacker.strengthened)
            {
                damAmount = Random.Range(Mathf.RoundToInt(damMin*1.25f), Mathf.RoundToInt(damMax*1.25f)+1);
                enemyAttacker.strStacks -= 1;
                if (enemyAttacker.strStacks == 0)
                {
                    enemyAttacker.strengthened = false;
                }
            }
            else if(enemyAttacker.weak)
            {
                damAmount = Random.Range(Mathf.RoundToInt(damMin * .75f), Mathf.RoundToInt(damMax * .75f)+1);
            }
            else
            {
                damAmount = Random.Range(damMin, damMax+1);
            }
            if(enemyAttacker.blind)
            {
                enemyAttacker.accuracy -= 25;
                wasBlind = true;
            }
            if(playerTarget.adrenalin)
            {
                playerTarget.dodge += 25;
                playerTarget.adrenStacks -= 1;
                wasAdren = true;
                if(playerTarget.adrenStacks == 0)
                {
                    playerTarget.adrenalin = false;
                }
            }
            float hitChance = Random.Range(0, enemyAttacker.accuracy+1);
            if (hitChance > playerTarget.dodge&&!playerTarget.braced&&!playerTarget.broken)
            {
                playerTarget.hpCurrent -= Mathf.RoundToInt(damAmount - (damAmount * (.1f*playerTarget.defenceValue)));
                if(playerTarget.hpCurrent<0)
                {
                    playerTarget.hpCurrent = 0;
                }
                results.damAmount = Mathf.RoundToInt(damAmount - (damAmount * (.1f * playerTarget.defenceValue)));
            }
            else if(hitChance>playerTarget.dodge&&playerTarget.braced&&!playerTarget.broken)
            {
                playerTarget.bracedStacks -= 1;
                if(playerTarget.bracedStacks == 0)
                {
                    playerTarget.braced = false;
                }
                playerTarget.hpCurrent -= Mathf.RoundToInt(damAmount - (damAmount * (.1f * (playerTarget.defenceValue*1.5f))));
                if (playerTarget.hpCurrent < 0)
                {
                    playerTarget.hpCurrent = 0;
                }
                results.damAmount = Mathf.RoundToInt(damAmount - (damAmount * (.1f * (playerTarget.defenceValue * 1.5f))));
            }
            else if(hitChance > playerTarget.dodge && !playerTarget.braced && playerTarget.broken)
            {
                playerTarget.hpCurrent -= Mathf.RoundToInt(damAmount - (damAmount * (.1f * (playerTarget.defenceValue * .5f))));
                if (playerTarget.hpCurrent < 0)
                {
                    playerTarget.hpCurrent = 0;
                }
                results.damAmount = Mathf.RoundToInt(damAmount - (damAmount * (.1f * (playerTarget.defenceValue * .5f))));
            }
            else if(hitChance > playerTarget.dodge && playerTarget.braced && playerTarget.broken)
            {
                playerTarget.bracedStacks -= 1;
                if (playerTarget.bracedStacks == 0)
                {
                    playerTarget.braced = false;
                }
                playerTarget.hpCurrent -= Mathf.RoundToInt(damAmount - (damAmount * (.1f * playerTarget.defenceValue)));
                if (playerTarget.hpCurrent < 0)
                {
                    playerTarget.hpCurrent = 0;
                }
                results.damAmount = Mathf.RoundToInt(damAmount - (damAmount * (.1f * playerTarget.defenceValue)));
            }
            else
            {
                Debug.Log("dodged player");
                results.damAmount = 0;
            }
            if(wasBlind)
            {
                enemyAttacker.accuracy += 25;
            }
            if(wasAdren)
            {
                playerTarget.dodge -= 25;
            }
        }
        //needs damage feedback nums
    }
}
