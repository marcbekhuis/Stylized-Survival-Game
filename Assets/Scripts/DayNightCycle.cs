using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DayNightCycle : MonoBehaviourPunCallbacks
{

    public GameObject light;
    public int durationOfHour;
    public float durationOfDay;
    [Range(0, 24)] public float timeOfDay = 12;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("RPCPassTime", newPlayer, timeOfDay);
        }
    }

    [PunRPC]
    void RPCPassTime(float newTime)
    {
        timeOfDay = newTime;
        updateLightAngle();
    }

    // Update is called once per frame
    void Update()
    {

        SetTime();
    }

    private void updateLightAngle()
    {
        var rotation = new Vector3(360 / 24 * timeOfDay, 0, 0);
        light.transform.eulerAngles = rotation;
    }

    private void SetTime()
    {
        timeOfDay += 24f / durationOfDay / 60 * Time.deltaTime;
        if (timeOfDay > 24) timeOfDay -= 24;
        updateLightAngle();
        if (photonView.IsMine)
        {
            photonView.RPC("RPCPassTime", RpcTarget.Others, timeOfDay);
        }
    }
}
