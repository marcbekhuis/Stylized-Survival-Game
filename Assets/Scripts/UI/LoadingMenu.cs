using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMenu : Singleton<LoadingMenu>
{
    [SerializeField] private GameObject[] disableMenus;
    [SerializeField] private GameObject loadingMenu;

    public void EnableLoadingMenu()
    {
        loadingMenu.SetActive(true);
        foreach (var menu in disableMenus)
        {
            menu.SetActive(false);
        }
    }
}
