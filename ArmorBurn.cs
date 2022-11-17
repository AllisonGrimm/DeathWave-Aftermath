using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBurn : MonoBehaviour
{
    public static void ArmorBurnAbility(SkillTemplate skill)
    {
        skill.user.energyCurrent -= 7;
        skill.eTarget.broken = true;
        skill.eTarget.brokenStacks += 3;
    }
}
