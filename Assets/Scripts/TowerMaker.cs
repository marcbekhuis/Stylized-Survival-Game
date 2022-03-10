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
    [Space]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject roofPrefab;
    [SerializeField] private GameObject trimPrefab;

    // Update is called once per frame
    void Update()
    {
        if (!update) return;

        foreach (Transform child in this.transform)
        {
            Destroy(child);
        }


        update = true;
    }
}
