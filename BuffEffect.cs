using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : MonoBehaviour
{
    public static void StatusEffect(int turns,int which, float regen, Stats target)
    {
        if(which == 0)//adrenaline
        {
            target.adrenStacks = turns;
            target.adrenalin = true;
        }
        else if(which == 1)//impervious
        {
            target.impervStacks = turns;
            target.impervious = true;
        }
        else if (which == 2)//regen
        {
            target.regenStacks = turns;
            target.regen = true;
            target.regenAmount = Mathf.RoundToInt(target.hpMax * regen);
        }
        else if (which == 3)//strengthened
        {
            target.strStacks = turns;
            target.strengthened = true;
        }
        else if (which == 4)//braced
        {
            target.bracedStacks = turns;
            target.braced = true;
        }
    }
}
