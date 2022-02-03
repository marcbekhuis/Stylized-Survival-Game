using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class CreatePhotonRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField roomNameInputfield;
    [SerializeField] private InputField maxPlayersInputfield;
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Text errorMessage;
    [SerializeField] private string sceneName = "GameScene";

    public void CreateRoom()
    {
        string roomName = roomNameInputfield.text.Trim();
        createRoomButton.interactable = false;

        if (roomName == "")
        {
            errorMessage.text = "No room name given";
            return;
        }

        int maxPlayers = int.Parse(maxPlayersInputfield.text.Trim());
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)maxPlayers;
        options.IsOpen = true;
        options.IsVisible = true;

        PhotonNetwork.CreateRoom(roomName, options);
    }

    public override void OnCreatedRoom()
    {
        createRoomButton.interactable = true;
        errorMessage.text = "";
        PhotonNetwork.LoadLevel(sceneName);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError(returnCode);
        errorMessage.text = message;
        createRoomButton.interactable = true;
    }
}
