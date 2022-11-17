using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questBox;
    [SerializeField] private DialogManager thisText;
    [SerializeField] private QuestList questList;
    [SerializeField] private Quests thisQuest;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDesc;

    public void AcceptQuest()
    {
        questList.myQuests.Add(thisQuest);
        thisQuest.started = true;
        questBox.gameObject.SetActive(false);
        thisText.textBox.gameObject.SetActive(true);
        thisText.speakCurrent.dialog.responses.respon.Remove(thisText.speakCurrent.dialog.responses.respon[thisText.responseNum]);
        thisText.ClearResponses();
        thisText.CreateResponses();
    }

    public void CancelQuest()
    {
        questBox.gameObject.SetActive(false);
        thisText.textBox.gameObject.SetActive(true);
    }

    public void FillQuest(Quests currentQuest, DialogManager textBox)
    {
        thisText = textBox;
        thisQuest = currentQuest;
        questBox.gameObject.SetActive(true);
        questName.text = currentQuest.questName;
        questDesc.text = currentQuest.questDescription;
    }
}
