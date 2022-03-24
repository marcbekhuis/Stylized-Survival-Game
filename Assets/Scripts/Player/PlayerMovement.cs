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
        if (isTeleporting) {
            isTeleporting = false;
            return;
        }
        if (!photonView.IsMine) return;
        base.Update();
        if (!GameManager.instance.playerGameInputBlocked)
        {
            Movement();
            YawRotation();
        }
        base.AfterUpdate();
    }

    private void YawRotation()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime,0));
    }

    private void Movement()
    {
        Vector3 movementDirection = Vector3.zero;

        float speed = walkSpeed;
        if (InputManager.instance.GetKey(InputActions.Sprint)) speed = runSpeed;
        movementDirection += Vector3.forward * InputManager.instance.GetAxis(InputAxis.Vertical) * speed;
        movementDirection += Vector3.right * InputManager.instance.GetAxis(InputAxis.Horizontal) * speed;

        if (InputManager.instance.GetKey(InputActions.Jump))
            Jump();

        characterController.Move(transform.rotation * movementDirection * Time.deltaTime);
    }
}
