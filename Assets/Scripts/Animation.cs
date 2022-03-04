using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private CharacterController cc;
    [SerializeField] private CharacterMovement cm;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        cc = GetComponent<CharacterController>();
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, lastPos);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    anim.SetInteger("Jump", 1);
        //}
        if (distance > 0.03)
        {
            anim.SetBool("Walk", true);
        }
        if (distance < 0.03)
        {
            anim.SetBool("Walk", false);
        }

        lastPos = transform.position;
    }
}
