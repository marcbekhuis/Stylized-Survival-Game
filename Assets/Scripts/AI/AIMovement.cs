using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AIMovement : CharacterMovement
{
    [SerializeField] private float radius = 10.0f;
    [SerializeField] private float cornerAcceptanceRange = 1f;
    [SerializeField] private float targetAcceptanceRange = 1f;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private Transform headOrigin;
    [SerializeField] private float minRandomPointHeight = 0;
    [SerializeField] private float maxRandomPointHeight = 500;
    [SerializeField] private int agentID = 0;

    [SerializeField] private bool showDebuging;

    private Transform target;
    private Vector3 point;
    private NavMeshPath path;

    void Start()
    {
        path = new NavMeshPath();
        lookAtPoint();
    }

    void Update()
    {
        if (isTeleporting)
        {
            isTeleporting = false;
            return;
        }
        base.Update();

        FindTarget();

        if (target)
        {
            NavMeshQueryFilter agentFilter = new NavMeshQueryFilter();
            agentFilter.agentTypeID = agentID;
            agentFilter.areaMask = NavMesh.AllAreas;
            NavMesh.CalculatePath(transform.position, target.position, agentFilter, path);
            if (path.corners.Length > 1)
            {
                if (Vector3.Distance(transform.position, path.corners[1]) > cornerAcceptanceRange)
                {
                    transform.LookAt(new Vector3(path.corners[1].x, this.transform.position.y, path.corners[1].z));
                }
            }
            else
            {
                transform.LookAt(new Vector3(target.position.x, this.transform.position.y, target.position.z));
            }
        }
        else
        {
            if (Vector3.Distance(point, transform.position) < cornerAcceptanceRange)
            {
                lookAtPoint();
            }
        }

        Move();

        base.AfterUpdate();
    }

    private void Move()
    {
        RaycastHit[] raycastHits = Physics.RaycastAll(headOrigin.position, this.transform.rotation * Vector3.forward, radius);

        if(raycastHits.Length > 0)
        {
            foreach (var raycast in raycastHits)
            {
                if(raycast.transform != this.transform)
                {
                    if (raycast.distance < targetAcceptanceRange)
                        return;
                }
            }
        }
        
        characterController.Move(transform.rotation * Vector3.forward * walkSpeed * Time.deltaTime);
    }

    private void FindTarget()
    {
        Collider[] targets = Physics.OverlapSphere(this.transform.position, radius, targetLayers);

        Transform closestTarget = null;
        foreach (var item in targets)
        {
            if (!closestTarget)
            {
                closestTarget = item.transform;
                continue;
            }
            if(Vector3.Distance(this.transform.position, item.transform.position) < Vector3.Distance(this.transform.position, closestTarget.position))
            {
                closestTarget = item.transform;
            }
        }

        if (target && !closestTarget) lookAtPoint();
        target = closestTarget;
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10, NavMesh.AllAreas))
            {
                result = hit.position;
                if (result.y > minRandomPointHeight && result.y < maxRandomPointHeight)
                    return true;
            }        
        }
        result = Vector3.zero;
        return false;
    }

    private void lookAtPoint()
    {
        if (RandomPoint(transform.position, radius, out point))
        {
            transform.LookAt(new Vector3(point.x, this.transform.position.y, point.z));
        }
    }

    private void OnDrawGizmos()
    {
        if (!showDebuging) return;

        Gizmos.color = new Color(0,0,1,0.5f);
        Gizmos.DrawSphere(this.transform.position, radius);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(this.transform.position, targetAcceptanceRange);
    }
}
