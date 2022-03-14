using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BridgeMaker : MonoBehaviour
{
    [SerializeField] private bool update = true;
    [SerializeField] private bool hasPillars = true;
    [SerializeField] private float pillarHeight = 6;
    [SerializeField] private float additionalPillars = 0;
    [SerializeField] private float maxFloorCheck = 100;
    [SerializeField] private GameObject bridgePrefab;
    [SerializeField] private GameObject bridgePillarPrefab;
    [SerializeField] private LayerMask targetMask;

    // Update is called once per frame
    void Update()
    {
        if (!update) return;

        foreach (Transform child in this.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        GameObject bridge = Instantiate(bridgePrefab, this.transform);
        bridge.transform.localPosition = Vector3.zero;

        if (!hasPillars) return;

        RaycastHit[] raycastHitsRight = Physics.RaycastAll(this.transform.position + this.transform.rotation * new Vector3(0, 0, 10), Vector3.down, maxFloorCheck, targetMask);
        RaycastHit[] raycastHitsLeft = Physics.RaycastAll(this.transform.position + this.transform.rotation * new Vector3(0, 0, -10), Vector3.down, maxFloorCheck, targetMask);

        float distanceFloor = 0;
        if (raycastHitsRight.Length > 0) distanceFloor = raycastHitsRight[0].distance;
        if (raycastHitsLeft.Length > 0)
        {
            distanceFloor = raycastHitsLeft[0].distance;
            if (distanceFloor < raycastHitsLeft[0].distance) distanceFloor = raycastHitsLeft[0].distance;
        }

        int pillars = Mathf.CeilToInt((distanceFloor / pillarHeight) / transform.localScale.x);
        for (int i = -1; i >= -pillars - additionalPillars; i--)
        {
            GameObject pillar = Instantiate(bridgePillarPrefab, this.transform);
            pillar.transform.localPosition = new Vector3(0, pillarHeight * i, 0);
        }

        update = false;
    }
}
