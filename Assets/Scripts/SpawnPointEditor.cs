using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnPointEditor : MonoBehaviour
{
    [SerializeField] float heightUp = 20;
    [SerializeField] float length = 100;
    [SerializeField] LayerMask hitTarget;

    private Vector3 oldPos;

    void Update()
    {
        if (transform.position != oldPos)
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up * heightUp, Vector3.down, length, hitTarget);
            transform.position = hits[0].point + Vector3.up * 0.5f;
        }

        oldPos = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
