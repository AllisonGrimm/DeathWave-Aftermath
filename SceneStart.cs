using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    void Start()//Locks and hides the cursor whenever a new scene loads
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
