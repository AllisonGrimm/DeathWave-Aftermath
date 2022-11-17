using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandito2Behavior : MonoBehaviour
{
    public static void BanditoAttack(EnemyStats bandito)
    {
        int action = Random.Range(0, 100);
        if (action < 30)
        {
            int target = Random.Range(0, bandito.party.myParty.Count);
            bandito.boxText = bandito.EnemyName + " burst fires at " + bandito.party.myParty[target].memberName;
            bandito.accuracy -= 25;
            Damage.DamageTarget(bandito.damMin, bandito.damMax, null, bandito.party.myParty[target], bandito, null, bandito.results);
            if (bandito.results.damAmount > 0)
            {
                bandito.boxText += "\n" + bandito.party.myParty[target].memberName + " is shot for " + bandito.results.damAmount + " damage";
            }
            else
            {
                bandito.boxText = bandito.party.myParty[target].memberName + " dodges the shot";
            }
            Damage.DamageTarget(bandito.damMin, bandito.damMax, null, bandito.party.myParty[target], bandito, null, bandito.results);
            if (bandito.results.damAmount > 0)
            {
                bandito.boxText += "\n" + bandito.party.myParty[target].memberName + " is shot for " + bandito.results.damAmount + " damage";
            }
            else
            {
                bandito.boxText = bandito.party.myParty[target].memberName + " dodges the shot";
            }
            bandito.accuracy += 25;
        }
        if (action < 50&&bandito.hpCurrent<bandito.hpMax)
        {
            int heal = Random.Range(bandito.damMin, bandito.damMax+1);
            bandito.boxText = bandito.EnemyName + " heals themselves for " + heal + " hp";
            bandito.hpCurrent += heal;
            if(bandito.hpCurrent>bandito.hpMax)
            {
                bandito.hpCurrent = Mathf.RoundToInt(bandito.hpMax);
            }
        }
        else
        {
            int target = Random.Range(0, bandito.party.myParty.Count);
            bandito.boxText = bandito.EnemyName + " shoots at " + bandito.party.myParty[target].memberName;
            Damage.DamageTarget(bandito.damMin, bandito.damMax, null, bandito.party.myParty[target], bandito, null, bandito.results);
            if (bandito.results.damAmount > 0)
            {
                bandito.boxText += "\n" + bandito.party.myParty[target].memberName + " is shot for " + bandito.results.damAmount + " damage";
            }
            else
            {
                bandito.boxText = bandito.party.myParty[target].memberName + " dodges the shot";
            }
        }
    }
}
