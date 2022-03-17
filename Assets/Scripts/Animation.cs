using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Animation : MonoBehaviourPun
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private AIMeleeAttack aiMeleeAttack;

    private bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        if(photonView.IsMine && isPlayer)
            UseableItem.onItemUsePrimairy += PrimairyAnimation;
        else if(aiMeleeAttack)
        {
            aiMeleeAttack.onAttack += AttackAnimation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (characterMovement.GetMovement)
        {
            case CharacterMovement.MovementType.Idle:
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
                break;
            case CharacterMovement.MovementType.Walk:
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                animator.SetBool("Idle", false);
                break;
            case CharacterMovement.MovementType.Run:
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                animator.SetBool("Idle", false);
                break;
            default:
                break;
        }

        if (characterMovement.Jumping)
        {

            animator.SetInteger("Jump", 1);
        }
        else if (!characterMovement.GroundCheck && !jump)
        {
            jump = true;
            animator.SetInteger("Jump", 2);
        }
        else if (characterMovement.GroundCheck && jump)
        {
            jump = false;
            animator.SetInteger("Jump", 3);
        }
        else animator.SetInteger("Jump", 0);
    }

    private void PrimairyAnimation(UseableItem useableItem)
    {
        animator.SetTrigger("Attack");
    }

    private void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}
