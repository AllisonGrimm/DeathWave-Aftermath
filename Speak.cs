using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speak : MonoBehaviour
{
    [SerializeField] private DialogManager manager;
    private bool inRange = false;
    [SerializeField] public Dialog dialog;
    [SerializeField] public GameObject interactText;
    public bool paused = false;
    public bool dialogFin = false;
    void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.Space)&&!paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            paused = true;
            Time.timeScale = 0f;
            interactText.gameObject.SetActive(false);
            manager.StartDialog(dialog,this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inRange = true;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            inRange = false;
            interactText.gameObject.SetActive(false);
        }
    }
}
