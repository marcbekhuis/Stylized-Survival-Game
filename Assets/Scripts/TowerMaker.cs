using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TowerMaker : MonoBehaviour
{
    [SerializeField] private bool update = true;
    [Space]
    [SerializeField] private int towerSecments = 3;
    [SerializeField] private int foundationSecments = 3;
    [SerializeField] private float towerSecmentHeight = 6;
    [SerializeField] [Range(0, 100)] private int windowChance = 50;
    [SerializeField] private int rotationSteps = 90;
    [Space]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject towerDoorPrefab;
    [SerializeField] private GameObject towerWindowPrefab;
    [SerializeField] private GameObject towerFoundationPrefab;
    [SerializeField] private GameObject roofPrefab;
    [SerializeField] private GameObject trimPrefab;

    // Update is called once per frame
    void Update()
    {
        if (!update) return;

        foreach (Transform child in this.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int i = 0; i < towerSecments; i++)
        {
            GameObject towerSecment;
            if (i == 0)
                towerSecment = Instantiate(towerDoorPrefab, this.transform);
            else
            {
                if (Random.Range(0, 100) < windowChance)
                {
                    towerSecment = Instantiate(towerWindowPrefab, this.transform);
                    towerSecment.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360/rotationSteps) * rotationSteps, 0);
                }
                else
                {
                    towerSecment = Instantiate(towerPrefab, this.transform);
                }
            }

            towerSecment.transform.localPosition = new Vector3(0,towerSecmentHeight * i,0);
        }

        for (int i = -1; i >= -foundationSecments; i--)
        {
            GameObject towerSecment = Instantiate(towerFoundationPrefab, this.transform);
            towerSecment.transform.localPosition = new Vector3(0, towerSecmentHeight * i, 0);
        }

        GameObject roof = Instantiate(roofPrefab, this.transform);
        roof.transform.localPosition = new Vector3(0, towerSecmentHeight * towerSecments + 0.1f, 0);

        GameObject trim = Instantiate(trimPrefab, this.transform);
        trim.transform.localPosition = new Vector3(0, towerSecmentHeight * towerSecments, 0);

        update = false;
    }
}
