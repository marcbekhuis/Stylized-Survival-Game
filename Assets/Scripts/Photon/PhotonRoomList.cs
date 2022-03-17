using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoomList : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomDisplay roomDisplayPrefab;
    [SerializeField] private Transform roomDisplayParent;
    [SerializeField] private GameObject feedbackText;

    private Dictionary<string, RoomDisplay> roomsDisplay = new Dictionary<string, RoomDisplay>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var room in roomList)
        {
            if (room.RemovedFromList)
            {
                if (roomsDisplay.ContainsKey(room.Name))
                {
                    Destroy(roomsDisplay[room.Name].gameObject);
                    roomsDisplay.Remove(room.Name);
                }
                break;
            }
            if (!roomsDisplay.ContainsKey(room.Name))
            {
                RoomDisplay roomDisplay = Instantiate(roomDisplayPrefab, roomDisplayParent);
                roomDisplay.GetComponent<RoomDisplay>().Setup(room);
                roomsDisplay.Add(room.Name, roomDisplay);
                roomDisplay.gameObject.SetActive(room.IsOpen && room.IsVisible);
            }
            else
            {
                roomsDisplay[room.Name].UpdateInfo(room);
                roomsDisplay[room.Name].gameObject.SetActive(room.IsOpen && room.IsVisible);
            }
        }
        feedbackText.SetActive(roomList.Count <= 0);
    }

    public override void OnJoinedLobby()
    {
        ClearRooms();
    }

    public override void OnLeftLobby()
    {
        ClearRooms();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        ClearRooms();
    }

    private void ClearRooms()
    {
        foreach (var display in roomsDisplay)
        {
            Destroy(display.Value.gameObject);
        }
        roomsDisplay.Clear();
    }
}
