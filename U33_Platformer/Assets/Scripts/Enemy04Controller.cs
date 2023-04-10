using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04Controller : MonoBehaviour
{
    Rigidbody2D enemy04Body2D;

    public float enemy04Speed;

    //Duvarý Bulma
    [Tooltip("karakterin duvarý bulup bulmadýðýný kontrol eder.")]
    public bool isGrounded;
    Transform groundCheck;
    const float groundCheckRadius = 0.02f;
    [Tooltip("Duvarýn ne olduðunu belirler.")]
    public LayerMask groundLayer;
    private Vector3 newScale;
    public bool moveRight;
    public float distanceToAttack=3;
    public float distanceToFollow = 15;
    public bool characterInFront = false;
    private Animator enemyAnimator;
    public GameObject player;
    public float attackInterval;
    private float timeOfLastAttack=10f;
    // Start is called before the first frame update
    void Start()
    {
        enemy04Body2D = GetComponent<Rigidbody2D>();
        enemyAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy04Body2D.velocity = new Vector2(enemy04Speed, 0);
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) < distanceToFollow)
        {
            FollowState();
        }
        else
        {
            PatrolState();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlatformEdge")
        {
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlatformEdge")
        {
            Flip();
        }
        if(collision.gameObject.tag == "Character_Attack_Hitbox")
        {
            enemyAnimator.SetTrigger("hit");
        }
    }


    void Flip()
    {
        enemy04Speed *= -1;

        newScale = new Vector3(gameObject.transform.localScale.x * (-1), gameObject.transform.localScale.y, 1);
        gameObject.transform.localScale = newScale;
    }

    void AttackState()
    { 
        if ((Time.time - timeOfLastAttack) > attackInterval)
        {
            enemyAnimator.SetBool("Attack", true);
            timeOfLastAttack = Time.time;
            //Do Damage
        }
        else
        {
            enemyAnimator.SetBool("Attack", false);
        }
    }

    void FollowState()
    {
        if ((gameObject.transform.position.x - player.transform.position.x) < 0 && enemy04Body2D.transform.localScale.x > 0)
        {
            if (distanceToAttack > Vector2.Distance(transform.position, player.transform.position))
            {
                AttackState();
            }
        }
        else if ((gameObject.transform.position.x - player.transform.position.x) > 0 && enemy04Body2D.transform.localScale.x < 0)
        {
            if (distanceToAttack > Vector2.Distance(transform.position, player.transform.position))
            {
                AttackState();
            }
        }
        else
        {
            Flip();
        }
    }

    private void PatrolState()
    {
        enemyAnimator.SetBool("Attack", false);
    }
}
