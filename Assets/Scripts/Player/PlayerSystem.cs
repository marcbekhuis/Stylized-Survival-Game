using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnemySpawner.instance.AddPlayerTransform(this.transform);
        this.gameObject.name = "Player " + PhotonNetwork.NickName;
    }
}
