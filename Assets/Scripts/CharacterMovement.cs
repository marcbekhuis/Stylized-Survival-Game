using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterMovement : MonoBehaviourPun
{
    protected CharacterController characterController;
    protected bool applyGravity = true;

    private float airTime = 0;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (characterController.isGrounded)
        {
            airTime = 0;
        }
        else
        {
            if (applyGravity) characterController.Move(GameManager.instance.Gravity(airTime));
            airTime += Time.deltaTime;
        }
    }
}
