using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagDrill : MonoBehaviour
{
    public static void Magazine(SkillTemplate skill)
    {
        skill.description = "Three free reloads ready";
        skill.user.energyCurrent -= 10;
        skill.user.freeReload = true;
        skill.user.freeReloadTurns = 3;
    }
}
