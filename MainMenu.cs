using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()//Loads the character creation scene
    {
        SceneManager.LoadScene("CharSelect");
    }
    public void LoadGame()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
