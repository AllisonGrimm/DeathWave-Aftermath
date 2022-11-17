using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog/Dialog")]
public class Dialog : ScriptableObject
{
    public string NPCName;
    [TextArea (3,10)]
    public string[] dialogText;
    public DialogResponses responses;
    public bool hasResponses;
    public Quests associatedQuest;
    public bool quit;
}
