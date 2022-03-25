using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private LayerMask targetLayer;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponent<CharacterMovement>().isTeleporting = true;
        other.transform.position = destination.position;
    }
}