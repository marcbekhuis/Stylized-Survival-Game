using UnityEngine;
using UnityEngine.AI;
using System.Collections;
[RequireComponent(typeof(SphereCollider))]
public class AIMovement : CharacterMovement
{
    [SerializeField] private SphereCollider sc;
    [SerializeField] private Transform target;
    [SerializeField] private float range = 10.0f;
    [SerializeField] private float timer = 10.0f;
    [SerializeField] private float acceptanceRange = 1f;
    [SerializeField] private Vector3 point;
    private NavMeshPath path;

    void Start()
    {
        sc = this.GetComponent<SphereCollider>();
        path = new NavMeshPath();
        lookAtPoint();
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    void Update()
    {
        base.Update();

        if (target != null)
        {
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            Debug.LogError(path.corners[1]);
            if(Vector3.Distance(transform.position, path.corners[1]) > 0.3) 
            {
                transform.LookAt(path.corners[1]);
            }
        }
        else
        {
            if(Vector3.Distance(point, transform.position) < acceptanceRange)
            {
                lookAtPoint();
            }
        }
        characterController.Move(transform.rotation * Vector3.forward * speed * Time.deltaTime);
        
        base.AfterUpdate();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
        lookAtPoint();
    }

    private void lookAtPoint()
    {
        if (RandomPoint(transform.position, range, out point))
        {
            transform.LookAt(point);
        }
    }
}
