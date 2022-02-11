using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class AIMovement : CharacterMovement
{
    [SerializeField] public SphereCollider sc;
    // Start is called before the first frame update
    void Start()
    {
        sc = this.GetComponent<SphereCollider>();
        sc.radius = 3;
        sc.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
