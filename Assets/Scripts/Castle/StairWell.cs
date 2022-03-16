using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StairWell : MonoBehaviour
{
    [SerializeField] private bool update = true;
    [SerializeField] private int stairs = 1;
    [SerializeField] private float stairRotation = 120;
    [SerializeField] private float stairHeight = 1.2f;
    [SerializeField] private GameObject stairPrefab;

    // Update is called once per frame
    void Update()
    {
        if (!update) return;

        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int i = 0; i < stairs; i++)
        {
            GameObject stair = Instantiate(stairPrefab, this.transform);
            stair.transform.localPosition = new Vector3(0, stairHeight * i, 0);
            stair.transform.localEulerAngles = new Vector3(stairPrefab.transform.localEulerAngles.x, stairRotation * i, stairPrefab.transform.localEulerAngles.z);
        }

        update = false;
    }
}
