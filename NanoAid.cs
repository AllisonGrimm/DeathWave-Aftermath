using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoAid : MonoBehaviour
{
    public static void Nano(SkillTemplate skill)
    {
        int heal = Random.Range(skill.user.damMin * 3, skill.user.damMax * 3+1);
        skill.boxText = skill.target.memberName + " is healed for " + heal + " hp";
        skill.target.hpCurrent += heal;
        skill.user.energyCurrent -= 10;
        if(skill.target.hpCurrent>skill.target.hpMax)
        {
            skill.target.hpCurrent = Mathf.RoundToInt(skill.target.hpMax);
        }
    }
}
