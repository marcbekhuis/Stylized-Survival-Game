using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private float gravity = -10;
    [SerializeField] private string playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    public static GameManager instance;

    protected void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        SpawnPlayerCharacter();
    }

    public Vector3 Gravity { get => Vector3.up * gravity * Time.deltaTime; }

    public void SpawnPlayerCharacter()
    {
        Vector3 spawnPoint = Vector3.zero;

        if (spawnPoints.Length > 0)
        {
            List<Transform> availableSpawnPoints = new List<Transform>();
            foreach (var point in spawnPoints)
            {
                if (!Physics.CheckSphere(point.position, 0.1f))
                    availableSpawnPoints.Add(point);
            }
            if(availableSpawnPoints.Count > 0)
                spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)].position;
        }

        PhotonNetwork.Instantiate(playerPrefab, spawnPoint, new Quaternion(0,0,0,0));
    }
}
