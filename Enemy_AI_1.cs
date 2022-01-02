using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_1 : MonoBehaviour
{
    // Reference to our player
    [SerializeField]
    Transform Player;

    //float values
    [SerializeField]
    float AIRange;

    [SerializeField]
    float MovementSpeed;

    // Int values
    private int EnemyHealth = 3;

    // References
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    SpriteRenderer flipx;

    // Start is called before the first frame update
    void Start()
    {
        // On game start
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get distance to player
        float DistanceToPlayer = Vector2.Distance(transform.position, Player.position);

        // If our player is wihin the AIRange, then move towards the player
        if(DistanceToPlayer < AIRange)
        {

            AIMoveTo();
        }
        // if not then stop
        else
        {
            StopAIMoveTo();
        }
    }
    
    // If we stop moving then play the idel animation
    void StopAIMoveTo()
    {
        // Set Animation
        animator.Play("Idling_Animation");
        rb2d.velocity = new Vector2(0, 0);
    }
    
    void AIMoveTo()
    {
        if (transform.position.x < Player.position.x)
        {
            // current walk speed
            rb2d.velocity = new Vector2(MovementSpeed, 0);

            // Set walk animation
            animator.Play("Walking_Animation");

            spriteRenderer.flipX = false;
        }
        else
        {
            // current walk speed
            rb2d.velocity = new Vector2(-MovementSpeed, 0);

            // Set walk animation
            animator.Play("Walking_Animation");

            spriteRenderer.flipX = true;
        }
    }
    
    // If this actor collides with this actor then remove the set amount of health
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile_1"))
        {
            // Destroy other actor
            Destroy(collision.gameObject);

            EnemyHealth--;

            // if the health drops below 0 then destroy this actor
            if (EnemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
