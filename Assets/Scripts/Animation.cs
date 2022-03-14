using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterMovement characterMovement;
    
    private bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        UseableItem.onItemUsePrimairy += PrimairyAnimation;
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
                animator.SetBool("Walking", true);
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

            animator.SetFloat("Jump", 1);
        }
        else if (!characterMovement.GroundCheck && !jump)
        {
            jump = true;
            animator.SetFloat("Jump", 2);
        }
        else if (characterMovement.GroundCheck && jump)
        {
            jump = false;
            animator.SetFloat("Jump", 3);
        }
        else animator.SetFloat("Jump", 0);
    }

    private void PrimairyAnimation(UseableItem useableItem)
    {
        animator.SetTrigger("Attack");
    }
}
