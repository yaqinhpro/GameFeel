using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            rb.isKinematic = true;
            rb.velocity = new Vector2(0, 0);
            bc.enabled = false;
        }
    }
}
