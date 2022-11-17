using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponseSlot : MonoBehaviour
{
    [Header("UI stuff to change")]
    [SerializeField] private TextMeshProUGUI nameS;

    [Header("Variables from item")]
    public DialogManager thisMenu;
    public Response thisResponse;
    public Speak thisSpeak;
    public int responseNum;

    public void Setup(Response response, DialogManager newMenu, Speak speak, int num)//Sets the inventory slot up
    {
        responseNum = num;
        thisResponse = response;
        thisMenu = newMenu;
        thisSpeak = speak;
        nameS.text = response.responseS;
    }

    public void ClickedOn()//passes the correct data back to the manager when the item is clicked on
    {
        if (thisResponse)
        {
            thisMenu.responseNum = responseNum;
            thisMenu.StartDialog(thisResponse.associatedDialog, thisSpeak);
        }
    }
}
