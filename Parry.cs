using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public static void ParryAb(SkillTemplate skill)
    {
        skill.boxText = skill.target.memberName + " gains two stacks of braced and is ready to parry the next attack";
        skill.user.braced = true;
        skill.user.bracedStacks = 2;
        skill.user.energyCurrent -= 8;
        //some sort of code to do parry maybe implement it like a status effect
    }
}
