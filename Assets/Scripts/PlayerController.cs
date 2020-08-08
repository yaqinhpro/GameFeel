using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Weapon weapon;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    public AudioSource jumpSound;

    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask ground;
    public bool isGround;
    public bool isJump;
    public bool isDashing;
    private bool jumpPressed;
    private int jumpCount;

    private float timeBtwShots = 0;
    public float startTimeBtwShots = 0.25f;
    public float shootKickOffset = 0.3f;
    private float faceDirection = 1;
    private float moveDirection = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                weapon.Fire(faceDirection);
                transform.position += moveDirection * Vector3.one * shootKickOffset;

                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 1.5f, ground);

        GroundMovement();
        Jump();
        SwitchAnim();
    }

    void GroundMovement()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        if ((moveDirection == 1) || (moveDirection == -1))
        {
            faceDirection = moveDirection;
            rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
            transform.localScale = new Vector3(moveDirection, 1, 1);
        }
    }

    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            jumpSound.Play();

            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void SwitchAnim()
    {
        if (isGround && (rb.velocity.x != 0))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
}
