using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private float maxGrowSize = 2;
    [SerializeField] private float growDuration = 40;

    private float size;
    private float sizePerSec;

    // Start is called before the first frame update
    void Start()
    {
        size = transform.localScale.x;
        sizePerSec = maxGrowSize / (growDuration * 60);
    }

    // Update is called once per frame
    void Update()
    {
        size = Mathf.Clamp(size + sizePerSec * Time.deltaTime, 0, maxGrowSize);
        transform.localScale = Vector3.one * size;
    }
}
