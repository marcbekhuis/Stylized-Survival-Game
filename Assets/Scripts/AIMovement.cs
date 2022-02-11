using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(SphereCollider))]
public class AIMovement : CharacterMovement
{
    [SerializeField] public SphereCollider sc;
    public Transform target;
    private NavMeshPath path;
    // Start is called before the first frame update
    void Start()
    {
        sc = this.GetComponent<SphereCollider>();
        sc.radius = 3;
        sc.isTrigger = true;
        path = new NavMeshPath();
    }

    void Update()
    {
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
    
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.Log(path.corners[i + 1]);
            characterController.transform.position = path.corners[i + 1];
        }
        Debug.Log(characterController);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }
}
