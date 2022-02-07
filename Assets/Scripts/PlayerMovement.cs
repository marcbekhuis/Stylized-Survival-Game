using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : CharacterMovement
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private float jumpDuration = 10;
    [SerializeField] private float rotationSpeed = 1;

    private bool jump = false;
    private bool Jump { get => jump; set 
        {
            jump = value;
            applyGravity = !value;
        } 
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!photonView.IsMine) return;
        base.Update();
        if (!GameManager.instance.playerGameInputBlocked)
        {
            Movement();
            YawRotation();
        }
        photonView.RPC("RPCSyncPosition", RpcTarget.Others, transform.position);
        photonView.RPC("RPCSyncRotation", RpcTarget.Others, transform.rotation);
    }

    private void YawRotation()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime,0));
    }

    private void Movement()
    {
        Vector3 movementDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            movementDirection += Vector3.forward * speed;
        if (Input.GetKey(KeyCode.S))
            movementDirection += Vector3.back * speed;
        if (Input.GetKey(KeyCode.A))
            movementDirection += Vector3.left * speed;
        if (Input.GetKey(KeyCode.D))
            movementDirection += Vector3.right * speed;
        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
            StartCoroutine(JumpEnum());

        characterController.Move(transform.rotation * movementDirection * Time.deltaTime);
    }

    private IEnumerator JumpEnum()
    {
        Jump = true;
        for (float i = 0; i < jumpDuration; i += Time.deltaTime)
        {
            characterController.Move(transform.rotation * (Vector3.up * jumpSpeed) * Time.deltaTime);
            yield return null;
        }
        Jump = false;
    }

    [PunRPC]
    private void RPCSyncPosition(Vector3 position)
    {
        transform.position = position;
    }

    [PunRPC]
    private void RPCSyncRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
