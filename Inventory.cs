using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static bool gamePaused = false;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Paused paused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))//Key to display inventory
        {
            if (gamePaused)
            {
                Resume();
            }
            else if(!paused.isPaused)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        paused.isPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inventoryMenu.SetActive(true);
        inventoryManager.started = true;
        Time.timeScale = 0f;
        gamePaused = true;
        paused.isPaused = true;
    }
}
