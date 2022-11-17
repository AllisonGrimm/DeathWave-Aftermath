using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedShot : MonoBehaviour
{
    public static void AimedShotAbility(SkillTemplate skill)
    {
        skill.user.accuracy += 25;
        skill.user.energyCurrent -= 5;
        skill.user.magCurrent -= skill.user.ammoPerShot;
        int damageIncrease = Random.Range(1, 101);
        if(damageIncrease>50)
        {
            Damage.DamageTarget(skill.user.damMin * 2,skill.user.damMax * 2, skill.eTarget, null, null, skill.user,skill.results);
        }
        else
        {
            Damage.DamageTarget(skill.user.damMin, skill.user.damMax, skill.eTarget, null, null, skill.user,skill.results);
        }
        if (skill.results.damAmount > 0)
        {
            skill.boxText = skill.eTarget.EnemyName + " takes " + skill.results.damAmount + " damage";
        }
        else
        {
            skill.boxText = skill.eTarget.EnemyName + " dodges the attack";
        }
        skill.user.accuracy -= 25;
    }
}
