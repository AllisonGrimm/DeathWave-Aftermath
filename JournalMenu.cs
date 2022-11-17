using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDesc;
    [SerializeField] private TextMeshProUGUI questStep;
    [SerializeField] private GameObject journalWindow;
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private GameObject blankQuest;
    [SerializeField] private QuestList list;
    private Quests currentQuest;
    public bool started = false;

    private void SetText(string qName,string qDesc,string qStep)
    {
        questName.text = qName;
        questDesc.text = qDesc;
        questStep.text = qStep;
    }

    private void SetupQuests()
    {
        if(list)
        {
            for(int i = 0; i< list.myQuests.Count;i++)
            {
                GameObject temp = Instantiate(blankQuest, journalPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(journalPanel.transform);
                QuestSlot newSlot = temp.GetComponent<QuestSlot>();
                if (newSlot)
                {
                    newSlot.Setup(list.myQuests[i], this);
                }
            }
        }
    }

    public void SetupQuest(string qName,string qDesc,int qStep,Quests thisQuest)
    {
        currentQuest = thisQuest;
        questName.text = qName;
        questDesc.text = qDesc;
        questStep.text = thisQuest.questSteps[qStep];
    }

    private void ClearQuests()
    {
        for (int i = 0; i < journalPanel.transform.childCount; i++)
        {
            Destroy(journalPanel.transform.GetChild(i).gameObject);
        }
    }

    void Start()
    {
        SetText("", "", "");
        SetupQuests();
    }

    void Update()
    {
        if(started)
        {
            SetText("", "", "");
            ClearQuests();
            SetupQuests();
            started = false;
        }
    }
}
