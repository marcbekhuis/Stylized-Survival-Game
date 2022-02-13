using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterMovement : MonoBehaviourPun
{
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float jumpSpeed = 10;
    [SerializeField] protected float jumpDuration = 10;
    [SerializeField] protected float rotationSpeed = 1;
    [Header("GroundCheck")]
    [SerializeField] private Vector3 groundCheckOffset = Vector3.zero;
    [SerializeField] private Vector3 groundCheckSize = Vector3.one;
    [SerializeField] private LayerMask groundCheckMask;

    protected CharacterController characterController;
    protected bool applyGravity = true;
    protected bool jumping = false;

    private float airTime = 0;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GroundCheck())
        {
            airTime = 0;
        }
        else
        {
            airTime += Time.deltaTime;
        }
        if (applyGravity) characterController.Move(GameManager.instance.Gravity(airTime));
    }

    protected bool GroundCheck()
    {
        return Physics.OverlapBox(transform.position + groundCheckOffset, groundCheckSize * 0.5f, new Quaternion(0, 0, 0, 0), groundCheckMask).Length > 0;
    }

    protected virtual void AfterUpdate()
    {
        photonView.RPC("RPCSyncTransform", RpcTarget.Others, transform.position, transform.rotation);
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
        StartCoroutine(JumpEnum());
    }

    protected IEnumerator JumpEnum()
    {
        jumping = true;
        applyGravity = false;
        for (float i = 0; i < jumpDuration; i += Time.deltaTime)
        {
            characterController.Move(transform.rotation * (Vector3.up * jumpSpeed) * Time.deltaTime);
            yield return null;
        }
        applyGravity = true;
        jumping = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + groundCheckOffset, groundCheckSize);
    }
}
