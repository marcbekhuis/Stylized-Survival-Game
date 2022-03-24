using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ResourceSpawnerEditor : MonoBehaviour
{
    [SerializeField] private bool replaceResources = false;
    [SerializeField] private Vector2 resourceFieldX = new Vector2(-1000, 1000);
    [SerializeField] private Vector2 resourceFieldZ = new Vector2(-1000, 1000);
    [SerializeField] private ResourceEntry[] resourceEntries;
    [SerializeField] LayerMask hitTarget;

    [System.Serializable]
    public class ResourceEntry 
    {
        public float minRadius = 1;
        [Range(0, 90)] public float maxAngle = 45;
        public int numberOfResources = 1000;
        public Vector2 randomSizeRange = new Vector2(0.5f, 1.5f);
        public Vector3 offset;
        public float minPlacementHeight = 0;
        public float maxPlacementHeight = 500;
        public bool randomRotation = false;
        public GameObject resourcePrefab;
    }


    // Update is called once per frame
    void Update()
    {
        if (!replaceResources) return;
        replaceResources = false;
        foreach (Transform resource in this.transform)
        {
            DestroyImmediate(resource.gameObject);
        }

        foreach (var resourceEntry in resourceEntries)
        {
            if (!resourceEntry.resourcePrefab) continue;
            if (resourceEntry.minPlacementHeight == resourceEntry.maxPlacementHeight) continue;
            if (resourceEntry.randomSizeRange.x < 0 && resourceEntry.randomSizeRange.y < 0) continue;
            for (int i = 0; i < resourceEntry.numberOfResources; i++)
            {
                if (!PlaceResource(resourceEntry)) i--;
            }
        }
    }

    private bool PlaceResource(ResourceEntry resourceEntry)
    {
        Vector2 positionNoHeight = new Vector2(Random.Range(resourceFieldX.x, resourceFieldX.y), Random.Range(resourceFieldZ.x, resourceFieldZ.y));
        RaycastHit[] hits = Physics.RaycastAll(new Vector3(positionNoHeight.x, resourceEntry.maxPlacementHeight, positionNoHeight.y), Vector3.down, resourceEntry.maxPlacementHeight - resourceEntry.minPlacementHeight, hitTarget);
        if (hits.Length <= 0) return false; 
        Vector3 position = hits[0].point;
        if (position.y < resourceEntry.minPlacementHeight) return false;
        if (position.y > resourceEntry.maxPlacementHeight) return false;

        if (hits[0].normal.y < (resourceEntry.maxAngle / 90)) return false;

        foreach (Transform resource in this.transform)
        {
            if (Vector3.Distance(position, resource.transform.position) < resourceEntry.minRadius) return false;
        }

        float scale = Random.Range(resourceEntry.randomSizeRange.x, resourceEntry.randomSizeRange.y);
        GameObject placedResource = Instantiate(resourceEntry.resourcePrefab, position + resourceEntry.offset * scale, new Quaternion(0,0,0,0), this.transform);
        placedResource.transform.localScale = Vector3.one * scale;
        if(resourceEntry.randomRotation)
            placedResource.transform.eulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        else
            placedResource.transform.eulerAngles = new Vector3(0, Random.Range(0f, 360f), 0);
        return true;
    }
}
