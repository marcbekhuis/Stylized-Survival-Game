using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSystem : MonoBehaviour
{
    [SerializeField] private int numberOfClouds = 40;
    [SerializeField] private Vector2 speedRange = new Vector2(0, 50);
    [SerializeField] private Vector2 heightRange = new Vector2(-50, 50);
    [SerializeField] private Vector2 scaleRange = new Vector2(0.5f, 2);
    [SerializeField] private Vector2 cloudFieldX = new Vector2(-400f, 400f);
    [SerializeField] private Vector2 cloudFieldZ = new Vector2(-400f, 400f);
    [SerializeField] private GameObject[] cloudPrefabs;

    private float speed = 10;
    private Vector3 direction = new Vector3(1, 0, 0);
    private List<GameObject> placedClouds = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(Random.Range(-1f, 1), 0, Random.Range(-1f, 1));
        speed = Random.Range(speedRange.x, speedRange.y);

        for (int i = 0; i < numberOfClouds; i++)
        {
            PlaceCloud();
        }
    }

    // Update is called once per frame
    void Update()
    {
        KeepCloudsInField();
        ChangeFlow();
        MoveClouds();
    }

    private void ChangeFlow()
    {
        direction = new Vector3(
            Mathf.Clamp(direction.x + Random.Range(-0.1f, 0.1f) * Time.deltaTime, -1, 1),
            0,
            Mathf.Clamp(direction.z + Random.Range(-0.1f, 0.1f) * Time.deltaTime, -1, 1)
            );

        speed = Mathf.Clamp(speed + Random.Range(-1f, 1f) * Time.deltaTime, speedRange.x, speedRange.y);
    }

    private void PlaceCloud()
    {
        GameObject cloud = Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Length)], new Vector3(Random.Range(cloudFieldX.x, cloudFieldX.y), this.transform.position.y + Random.Range(heightRange.x, heightRange.y), Random.Range(cloudFieldZ.x, cloudFieldZ.y)), new Quaternion(0, 0, 0, 0), this.transform);
        cloud.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
        cloud.transform.eulerAngles = new Vector3(0, 90 * Random.Range(0, 3), 0);
        placedClouds.Add(cloud);
    }

    private void KeepCloudsInField()
    {
        foreach (var cloud in placedClouds)
        {
            if ((cloud.transform.position.x < cloudFieldX.x && direction.x < 0) ||
                (cloud.transform.position.x > cloudFieldX.y && direction.x > 0) ||
                (cloud.transform.position.z < cloudFieldZ.x && direction.z < 0) ||
                (cloud.transform.position.z > cloudFieldZ.y && direction.z > 0))
            {
                cloud.transform.position = new Vector3(cloud.transform.position.x * -1, this.transform.position.y + Random.Range(heightRange.x, heightRange.y), cloud.transform.position.z * -1);
                cloud.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
            }
        }
    }

    private void MoveClouds()
    {
        foreach (var cloud in placedClouds)
        {
            cloud.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
}
