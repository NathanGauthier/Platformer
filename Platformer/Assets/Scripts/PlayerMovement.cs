using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float climbSpeed;
    float inputX;
    float inputY;

    float initialgravity;

    bool playerHasHorizontalMovement;

    bool isAlive = true;

    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    public GameObject bullet;
    public Transform bulletSpawnPos;
    
    
    void Start()
    {       
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
         
        initialgravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive)
        {
            return;
        }
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Run();
        FlipSprite();
        Jump();
        Climb();
        Fire();
        Die();
    }

    private void Run()
    {
        
        Vector2 playerVelocity = new Vector2(inputX * speed, rb.velocity.y);
        rb.velocity = playerVelocity;        
        animator.SetBool("isRunning", playerHasHorizontalMovement);
    }

    private void Jump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void Climb()
    {
        

        if (!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = initialgravity;
            animator.SetBool("isClimbing", false);
            return;
        }

        animator.SetBool("isRunning", false);
        Vector2 climbVelocity = new Vector2(rb.velocity.x, inputY * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;
        animator.SetBool("isClimbing", true);
        bool playerHasVerticalMovement = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        if(playerHasVerticalMovement)
        {
            animator.SetFloat("stopClimbing", 1f);
        }
        else
        {
            animator.SetFloat("stopClimbing", 0f);
        }               
    }

    void FlipSprite()
    {
        playerHasHorizontalMovement = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);           
        }       
    }

    
    void Die()
    {
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            Vector2 deathForce = new Vector2(0f, 15f);
            isAlive = false;
            animator.SetTrigger("Dying");
            rb.velocity = deathForce;
            FindObjectOfType<GameSession>().processPlayerDeath();
        }      
    }

    void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        {
           // Vector3 pos = bulletSpawnPos.position;
            Instantiate(bullet, bulletSpawnPos.position, transform.rotation);
        }
    }
}
