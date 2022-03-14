using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BuildingMaker : MonoBehaviour
{
    [SerializeField] private bool update = true;
    [Space]
    [SerializeField] private Vector2Int size = Vector2Int.one;
    [SerializeField] private int buildingSecments = 3;
    [SerializeField] private int foundationSecments = 3;
    [SerializeField] private float floorSecmentHeight = 6;
    [SerializeField] private float wallWidth = 8;
    [SerializeField] [Range(0, 100)] private int windowChance = 50;
    [Space]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject wallDoorPrefab;
    [SerializeField] private GameObject wallWindowPrefab;
    [SerializeField] private GameObject buildingFoundationPrefab;
    [SerializeField] private GameObject roofPrefab;
    [SerializeField] private GameObject roofTrimPrefab;

    // Update is called once per frame
    void Update()
    {
        if (!update) return;

        foreach (Transform child in this.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        float startPointX = wallWidth * size.x / 2;
        float startPointZ = wallWidth * size.y / 2;
        float halfWall = wallWidth / 2;
        for (int y = 0; y < buildingSecments; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                GameObject prefab = wallPrefab;
                if (Random.Range(0, 100) < windowChance)
                    prefab = wallWindowPrefab;

                GameObject towerSecment = Instantiate(prefab, this.transform);
                towerSecment.transform.localPosition = new Vector3(x * wallWidth - startPointX + halfWall, floorSecmentHeight * y, startPointZ - halfWall);
                towerSecment.transform.localEulerAngles = new Vector3(prefab.transform.eulerAngles.x, 180, prefab.transform.eulerAngles.z);

                prefab = wallPrefab;
                if (Random.Range(0, 100) < windowChance)
                    prefab = wallWindowPrefab;

                towerSecment = Instantiate(prefab, this.transform);
                towerSecment.transform.localPosition = new Vector3(x * wallWidth - startPointX + halfWall, floorSecmentHeight * y, -startPointZ + halfWall);
            }

            for (int z = 0; z < size.y; z++)
            {
                GameObject prefab = wallPrefab;
                if (Random.Range(0, 100) < windowChance)
                    prefab = wallWindowPrefab;

                GameObject towerSecment = Instantiate(prefab, this.transform);
                towerSecment.transform.localPosition = new Vector3(startPointX - halfWall, floorSecmentHeight * y, z * wallWidth - startPointZ + halfWall);
                towerSecment.transform.localEulerAngles = new Vector3(prefab.transform.eulerAngles.x, -90, prefab.transform.eulerAngles.z);

                prefab = wallPrefab;
                if (Random.Range(0, 100) < windowChance)
                    prefab = wallWindowPrefab;

                towerSecment = Instantiate(prefab, this.transform);
                towerSecment.transform.localPosition = new Vector3(-startPointX + halfWall, floorSecmentHeight * y, z * wallWidth - startPointZ + halfWall);
                towerSecment.transform.localEulerAngles = new Vector3(prefab.transform.eulerAngles.x,90, prefab.transform.eulerAngles.z);
            }
        }

        for (int i = -1; i >= -foundationSecments; i--)
        {
            GameObject towerSecment = Instantiate(buildingFoundationPrefab, this.transform);
            towerSecment.transform.localPosition = new Vector3(0, floorSecmentHeight * i, 0);
            towerSecment.transform.localScale = new Vector3(size.x,1,size.y);
        }

        GameObject roof = Instantiate(roofPrefab, this.transform);
        roof.transform.localPosition = new Vector3(0, floorSecmentHeight * buildingSecments + 0.1f, 0);
        roof.transform.localScale = new Vector3(size.x * 100, size.y * 100, 100);

        GameObject trim = Instantiate(roofTrimPrefab, this.transform);
        trim.transform.localPosition = new Vector3(0, floorSecmentHeight * buildingSecments, 0);
        trim.transform.localScale = new Vector3(size.x * 100, size.y * 100, 100);

        update = false;
    }
}
