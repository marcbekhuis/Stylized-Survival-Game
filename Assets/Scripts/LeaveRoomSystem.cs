using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LeaveRoomSystem : MonoBehaviour
{
    [SerializeField] private string redirectScene = "MainMenu";

    public void LeaveRoomScene()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(redirectScene);
    }

    public void LeaveRoomQuit()
    {
        PhotonNetwork.LeaveRoom();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}