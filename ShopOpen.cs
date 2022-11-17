using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    [SerializeField] private ShopMenu shop;
    private bool inRange = false;
    [SerializeField] public GameObject interactText;
    public bool paused = false;
    [SerializeField] private Paused isPaused;

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.Space) && !paused && !isPaused.isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            paused = true;
            Time.timeScale = 0f;
            interactText.gameObject.SetActive(false);
            shop.started = true;
            shop.fullShop.gameObject.SetActive(true);
            isPaused.isPaused = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            interactText.gameObject.SetActive(false);
        }
    }
}
