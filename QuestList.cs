using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewList", menuName = "Quest/QuestList")]
public class QuestList : ScriptableObject
{
    public List<Quests> myQuests = new List<Quests>();
}
