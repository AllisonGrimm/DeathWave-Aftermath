using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBossBehavior : MonoBehaviour
{
    public static void WolfBossAttack(EnemyStats wolfBoss)
    {
        int action = Random.Range(1, 101);
        if (action > 85)
        {
            wolfBoss.boxText = wolfBoss.EnemyName + " howls to strengthen their allies and weaken their enemies";
            for (int i = 0; i < wolfBoss.group.enemyGroup.Count; i++)
            {
                wolfBoss.group.enemyGroup[i].strengthened = true;
                wolfBoss.group.enemyGroup[i].strStacks += 2;
                //applies the buff to all allies
                //animation
            }
            for (int i = 0; i < wolfBoss.party.myParty.Count; i++)
            {
                float res = Random.Range(1, 101);
                if (res > wolfBoss.party.myParty[i].effectRes)
                {
                    wolfBoss.party.myParty[i].weak = true;
                    wolfBoss.party.myParty[i].weakStacks += 2;
                }
            }
            //bighowl .mp3
        }
        else
        {
            int target = Random.Range(0, wolfBoss.party.myParty.Count);
            Damage.DamageTarget(wolfBoss.damMin, wolfBoss.damMax, null, wolfBoss.party.myParty[target], wolfBoss, null, wolfBoss.results);
            if (wolfBoss.results.damAmount > 0)
            {
                wolfBoss.boxText = wolfBoss.party.myParty[target].memberName + " is bitten for " + wolfBoss.results.damAmount + " damage";
                float res = Random.Range(1, 101);
                if (res > wolfBoss.party.myParty[target].effectRes)
                {
                    wolfBoss.party.myParty[target].bleed = true;
                    wolfBoss.party.myParty[target].bleedStacks = 2;
                    wolfBoss.party.myParty[target].bleedAmount = wolfBoss.results.damAmount;
                    wolfBoss.boxText += "\n" + wolfBoss.party.myParty[target].memberName + " bleeds for two turns for " + wolfBoss.results.damAmount + " damage";
                }
                else
                {
                    wolfBoss.boxText += "\n" + wolfBoss.party.myParty[target].memberName + " resists the bleed";
                }
            }
            else
            {
                wolfBoss.boxText = wolfBoss.party.myParty[target].memberName + " dodges the bite";
            }
            //needs to play animation
            //bite
        }
    }

}
