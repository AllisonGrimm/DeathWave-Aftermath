using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loid : MonoBehaviour
{
    [SerializeField] private LoidHat controller;
    [SerializeField] private Speak speak;
    [SerializeField] private Dialog altDialog;
    [SerializeField] private Quests quest;

    void Update()
    {
        if(controller.fin)
        {
            speak.dialog = altDialog;
        }
        if(speak.dialogFin)
        {
            quest.isComplete = true;
            speak.dialogFin = false;
        }
    }
}
