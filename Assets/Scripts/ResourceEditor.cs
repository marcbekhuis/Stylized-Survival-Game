using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ResourceEditor : MonoBehaviour
{
    [SerializeField] private bool refresh = true;
    [SerializeField] private Vector2 randomSizeRange = new Vector2(0.5f, 1.5f);
    [Header("Placement")]
    [SerializeField] float heightUp = 20;
    [SerializeField] float length = 100;
    [SerializeField] LayerMask hitTarget;

    private Vector3 oldPos;

    void Update()
    {
        if (refresh)
        {
            transform.localScale = Vector3.one * Random.Range(randomSizeRange.x, randomSizeRange.y);
            transform.eulerAngles = new Vector3(0, Random.Range(0f,360f),0);
            refresh = false;
        }

        if (transform.position != oldPos)
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up * heightUp, Vector3.down, length, hitTarget);
            transform.position = hits[0].point;
        }

        oldPos = transform.position;
    }
}
