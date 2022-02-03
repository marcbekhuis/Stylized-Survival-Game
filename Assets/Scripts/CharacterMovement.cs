using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterMovement : MonoBehaviourPun
{
    protected CharacterController characterController;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        characterController.Move(GameManager.instance.Gravity);
    }
}
