using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private float gravity = -10;
    [SerializeField] private AnimationCurve gravityTimeMultiplier;
    [SerializeField] private string playerPrefab;
    [SerializeField] private Transform spawnPoints;
    [SerializeField] private SetDisplayVariables displayVariables;
    [Space]
    [SerializeField] private GameObject deathCamera;
    [SerializeField] private GameObject respawnMenu;
    [SerializeField] private GameObject gameUI;

    public bool inventoryOpen = false;
    public bool playerPaused = false;
    public bool playerGameInputBlocked { get => inventoryOpen || playerPaused; }
    public static GameManager instance;

    public float gameTime = 0;

    protected void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
        UseableItem.ClearEvents();
    }

    private void Start()
    {
        SpawnPlayerCharacter();
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        gameTime += Time.deltaTime;
        photonView.RPC("RPCSyncTime", RpcTarget.Others, gameTime);
    }

    [PunRPC]
    private void RPCSyncTime(float time)
    {
        gameTime = time;
    }

    public Vector3 Gravity(float time) 
    { 
        return Vector3.up * (gravity * (time * gravityTimeMultiplier.Evaluate(time))) * Time.deltaTime; 
    }

    public void ToggleDeathScreen(bool active)
    {
        gameUI.SetActive(!active);
        deathCamera.SetActive(active);
        respawnMenu.SetActive(active);
        if (active) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = active;
    }

    public void SpawnPlayerCharacter()
    {
        Vector3 spawnPoint = Vector3.zero;

        if (spawnPoints.childCount > 0)
        {
            List<Transform> availableSpawnPoints = new List<Transform>();
            foreach (Transform point in spawnPoints)
            {
                if (point.gameObject.activeSelf)
                    if (!Physics.CheckSphere(point.position, 0.1f))
                        availableSpawnPoints.Add(point);
            }
            if(availableSpawnPoints.Count > 0)
                spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)].position;
        }

        GameObject player = PhotonNetwork.Instantiate(playerPrefab, spawnPoint, new Quaternion(0,0,0,0));
        displayVariables.SetVariable(player);
    }
}
