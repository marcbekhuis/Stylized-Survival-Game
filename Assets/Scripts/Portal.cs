using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private LayerMask targetLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (other.gameObject.layer | (1 << targetLayer)))
            if (destination)
                other.transform.position = destination.position;
    }
}
