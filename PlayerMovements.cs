using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] int maxJumps = 2;
    [SerializeField] Collider2D myBodyCollider;
    [SerializeField] Vector2 deathKick = new Vector2(30f, 30f); 
    [SerializeField] GameObject Sword;
    [SerializeField] Transform handle;

    
    // Component references
    Rigidbody2D rb; // Consistent naming (lowercase rb)
    Animator animator; // Consistent naming
    
    // State variables
    Vector2 moveInput;
    bool isFacingRight = true;
    int jumpsRemaining;
    bool isAlive = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpsRemaining = maxJumps;
    }

    void OnMove(InputValue value)
    {
        if(!isAlive) return;
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!isAlive) return;
        if (value.isPressed && jumpsRemaining > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsRemaining--;
            animator.SetBool("IsJumping", true);
        }
    }

    void Update()
    {
        if (!isAlive)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Flip character
        if (moveInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            Flip();
        }

        // Ground check
        if (Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            jumpsRemaining = maxJumps;
            animator.SetBool("IsJumping", false);
        }

        // Movement
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        animator.SetBool("IsRunning", Mathf.Abs(moveInput.x) > 0.1f);

        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazard")))
        {
            Die();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Die()
    {
        isAlive = false;
        moveInput = Vector2.zero;
        rb.velocity = deathKick;
        animator.SetTrigger("Die");
        FindObjectOfType<GameSession>().PlayerDeath();
    }
    void OnFire (InputValue value){
        if(!isAlive){return;}
        Instantiate(Sword,handle.position,transform.rotation);
    }
}