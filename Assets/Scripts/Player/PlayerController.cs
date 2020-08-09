using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Weapon weapon;
    public AudioSource jumpSound;
    public AudioSource hurtSound;

    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask ground;
    public bool isGround;
    public bool isJump;
    public bool isDashing;
    private bool jumpPressed;
    private int jumpCount;

    public int health = 3;

    private float timeBtwShots = 0;
    public float startTimeBtwShots = 0.25f;
    public float shootKickOffset = 0.3f;

    private float faceDirection = 1;
    private float moveDirection = 1;

    private Rigidbody2D rigidBody;
    private Animator animator;

    public event EventHandler OnPlayerDied;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
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
        isGround = Physics2D.OverlapCircle(groundCheck.position, Mathf.Abs(groundCheck.localPosition.y), ground);

        GroundMovement();
        Jump();
    }

    void GroundMovement()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        if ((moveDirection == 1) || (moveDirection == -1))
        {
            faceDirection = moveDirection;
            rigidBody.velocity = new Vector2(moveDirection * speed, rigidBody.velocity.y);
            transform.localScale = new Vector3(moveDirection, 1, 1);

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
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
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            GetHurt(new Vector2(collision.collider.transform.position.x - transform.position.x, jumpForce));
        }
    }

    private void GetHurt(Vector2 kickVelocity)
    {
        rigidBody.velocity = kickVelocity;
        hurtSound.Play();
        animator.Play("Hurt");
        health -= 1;

        if (health == 0)
        {
            RestInPeace();
        }
    }

    private void RestInPeace()
    {
        OnPlayerDied.Invoke(this, EventArgs.Empty);
    }
}
