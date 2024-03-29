using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private int Menu;
    public static bool gamePaused = false;
    [SerializeField] private GameObject pausemenuUI;
    [SerializeField] private Paused paused;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
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
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        paused.isPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        paused.isPaused = true;
    }
    
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Menu);
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }

    public void Options()
    {

    }
}
