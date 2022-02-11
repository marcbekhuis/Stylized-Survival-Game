using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject Inventory;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerPaused) return;

        if (InputManager.instance.GetKeyDown(InputActions.OpenInventory))
        {
            ItemMovement.instance.movementActive = !Inventory.activeSelf;
            Inventory.SetActive(!Inventory.activeSelf);
            GameManager.instance.inventoryOpen = Inventory.activeSelf;

            if (Inventory.activeSelf) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = Inventory.activeSelf;
        }
    }
}
