using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : CharacterMovement
{

    // Update is called once per frame
    protected override void Update()
    {
        if (!photonView.IsMine) return;
        base.Update();
        Movement();
        photonView.RPC("RPCSyncPosition", RpcTarget.Others, transform.position);
    }

    private void Movement()
    {
        Vector3 movementDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            movementDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            movementDirection += Vector3.back;
        if (Input.GetKey(KeyCode.A))
            movementDirection += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            movementDirection += Vector3.right;
        if (Input.GetKey(KeyCode.Space))
            movementDirection += Vector3.up;

        characterController.Move(movementDirection * Time.deltaTime);
    }

    [PunRPC]
    private void RPCSyncPosition(Vector3 position)
    {
        transform.position = position;
    }
}
