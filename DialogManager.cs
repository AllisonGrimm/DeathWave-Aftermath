using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    [SerializeField] public GameObject textBox;
    [SerializeField] private GameObject scrollView;
    [SerializeField] private GameObject responsePanel;
    [SerializeField] private GameObject blankResponseSlot;
    [SerializeField] private GameObject continueButton;
    public Speak speakCurrent;
    private bool quit;
    private Dialog thisDialog;
    public int responseNum;
    [SerializeField] private QuestManager manager;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI nameText;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog,Speak speak)
    {
        thisDialog = dialog;
        quit = dialog.quit;
        dialogText.gameObject.SetActive(true);
        nameText.gameObject.SetActive(true);
        scrollView.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
        sentences.Clear();
        speakCurrent = speak;
        nameText.text = dialog.NPCName;
        textBox.gameObject.SetActive(true);
        ClearResponses();

        foreach(string sentence in dialog.dialogText)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void ClearResponses()
    {
        for (int i = 0; i < responsePanel.transform.childCount; i++)
        {
            Destroy(responsePanel.transform.GetChild(i).gameObject);
        }
    }

    public void CreateResponses()
    {
        for (int i = 0; i < speakCurrent.dialog.responses.respon.Count; i++)
        {
            GameObject temp = Instantiate(blankResponseSlot, responsePanel.transform.position, Quaternion.identity);
            temp.transform.SetParent(responsePanel.transform);
            ResponseSlot newSlot = temp.GetComponent<ResponseSlot>();
            if (newSlot)
            {
                newSlot.Setup(speakCurrent.dialog.responses.respon[i], this, speakCurrent, i);
            }
        }
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0 && !speakCurrent.dialog.hasResponses)
        {
            EndDialog();
            return;
        }
        else if(sentences.Count == 0 && speakCurrent.dialog.hasResponses && !quit)
        {
            dialogText.gameObject.SetActive(false);
            nameText.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
            scrollView.gameObject.SetActive(true);
            CreateResponses();
            if(thisDialog.associatedQuest != null)
            {
                textBox.gameObject.SetActive(false);
                manager.FillQuest(thisDialog.associatedQuest,this);
            }
        }
        else if(quit)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }    

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    private void EndDialog()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        speakCurrent.paused = false;
        textBox.gameObject.SetActive(false);
        speakCurrent.interactText.gameObject.SetActive(true);
        speakCurrent.dialogFin = true;
    }
}
