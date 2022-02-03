using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject serverList;
    public GameObject createServer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
        if (Application.isPlaying)
        {
            EditorApplication.Exit(0);
        }
    }

    public void Back()
    {
        serverList.SetActive(true);
        createServer.SetActive(false);
    }
}
