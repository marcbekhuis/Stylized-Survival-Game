using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoomList : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomDisplay roomDisplayPrefab;
    [SerializeField] private Transform roomDisplayParent;

    private List<GameObject> roomsDisplay = new List<GameObject>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var roomDisplay in roomsDisplay)
        {
            Destroy(roomDisplay);
        }

        foreach (var room in roomList)
        {
            RoomDisplay roomDisplay = Instantiate(roomDisplayPrefab, roomDisplayParent);
            roomDisplay.Setup(room);
            roomsDisplay.Add(roomDisplay.gameObject);
        }
    }
}
