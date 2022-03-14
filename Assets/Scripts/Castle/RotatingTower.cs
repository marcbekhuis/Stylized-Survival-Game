using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTower : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,rotationSpeed * Time.deltaTime,0));
    }
}
