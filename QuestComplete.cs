using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestComplete : MonoBehaviour
{
    [SerializeField] private QuestList list;
    [SerializeField] private CurrentParty party;

    void Update()
    {
        for(int i = 0; i < list.myQuests.Count; i++)
        {
            if(list.myQuests[i].isComplete)
            {
                //give rewards
                party.myParty[0].credits += list.myQuests[i].creditReward;
                for(int k = 0;k<party.myParty.Count;k++)
                {
                    party.myParty[k].exp += list.myQuests[i].xpReward;
                    //check for level up
                    if(party.myParty[k].exp>=party.myParty[k].expNext)
                    {
                        LevelUp.LevelUpTarget(party.myParty[k]);
                    }
                }
                list.myQuests.Remove(list.myQuests[i]);
            }
        }
    }
}
