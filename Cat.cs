using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private LoidHat quest;
    [SerializeField] private GameObject cat;
    [SerializeField] private GameObject cat2;
    [SerializeField] private Speak speakCurrent;
    [SerializeField] private Quests current;
    private bool hasTalked;

    void Update()
    {
        if(quest.started)
        {
            cat.gameObject.SetActive(true);
            quest.started = false;
            current.started = false;
        }
        if(quest.second)
        {
            cat2.gameObject.SetActive(true);
            quest.second = false;
        }

        if (speakCurrent.dialogFin && !hasTalked)
        {
            quest.second = true;
            cat.gameObject.SetActive(false);
            hasTalked = true;
            current.currentStep += 1;
            speakCurrent.dialogFin = false;
            //cat runs away
        }

        if(speakCurrent.dialogFin && hasTalked)
        {
            quest.fin = true;
            cat.gameObject.SetActive(false);
            current.currentStep += 1;
            speakCurrent.dialogFin = false;
        }
    }
}
