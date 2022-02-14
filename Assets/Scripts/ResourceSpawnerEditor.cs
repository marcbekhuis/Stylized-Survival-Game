using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ResourceSpawnerEditor : MonoBehaviour
{
    [SerializeField] private bool replaceResources = false;
    [SerializeField] private float minRadius = 1;
    [SerializeField] private int numberOfResources = 1000;
    [SerializeField] private Vector2 randomSizeRange = new Vector2(0.5f, 1.5f);
    [SerializeField] private Vector2 resourceFieldX = new Vector2(-1000, 1000);
    [SerializeField] private Vector2 resourceFieldZ = new Vector2(-1000, 1000);
    [SerializeField] private float minPlacementHeight = 0;
    [SerializeField] private float maxPlacementHeight = 0;
    [SerializeField] private GameObject[] resourcePrefabs;
    [SerializeField] LayerMask hitTarget;

    private List<GameObject> placedResources = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (!replaceResources) return;
        while(placedResources.Count > 0)
        {
            DestroyImmediate(placedResources[0]);
            placedResources.RemoveAt(0);
        }

        for (int i = 0; i < numberOfResources; i++)
        {
            if (!PlaceResource()) i--;
        }
        replaceResources = false;
    }

    private bool PlaceResource()
    {
        Vector2 positionNoHeight = new Vector2(Random.Range(resourceFieldX.x, resourceFieldX.y), Random.Range(resourceFieldZ.x, resourceFieldZ.y));
        RaycastHit[] hits = Physics.RaycastAll(new Vector3(positionNoHeight.x, 600, positionNoHeight.y), Vector3.down, 600, hitTarget);
        if (hits.Length <= 0) return false; 
        Vector3 position = hits[0].point;

        foreach (var resource in placedResources)
        {
            if (Vector3.Distance(position, resource.transform.position) < minRadius) return false;
        }

        GameObject placedResource = Instantiate(resourcePrefabs[Random.Range(0, resourcePrefabs.Length)], position, new Quaternion(0,0,0,0), this.transform);
        placedResource.transform.localScale = Vector3.one * Random.Range(randomSizeRange.x, randomSizeRange.y);
        placedResource.transform.eulerAngles = new Vector3(0, Random.Range(0f, 360f), 0);
        placedResources.Add(placedResource);
        return true;
    }
}
