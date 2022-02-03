using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomDisplay : MonoBehaviour
{
    [SerializeField] private Text roomNameText;
    [SerializeField] private Text playerCountText;
    [SerializeField] private Button joinRoomButton;

    private RoomInfo room;

    public void Setup(RoomInfo roomInfo)
    {
        room = roomInfo;
        roomNameText.text = roomInfo.Name;
        playerCountText.text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + " Players";
        joinRoomButton.onClick.AddListener(JoinRoom);
    }

    public void UpdateInfo(RoomInfo roomInfo)
    {
        room = roomInfo;
        roomNameText.text = roomInfo.Name;
        playerCountText.text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + " Players";
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(room.Name);
    }
}
