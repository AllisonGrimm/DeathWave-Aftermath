using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionBattle : MonoBehaviour
{
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public void BattleTransition()
    {
        SceneManager.LoadScene("Battle");
    }
}
