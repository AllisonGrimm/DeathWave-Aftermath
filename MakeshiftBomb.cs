using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeshiftBomb : MonoBehaviour
{
    public static void MakeshiftBombAbility(SkillTemplate skill)
    {
        skill.user.energyCurrent -= 10;
        for (int i = 0; i < skill.group.enemyGroup.Count; i++)
        {
            Damage.DamageTarget(Mathf.RoundToInt(skill.user.damMin * .4f), Mathf.RoundToInt(skill.user.damMax * .4f), skill.group.enemyGroup[i], null, null, skill.user, skill.results);
        }
        if(skill.user.dexterity>skill.user.intellect&& skill.user.dexterity >skill.user.power&& skill.user.dexterity > skill.user.affinity&&skill.user.dexterity > skill.user.tenacity)
        {
            for (int i = 0; i < skill.group.enemyGroup.Count; i++)
            {
                int apply = Random.Range(1, 101);
                if (apply > 25)
                {
                    int res = Random.Range(1, 101);
                    if (res > skill.group.enemyGroup[i].effectRes)
                    {
                        skill.group.enemyGroup[i].bleed = true;
                        skill.group.enemyGroup[i].bleedAmount = skill.results.damAmount;
                        skill.group.enemyGroup[i].bleedStacks = 1;
                    }
                }
            }
        }
        else if(skill.user.intellect > skill.user.dexterity && skill.user.intellect > skill.user.power && skill.user.intellect > skill.user.affinity && skill.user.intellect > skill.user.tenacity)
        {
            for (int i = 0; i < skill.group.enemyGroup.Count; i++)
            {
                int apply = Random.Range(1, 101);
                if (apply > 50)
                {
                    int res = Random.Range(1, 101);
                    if (res > skill.group.enemyGroup[i].effectRes)
                    {
                        skill.group.enemyGroup[i].blind = true;
                        skill.group.enemyGroup[i].blindStacks += 1;
                    }
                }
            }
        }
        else if(skill.user.power > skill.user.intellect && skill.user.power > skill.user.dexterity && skill.user.power > skill.user.affinity && skill.user.power > skill.user.tenacity)
        {
            for (int i = 0; i < skill.group.enemyGroup.Count; i++)
            {
                int apply = Random.Range(1, 101);
                if (apply > 50)
                {
                    int res = Random.Range(1, 101);
                    if (res > skill.group.enemyGroup[i].effectRes)
                    {
                        skill.group.enemyGroup[i].broken = true;
                        skill.group.enemyGroup[i].brokenStacks += 1;
                    }
                }
            }
        }
        else if(skill.user.affinity > skill.user.intellect && skill.user.affinity > skill.user.power && skill.user.affinity > skill.user.dexterity && skill.user.affinity > skill.user.tenacity)
        {
            for (int i = 0; i < skill.group.enemyGroup.Count; i++)
            {
                int apply = Random.Range(1, 101);
                if (apply > 50)
                {
                    int res = Random.Range(1, 101);
                    if (res > skill.group.enemyGroup[i].effectRes)
                    {
                        skill.group.enemyGroup[i].weak = true;
                        skill.group.enemyGroup[i].weakStacks += 1;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < skill.group.enemyGroup.Count; i++)
            {
                int apply = Random.Range(1, 101);
                if (apply > 50)
                {
                    int res = Random.Range(1, 101);
                    if (res > skill.group.enemyGroup[i].effectRes)
                    {
                        skill.group.enemyGroup[i].stun = true;
                        skill.group.enemyGroup[i].stunStacks += 1;
                    }
                }
            }
        }
    }
}
