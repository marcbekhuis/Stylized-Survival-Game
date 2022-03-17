 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SinglePlayerMode : MonoBehaviour
{
    [SerializeField] private bool ShouldCheckForOffline = false;
    [SerializeField] private string sceneName = "GameScene";

    // Start is called before the first frame update
    void Awake()
    {
        if (!PhotonNetwork.IsConnected && ShouldCheckForOffline)
        {
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.CreateRoom("Singleplayer");
        }
    }

    public void CreateSinglePlayerSession()
    {
        PhotonNetwork.OfflineMode = true;
        PhotonNetwork.CreateRoom("Singleplayer");
        LoadingMenu.instance.EnableLoadingMenu();
        PhotonNetwork.LoadLevel(sceneName);
    }
}
