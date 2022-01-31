using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LoginPhoton : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField inputField;
    [SerializeField] private Button loginButton;
    [SerializeField] private Text errorMessage;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject mainMenuPanel;

    private void Start()
    {
        PhotonNetwork.GameVersion = Application.version;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Login()
    {
        loginButton.interactable = false;
        errorMessage.text = "";
        string nickname = inputField.text.Trim();
        if (nickname == "")
        {
            loginButton.interactable = true;
            errorMessage.text = "No username given";
            return;
        }
        PhotonNetwork.NickName = nickname;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        loginButton.interactable = true;
        loginPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
