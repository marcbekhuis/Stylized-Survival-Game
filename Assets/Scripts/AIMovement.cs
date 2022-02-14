using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(SphereCollider))]
public class AIMovement : CharacterMovement
{
    [SerializeField] private SphereCollider sc;
    public Transform target;
    private NavMeshPath path;
    [SerializeField] private float colliderRadius;
    // Start is called before the first frame update
    void Start()
    {
        sc = this.GetComponent<SphereCollider>();
        sc.radius = colliderRadius;
        path = new NavMeshPath();
    }

    void Update()
    {
        base.Update();
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);

        transform.LookAt(path.corners[1]);
        characterController.Move(transform.rotation * Vector3.forward * speed * Time.deltaTime);
        base.AfterUpdate();
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
