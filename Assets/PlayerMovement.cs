using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerCollision collision;
    public Rigidbody2D rb;
    public float speed = 10f;
    public float jumpForce = 2f;
    public bool isMoving;
    public float climbSpeed;
    [SerializeField] private float climbEffort;
    private float jumpCooldown;
    public bool wantToJump;
    float yVelocity = 0.0f;
    public float acceleration = 0.2f;
    public float decceleration = 0.05f;
    public SpriteRenderer sprite_renderer;
    public Animator playerAnim;
    bool isAttacking;
    public bool onRope;
    private float struggleTime;

    float x = 0;
    float y = 0;

    private void Start()
    {
        collision = GetComponent<PlayerCollision>();
    }
    
    private void Update()
    {

        if (collision.onGround) playerAnim.SetBool("OnGround", true); else playerAnim.SetBool("OnGround", false);
        if (isMoving) playerAnim.SetBool("IsMoving", true); else playerAnim.SetBool("IsMoving", false);

        if(Input.GetButtonDown("Fire1") && !isAttacking && collision.onGround)
        {
            x = 0;
            y = 0;
            isMoving = false;
            rb.velocity = Vector3.zero;
            isAttacking = true;
            playerAnim.Play("Temp_player_Attack");
            Invoke("StopAttacking", 1.5f);
        }
        if (onRope)
        { 
            if(Input.GetAxisRaw("Vertical") > 0)
            {
                climbEffort = struggleTime;
                struggleTime = struggleTime + Time.deltaTime;
                if (struggleTime >= 1.5f) struggleTime = 0;
                rb.velocity = (new Vector2(0, Input.GetAxis("Vertical") * (climbSpeed - climbEffort)));
            }
            else if(Input.GetAxisRaw("Vertical") < 0)
            {
                rb.velocity = (new Vector2(0, Input.GetAxis("Vertical") * (2*climbSpeed)));
            }
            else
            {
                climbEffort = 0;
                rb.velocity = (new Vector2(0, Input.GetAxis("Vertical")));
            }
        }


        if (!isAttacking && !onRope)
        {
            
            if (rb.velocity.x <= 0 && isMoving)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                //sprite_renderer.flipX = true;
            }
            else if (isMoving)
            {
                transform.localScale = new Vector3(1, 1, 1);
                //sprite_renderer.flipX = false;
            }
            


            if (!collision.onGround)
                acceleration = Mathf.SmoothDamp(acceleration, 0, ref yVelocity, 0.1f);
            else
                acceleration = 0.04f;


            if (Input.GetAxisRaw("Horizontal") != 0) isMoving = true; else isMoving = false;

            if (Input.GetAxisRaw("Horizontal") != 0)
                x = Mathf.SmoothDamp(x, Input.GetAxis("Horizontal"), ref yVelocity, acceleration);

            //y = Mathf.SmoothDamp(y, Input.GetAxis("Vertical"), ref yVelocity, acceleration);
            Vector2 dir = new Vector2(x, y);

            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                x = Mathf.SmoothDamp(x, Input.GetAxis("Horizontal"), ref yVelocity, decceleration);
            }

            Walk(dir);

            if (Input.GetButtonDown("Jump"))
            {
                CancelInvoke("ResetWantToJump");
                Invoke("ResetWantToJump", 0.07f);
                wantToJump = true;
            }

            if (collision.onGround && jumpCooldown <= 0 && wantToJump == true)
            {
                Jump();
            }

            if (Input.GetButtonDown("Jump") && (collision.onGround || collision.onGroundCoyote) && jumpCooldown <= 0)
            {
                Jump();
                jumpCooldown = 0.5f;
            }

            if (jumpCooldown > 0)
            {
                jumpCooldown = jumpCooldown - Time.deltaTime;
            }

        }

    }

    public void ClimbRope()
    {
        onRope = true;
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
    }

    public void DismountRope()
    {
        onRope = false;
        rb.velocity = Vector3.zero;
        rb.gravityScale = 1;
    }

    public void StopAttacking()
    {
        isAttacking = false;
    }

    public void ResetWantToJump()
    {
        wantToJump = false;
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void Jump()
    {
        playerAnim.Play("Temp_player_Jump");
        collision.onGroundCoyote = false;
        wantToJump = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
    }
}
