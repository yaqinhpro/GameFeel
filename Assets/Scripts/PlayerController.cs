using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Weapon weapon;
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveVelocity;
    public float speed;

    private float timeBtwShots = 0;
    public float startTimeBtwShots = 0.25f;

    public float shootKickOffset = 0.3f;
    private Vector3 shootDirection = Vector3.right;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        moveVelocity = moveInput * speed;

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                weapon.Fire();
                transform.position += -1 * shootDirection * shootKickOffset;

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
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }


}
