using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] private InventoryItem collectable;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private int num;
    private bool inRange;
    private bool paused;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject itemBox;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject boxOpen;

    private void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.Space)&&!paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            paused = true;
            Time.timeScale = 0f;
            interactText.gameObject.SetActive(false);
            itemBox.gameObject.SetActive(true);
        }
    }

    public void AcceptItem()
    {
        if(collectable.isConsumable)
        {
            for(int i = 0; i<inventory.myInven.Count;i++)
            {
                if(inventory.myInven[i].itemName==collectable.itemName)
                {
                    inventory.myInven[i].numHeld += num;
                }
            }
        }
        else
        {
            inventory.myInven.Add(collectable);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        Time.timeScale = 1f;
        itemBox.gameObject.SetActive(false);
        box.gameObject.SetActive(false);
        boxOpen.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            interactText.gameObject.SetActive(true);
        }
    }
}
