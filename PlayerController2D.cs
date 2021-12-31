
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    // References
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    SpriteRenderer flipx;

    // Bool Conditions
    bool IsGrounded;
    bool IsMoving;
    bool IsFacingLeft;
    private bool IsFiringProjectile;
    private bool IsDoingMelee;

    // Movement and projectile references 
    [SerializeField]
    GameObject Projectile_1;
    [SerializeField]
    Transform ProjectileSpawn;
    [SerializeField]
    Transform GroundCheck;
    [SerializeField]
    
    // Float references
    private float MovementSpeed = 1.5f;
    [SerializeField]
    private float JumpSpeed = 3.5f;
    [SerializeField]
    private float FireDelay = .5f;
    [SerializeField]
    private float MeleeDelay = .5f;

    // Start is called before the first frame update
    void Start()
    {
        // On game start
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // If mouse 1 is pressed or right control. Then do the melee animation while we are not grounded
        if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.RightControl))
        {
            if (IsDoingMelee) return;
            if (IsGrounded == false)
            {
                animator.Play("Melee_Animation");
                IsDoingMelee = true;
                Invoke("ResetMelee", MeleeDelay);
            }
        }
        
        // If we are not grounded and mouse 0 is pressed or left control. Then do the jumping projectile animation
        else if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.LeftControl))
        {

            if (IsFiringProjectile) return;
            if (IsGrounded == false)
            {
                animator.Play("Jumping_Projectile_Animation");

                IsFiringProjectile = true;

                Invoke("ResetProjectileFire",FireDelay);

                GameObject Arrow = Instantiate(Projectile_1);
                Arrow.GetComponent<Projectile_1_Script>().OnSpawn(IsFacingLeft);
                Arrow.transform.position = ProjectileSpawn.transform.position;
            }
        }

        // else do the normal jump animation.
        else
        {
            if (IsGrounded == false)

             animator.Play("Jump_Animation");
        }
    }

    // Firing projectile is false
    void ResetProjectileFire()
    {
        IsFiringProjectile = false;
    }

    // Doing melee is false
    void ResetMelee()
    {
        IsDoingMelee = false;
    }

    // Tick functionality
    private void FixedUpdate()
    {
        // Our we on the ground condition
        if (Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("OnGround")))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }

        // Move right functionality
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            // current walk speed
           rb2d.velocity = new Vector2(MovementSpeed, rb2d.velocity.y);
            
            // Grounded is true
            if (IsGrounded)
               
            // Set walk speed
            animator.Play("Walking_Animation");
            spriteRenderer.flipX = false;

            IsFacingLeft = false;
        }

        // Move left functionality
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            // current walk speed
            rb2d.velocity = new Vector2(-MovementSpeed, rb2d.velocity.y);
            
            // Grounded is true
            if (IsGrounded)

            // Set walk speed
            animator.Play("Walking_Animation");
            spriteRenderer.flipX = true;

            IsFacingLeft = true;
        }

        // If mouse 1 is pressed or right control. Then do the melee animation while we are grounded
        else if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.RightControl))
        {
            if (IsDoingMelee) return;
            animator.Play("Melee_Animation");
            IsDoingMelee = true;
            Invoke("ResetMelee", MeleeDelay);
        }

        // If mouse 0 is pressed or left control. Then do the melee animation while we are grounded
        else if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.LeftControl))
        {
            if (IsFiringProjectile) return;
            animator.Play("Idle_Projectile_Animation");
            IsFiringProjectile = true;
            Invoke("ResetProjectileFire", FireDelay);

            GameObject Arrow = Instantiate(Projectile_1);
            Arrow.GetComponent<Projectile_1_Script>().OnSpawn(IsFacingLeft);
            Arrow.transform.position = ProjectileSpawn.transform.position; 
        }

        // Idle functionality. If we are not moving then we are doing the idle animation.
        else
        {
            // Grounded is true
            if (IsGrounded)

            // Set walk speed
            animator.Play("Idling_Animation");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        // Jump functionality. If the space key is pressed then we jump and do the jump animation
        if (Input.GetKey("space") && IsGrounded)
        {
            animator.Play("Jump_Animation");

            rb2d.velocity = new Vector2(rb2d.velocity.x, JumpSpeed);
        }
    }
}

