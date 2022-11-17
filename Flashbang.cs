using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashbang : MonoBehaviour
{
    public static void Flash(SkillTemplate skill)
    {
        skill.user.energyCurrent -= 6;
        for(int i = 0; i<skill.group.enemyGroup.Count;i++)
        {
            int apply = Random.Range(1, 101);
            if(apply>25)
            {
                int res = Random.Range(1, 101);
                if(res>skill.group.enemyGroup[i].effectRes)
                {
                    skill.group.enemyGroup[i].blind = true;
                    skill.group.enemyGroup[i].blindStacks += 1;
                }
            }
        }
    }
}
