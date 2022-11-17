using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBehavior : MonoBehaviour
{
    public static void WolfAttack(EnemyStats wolf)
    {
        int action = Random.Range(1, 101);
        if(action>85)
        {
            wolf.boxText = wolf.EnemyName + " howls to strengthens their allies";
            for(int i = 0; i<wolf.group.enemyGroup.Count;i++)
            {
                wolf.group.enemyGroup[i].strengthened = true;
                wolf.group.enemyGroup[i].strStacks += 2;
                //applies the buff to all allies
                //animation
            }
            //howl .mp3
        }
        else
        {
            int target = Random.Range(0, wolf.party.myParty.Count);
            wolf.boxText = wolf.EnemyName + " tries to bite " + wolf.party.myParty[target].memberName;
            Damage.DamageTarget(wolf.damMin, wolf.damMax, null, wolf.party.myParty[target], wolf, null,wolf.results);
            if (wolf.results.damAmount > 0)
            {
                wolf.boxText += "\n" + wolf.party.myParty[target].memberName + " is bitten for " + wolf.results.damAmount + " damage";
            }
            else
            {
                wolf.boxText = "\n" + wolf.party.myParty[target].memberName + " dodges the bite";
            }
            //needs to play animation
            //bite
        }
    }
}
