using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ResourceSpawnerEditor : MonoBehaviour
{
    [SerializeField] private bool replaceResources = false;
    [SerializeField] private float minRadius = 1;
    [SerializeField][Range(0, 90)] private float maxAngle = 45;
    [SerializeField] private int numberOfResources = 1000;
    [SerializeField] private Vector2 randomSizeRange = new Vector2(0.5f, 1.5f);
    [SerializeField] private Vector2 resourceFieldX = new Vector2(-1000, 1000);
    [SerializeField] private Vector2 resourceFieldZ = new Vector2(-1000, 1000);
    [SerializeField] private float minPlacementHeight = 0;
    [SerializeField] private float maxPlacementHeight = 500;
    [SerializeField] private GameObject[] resourcePrefabs;
    [SerializeField] LayerMask hitTarget;

    // Update is called once per frame
    void Update()
    {
        if (!replaceResources) return;
        foreach (Transform resource in this.transform)
        {
            DestroyImmediate(resource.gameObject);
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
        RaycastHit[] hits = Physics.RaycastAll(new Vector3(positionNoHeight.x, maxPlacementHeight, positionNoHeight.y), Vector3.down, maxPlacementHeight - minPlacementHeight, hitTarget);
        if (hits.Length <= 0) return false; 
        Vector3 position = hits[0].point;
        if (position.y < minPlacementHeight) return false;
        if (position.y > maxPlacementHeight) return false;

        if (hits[0].normal.y < (maxAngle / 90)) return false;

        foreach (Transform resource in this.transform)
        {
            if (Vector3.Distance(position, resource.transform.position) < minRadius) return false;
        }

        GameObject placedResource = Instantiate(resourcePrefabs[Random.Range(0, resourcePrefabs.Length)], position, new Quaternion(0,0,0,0), this.transform);
        placedResource.transform.localScale = Vector3.one * Random.Range(randomSizeRange.x, randomSizeRange.y);
        placedResource.transform.eulerAngles = new Vector3(0, Random.Range(0f, 360f), 0);
        return true;
    }
}
