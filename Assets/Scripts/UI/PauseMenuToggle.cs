using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        if (InputManager.instance.GetKeyDown(InputActions.Pause))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if(pauseMenu.activeSelf) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = pauseMenu.activeSelf;
            GameManager.instance.playerPaused = pauseMenu.activeSelf;
        }
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.instance.playerPaused = false;
    }
}
