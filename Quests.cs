using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest/Quest")]

public class Quests : ScriptableObject
{
    public string questName;
    public string questDescription;
    [TextArea(3, 10)]
    public string[] questSteps;//updates to next whenever step complete
    public int currentStep;
    public int xpReward;
    public int creditReward;
    public bool isComplete;//When talk to quest giver again finishes quest
    public bool started;
    //some way to give items as reward
    public void GiveRewards()
    {
        //add player xp check for level
        //add money
    }

    //in the instance of the two main sidequests they "finish" when you beat the bosses
}
