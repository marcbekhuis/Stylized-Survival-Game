using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    [SerializeField] private InputActions inputAction;
    [SerializeField] private GameObject activateMenu;
    [SerializeField] private GameObject[] deactivateMenus;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerPaused) return;

        if (InputManager.instance.GetKeyDown(inputAction))
        {
            ItemMovement.instance.movementActive = !activateMenu.activeSelf;
            activateMenu.SetActive(!activateMenu.activeSelf);
            GameManager.instance.inventoryOpen = activateMenu.activeSelf;

            if (activateMenu.activeSelf)
            {
                foreach (var menu in deactivateMenus)
                {
                    menu.SetActive(false);
                }
            }

            if (activateMenu.activeSelf) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = activateMenu.activeSelf;
        }
    }
}
