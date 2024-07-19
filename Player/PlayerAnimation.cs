using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    float posY;
    float speed;
    bool isJumping;
    bool isGrounded, isBall;
    PlayerMovement playerMovement;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {


        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerValues();
        SetAnimationValues();
        CheckSpeed();
    }
    void GetPlayerValues()
    {
        posY = playerMovement.posY;
        speed = playerMovement.forwardSpeed;
        isJumping = playerMovement.isJumping;
        isGrounded = playerMovement.isGrounded;
        isBall = playerMovement.isBall;
    }
    void SetAnimationValues()
    {
        animator.SetFloat("speed", speed);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("PosY", posY);
        animator.SetBool("isBall", isBall);

    }
    void CheckSpeed()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
        animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            animator.speed = speed / playerMovement.initialSpeed; // Ajusta la velocidad de la animación
        }
        else
        {
            animator.speed = 1;
        }
    }

}

