using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class CharacterMovement : MonoBehaviourPun
{
    [SerializeField] protected float walkSpeed = 10;
    [SerializeField] protected float runSpeed = 10;
    [SerializeField] protected float rotationSpeed = 1;
    [SerializeField] protected AnimationCurve jumpCurve;
    [Header("GroundCheck")]
    [SerializeField] private Vector3 groundCheckOffset = Vector3.zero;
    [SerializeField] private Vector3 groundCheckSize = Vector3.one;
    [SerializeField] private LayerMask groundCheckMask;

    protected CharacterController characterController;
    protected bool applyGravity = true;
    protected bool jumping = false;

    private float airTime = 0;
    private float velocity;
    private MovementType movementType;
    private Vector2 Oldposition;

    public float Velocity { get => velocity; }
    public MovementType GetMovement { get => movementType; }

    public bool GroundCheck
    {
        get => Physics.OverlapBox(transform.position + groundCheckOffset, groundCheckSize * 0.5f, new Quaternion(0, 0, 0, 0), groundCheckMask).Length > 0;
    }

    public bool Jumping { get => jumping; }

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Oldposition = new Vector2(this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GroundCheck)
        {
            airTime = 0;
        }
        else
        {
            airTime += Time.deltaTime;
        }
        if (applyGravity) characterController.Move(GameManager.instance.Gravity(airTime));
    }

    protected virtual void AfterUpdate()
    {
        photonView.RPC("RPCSyncTransform", RpcTarget.Others, transform.position, transform.rotation);
        CalculateVelocity();
    }

    [PunRPC]
    private void RPCSyncTransform(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    protected void Jump()
    {
        if (jumping) return;
        if (!GroundCheck) return;
        StartCoroutine(JumpEnum());
    }

    protected IEnumerator JumpEnum()
    {
        airTime = 0;
        jumping = true;
        applyGravity = false;
        for (float i = 0; i < jumpCurve.keys.Last().time; i += Time.deltaTime)
        {
            characterController.Move(transform.rotation * (Vector3.up * jumpCurve.Evaluate(i)) * Time.deltaTime);
            yield return null;
        }
        applyGravity = true;
        jumping = false;
    }

    private void CalculateVelocity()
    {
        Vector2 flatNewPos = new Vector2(this.transform.position.y, this.transform.position.z);
        velocity = Vector2.Distance(flatNewPos, Oldposition);
        if(velocity < walkSpeed * Time.deltaTime)
        {
            movementType = MovementType.Idle;
        }
        else if(velocity < runSpeed * Time.deltaTime)
        {
            movementType = MovementType.Walk;
        }
        else
        {
            movementType = MovementType.Run;
        }

        Oldposition = flatNewPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + groundCheckOffset, groundCheckSize);
    }

    public enum MovementType {Idle, Walk, Run}
}
