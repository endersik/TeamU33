using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnim;

    public float moveSpeed = 4f;
    public float jumpSpeed = 10f, jumpFrequency = 1f, nextJumpTime;
    public bool isGrounded = false;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;

    bool facingRight = true;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        HorizontalMove();
        OnGroundCheck();

        if (playerRB.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }
        else if (playerRB.velocity.x < 0 && facingRight)
        {
            FlipFace();
        }

        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJumpTime < Time.timeSinceLevelLoad))
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();
        }

    }

    void HorizontalMove()
    {
        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
        playerAnim.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
    }

    void Jump()
    {
        playerRB.AddForce(transform.up * jumpSpeed);
    }

    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer);
        playerAnim.SetBool("isGroundedAnim", isGrounded);
    }

    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }
}
