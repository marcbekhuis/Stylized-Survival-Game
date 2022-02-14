using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(SphereCollider))]
public class AIMovement : CharacterMovement
{
    [SerializeField] private SphereCollider sc;
    [SerializeField] private Transform target;
    [SerializeField] private float range = 10.0f;
    [SerializeField] private float timer = 10.0f;
    [SerializeField] private Vector3 point;
    private NavMeshPath path;

    [SerializeField] private float colliderRadius;
    // Start is called before the first frame update
    void Start()
    {
        sc = this.GetComponent<SphereCollider>();
        //point = this.transform.GetChild(0).gameObject;
        sc.radius = colliderRadius;
        path = new NavMeshPath();
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
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
        timer -= Time.deltaTime;
        if (target != null)
        {
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            transform.LookAt(path.corners[1]);
            characterController.Move(transform.rotation * Vector3.forward * speed * Time.deltaTime);
        }
        if (target == null)
        {
            //if (RandomPoint(transform.position, range, out point))
            //{
            //    Debug.DrawRay(point, Vector3.up, Color.blue, 10.0f);
            //    transform.LookAt(point);
            //}
            characterController.Move(transform.rotation * Vector3.forward * speed * Time.deltaTime);
        }

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
    }
}
