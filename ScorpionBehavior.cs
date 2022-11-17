using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionBehavior : MonoBehaviour
{
    public static void ScorpionAttack(EnemyStats scorpion)
    {
        int action = Random.Range(1, 101);
        if (action < 40)
        {
            int target = Random.Range(0, scorpion.party.myParty.Count);
            bool targetFound = false;
            int oldTarget = target;
            Damage.DamageTarget(scorpion.damMin, scorpion.damMax, null, scorpion.party.myParty[target], scorpion, null, scorpion.results);
            if (scorpion.results.damAmount > 0)
            {
                scorpion.boxText = scorpion.party.myParty[target].memberName + " is crushed for " + scorpion.results.damAmount + " damage";
            }
            else
            {
                scorpion.boxText = scorpion.party.myParty[target].memberName + " dodges the crush";
            }
            if (scorpion.party.myParty.Count > 1)
            {
                while (targetFound == false)
                {
                    target = Random.Range(0, scorpion.party.myParty.Count - 1);
                    if (target != oldTarget)
                    {
                        targetFound = true;
                    }
                }
                Damage.DamageTarget(scorpion.damMin, scorpion.damMax, null, scorpion.party.myParty[target], scorpion, null, scorpion.results);
                if (scorpion.results.damAmount > 0)
                {
                    scorpion.boxText += scorpion.party.myParty[target].memberName + " is crushed for " + scorpion.results.damAmount + " damage";
                }
                else
                {
                    scorpion.boxText += scorpion.party.myParty[target].memberName + " dodges the crush";
                }
            }
            //crush
        }
        else if (action < 80)
        {
            int target = Random.Range(0, scorpion.party.myParty.Count);
            Damage.DamageTarget(Mathf.RoundToInt( scorpion.damMin *0.75f), Mathf.RoundToInt(scorpion.damMax*0.75f), null, scorpion.party.myParty[target], scorpion, null, scorpion.results);
            if (scorpion.results.damAmount > 0)
            {
                scorpion.boxText = scorpion.party.myParty[target].memberName + " is stung for " + scorpion.results.damAmount + " damage";
                float res = Random.Range(1, 101);
                if (res > scorpion.party.myParty[target].effectRes)
                {
                    scorpion.party.myParty[target].poison = true;
                    scorpion.party.myParty[target].poisonAmount += scorpion.results.damAmount;
                    scorpion.boxText += scorpion.party.myParty[target].memberName + " is poisoned for " + scorpion.results.damAmount;
                }
                //poison stuff
            }
            else
            {
                scorpion.boxText = scorpion.party.myParty[target].memberName + " dodges the sting";
            }
            //sting
        }
        else
        {
            scorpion.bracedStacks += 2;
            if (scorpion.braced == false)
            {
                scorpion.braced = true;
            }
            scorpion.boxText = scorpion.EnemyName + " burrows in the ground and gains two stacks of braced";
            //burrow
        }
    }
}
