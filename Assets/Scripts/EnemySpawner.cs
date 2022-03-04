using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class EnemySpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private float minPlacementHeight = 0;
    [SerializeField] private float maxPlacementHeight = 500;
    [SerializeField] LayerMask hitTarget;
    [SerializeField] private EnemyEntry[] enemyPrefabs;

    public static EnemySpawner instance;
    private List<Transform> players;

    public class EnemyEntry
    {
        public GameObject prefab;
        public int maxAmount = 10;
        public float spawnCooldown = 10;
        public float minSpawnRadius = 10;
        public float maxSpawnRadius = 40;
        public Vector3 groundOffset = new Vector3(0,1,0);

        [HideInInspector] public float nextSpawn = 0;
        [HideInInspector] public int amountSpawned = 0;
    }

    public void AddPlayerTransform(Transform player)
    {
        players.Add(player);
    }

    public void RemovePlayerTransform(Transform player)
    {
        players.Remove(player);
    }

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var enemyPrefab in enemyPrefabs)
        {
            if (GameManager.instance.gameTime < enemyPrefab.nextSpawn) continue;
            if (enemyPrefab.amountSpawned >= enemyPrefab.maxAmount) continue;
            if (players.Count <= 0) return;

            enemyPrefab.nextSpawn = GameManager.instance.gameTime + enemyPrefab.spawnCooldown;
            enemyPrefab.amountSpawned++;

            PhotonNetwork.Instantiate(enemyPrefab.prefab.name, GetRandomPosition(enemyPrefab), new Quaternion(0,0,0,0));
        }
    }

    private Vector3 GetRandomPosition(EnemyEntry enemyPrefab)
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 position = players[Random.Range(0, players.Count)].position;
            float[] scale = new float[] { -1, 1 };
            float x = Random.Range(enemyPrefab.minSpawnRadius, enemyPrefab.maxSpawnRadius) * scale[Random.Range(0, scale.Length)];
            float z = Random.Range(enemyPrefab.minSpawnRadius, enemyPrefab.maxSpawnRadius) * scale[Random.Range(0, scale.Length)];
            position += new Vector3(x, 0, z);
            RaycastHit[] hits = Physics.RaycastAll(new Vector3(position.x, maxPlacementHeight, position.y), Vector3.down, maxPlacementHeight - minPlacementHeight, hitTarget);
            if (hits.Length <= 0) continue;
            return hits[0].point;
        }

        return Vector3.zero;
    }
}
