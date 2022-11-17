using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBehavior : MonoBehaviour
{
    public static void BearAttack(EnemyStats bear)
    {
        int action = Random.Range(1, 101);
        if (action > 85)
        {
            bear.boxText = bear.EnemyName + " roars";
            for (int i = 0; i < bear.party.myParty.Count; i++)
            {
                float res = Random.Range(1, 101);
                if (res > bear.party.myParty[i].effectRes)
                {
                    bear.party.myParty[i].weak = true;
                    bear.party.myParty[i].weakStacks += 2;
                }
                else
                {
                    bear.boxText = "\n" + bear.party.myParty[i].memberName + " resists the weak";
                }
            }
            //roar .mp3
        }
        else
        {
            bear.boxText = bear.EnemyName + " slashes at the party";
            for (int i = 0; i < bear.party.myParty.Count; i++)
            {
                Damage.DamageTarget(bear.damMin, bear.damMax, null, bear.party.myParty[i], bear, null, bear.results);
                if (bear.results.damAmount > 0)
                {
                    bear.boxText += "\n" + bear.party.myParty[i].memberName + " is slashed for " + bear.results.damAmount + "damage";
                }
                else
                {
                    bear.boxText += "\n" + bear.party.myParty[i].memberName + " dodges the slash";
                }
            }
        }
    }
}
