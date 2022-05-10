using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string load;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public void OnTriggerEnter2D(Collider2D other)//code to transistion between scenes when the player walks into a collider
    {
        if(other.CompareTag("Player")&& !other.isTrigger)
        {
            playerStorage.intitalValue = playerPosition;
            SceneManager.LoadScene(load);
        }
    }
}
