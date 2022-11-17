using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditoBossBehavior : MonoBehaviour
{
    public static void BanditoBossAttack(EnemyStats boss)
    {
        int action = Random.Range(1, 101);
        if (action < 25)//Grenade Boque
        {
            boss.boxText = boss.EnemyName + " throws a grenade bouquet";
            for (int i = 0; i < boss.party.myParty.Count; i++)
            {
                Damage.DamageTarget(boss.damMin, boss.damMax, null, boss.party.myParty[i], boss, null, boss.results);
                if (boss.results.damAmount > 0)
                {
                    boss.boxText += "\n" + boss.party.myParty[i].memberName + " is exploded for " + boss.results.damAmount + "damage";
                    float res = Random.Range(1, 100);
                    if (res > boss.party.myParty[i].effectRes)
                    {
                        boss.party.myParty[i].broken = true;
                        boss.party.myParty[i].brokenStacks += 2;
                    }
                    if (res > boss.party.myParty[i].effectRes)
                    {
                        boss.party.myParty[i].bleed = true;
                        boss.party.myParty[i].bleedStacks = 2;
                        boss.party.myParty[i].bleedAmount = Mathf.RoundToInt(0.5f * boss.results.damAmount);
                    }
                }
                else
                {
                    boss.boxText += "\n" + boss.party.myParty[i].memberName + " dodges the explosion";
                }
            }
        }
        else if (action < 80)//50 cal burst fire
        {
            int target = Random.Range(0, boss.party.myParty.Count);
            boss.boxText = boss.EnemyName + " burst fires the 50 cal at " + boss.party.myParty[target].memberName;
            boss.accuracy -= 25;
            Damage.DamageTarget(boss.damMin, boss.damMax, null, boss.party.myParty[target], boss, null, boss.results);
            if (boss.results.damAmount > 0)
            {
                boss.boxText += "\n" + boss.party.myParty[target].memberName + " is shot for " + boss.results.damAmount + " damage";
            }
            else
            {
                boss.boxText = boss.party.myParty[target].memberName + " dodges the shot";
            }
            Damage.DamageTarget(boss.damMin, boss.damMax, null, boss.party.myParty[target], boss, null, boss.results);
            if (boss.results.damAmount > 0)
            {
                boss.boxText += "\n" + boss.party.myParty[target].memberName + " is shot for " + boss.results.damAmount + " damage";
            }
            else
            {
                boss.boxText += "\n" + boss.party.myParty[target].memberName + " dodges the shot";
            }
            Damage.DamageTarget(boss.damMin, boss.damMax, null, boss.party.myParty[target], boss, null, boss.results);
            if (boss.results.damAmount > 0)
            {
                boss.boxText += "\n" + boss.party.myParty[target].memberName + " is shot for " + boss.results.damAmount + " damage";
            }
            else
            {
                boss.boxText += "\n" + boss.party.myParty[target].memberName + " dodges the shot";
            }
            boss.accuracy += 25;
        }
        else if (action <= 100 && boss.hpCurrent <= Mathf.RoundToInt(boss.hpMax * .25f))//injects syringe
        {
            int heal = Random.Range(boss.damMin * 2, boss.damMax * 2+1);
            boss.boxText = boss.EnemyName + " injects themselves with a syringe. \nThey heal for " + heal + " health and gain two stacks of strengthened";
            boss.strengthened = true;
            boss.strStacks += 2;
        }
        else//flashbang
        {
            boss.boxText = boss.EnemyName + " throws a flashbang";
            for (int i = 0; i < boss.party.myParty.Count; i++)
            {
                float res = Random.Range(1, 101);
                if (res > boss.party.myParty[i].effectRes)
                {
                    boss.party.myParty[i].blind = true;
                    boss.party.myParty[i].blindStacks += 2;
                }
                else
                {
                    boss.boxText = "\n" + boss.party.myParty[i].memberName + " resists the blind";
                }
            }
        }
    }
}
