using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private LayerMask targetLayer;

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("fuiwqhyifu8weqgyfuweq8dfhmyuwe7qifhduewiufhduei9ufheidfheh8ii");
        other.transform.position = destination.position;
    }
}
