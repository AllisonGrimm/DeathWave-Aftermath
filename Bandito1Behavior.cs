using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandito1Behavior : MonoBehaviour
{
    public static void BanditoAttack(EnemyStats bandito)
    {
        int action = Random.Range(0, 101);
        if(action<30)
        {
            int target = Random.Range(0, bandito.party.myParty.Count);
            bandito.boxText = bandito.EnemyName + " blasts " + bandito.party.myParty[target].memberName;
            Damage.DamageTarget(bandito.damMin*2, bandito.damMax*2, null, bandito.party.myParty[target], bandito, null, bandito.results);
            if (bandito.results.damAmount > 0)
            {
                bandito.boxText += "\n" + bandito.party.myParty[target].memberName + " is blasted for " + bandito.results.damAmount + " damage";
            }
            else
            {
                bandito.boxText = bandito.party.myParty[target].memberName + " dodges the blast";
            }
            bandito.stun = true;
            bandito.stunStacks = 1;
        }
        if(action<50)
        {
            bandito.boxText = bandito.EnemyName + " throws a grenade";
            for (int i = 0; i < bandito.party.myParty.Count; i++)
            {
                Damage.DamageTarget(Mathf.RoundToInt(bandito.damMin*.5f), Mathf.RoundToInt(bandito.damMax*.5f), null, bandito.party.myParty[i], bandito, null, bandito.results);
                if (bandito.results.damAmount > 0)
                {
                    bandito.boxText += "\n" + bandito.party.myParty[i].memberName + " is exploded for " + bandito.results.damAmount + "damage";
                }
                else
                {
                    bandito.boxText += "\n" + bandito.party.myParty[i].memberName + " dodges the explosion";
                }
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
