using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoomList : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomDisplay roomDisplayPrefab;
    [SerializeField] private Transform roomDisplayParent;

    private Dictionary<string, RoomDisplay> roomsDisplay = new Dictionary<string, RoomDisplay>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var room in roomList)
        {
            Debug.LogError(room.Name);
            if (!roomsDisplay.ContainsKey(room.Name))
            {
                RoomDisplay roomDisplay = Instantiate(roomDisplayPrefab, roomDisplayParent);
                roomDisplay.GetComponent<RoomDisplay>().Setup(room);
                roomsDisplay.Add(room.Name, roomDisplay);
                roomDisplay.gameObject.SetActive(room.IsOpen || room.IsVisible);
            }
            else if (room.RemovedFromList)
            {
                Destroy(roomsDisplay[room.Name].gameObject);
                roomsDisplay.Remove(room.Name);
            }
            else
            {
                roomsDisplay[room.Name].UpdateInfo(room);
                roomsDisplay[room.Name].gameObject.SetActive(room.IsOpen || room.IsVisible);
            }
        }
    }
}
