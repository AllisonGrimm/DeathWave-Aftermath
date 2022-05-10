using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBehavior : MonoBehaviour
{
    public static void WolfAttack(EnemyStats wolf)
    {
        int action = Random.Range(1, 100);

        if(action>80)
        {
            for(int i = 0; i<wolf.group.enemyGroup.Count;i++)
            {
                //applies the buff to all allies
                //animation
            }
            //howl
        }
        else
        {
            int target = Random.Range(0, wolf.party.myParty.Count);
            Damage.DamageTarget(wolf.damMin, wolf.damMax, null, wolf.party.myParty[target], wolf, null);
            //needs to play animation and give feedback on damage and such
            //bite
        }
    }

}
