using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedBullet : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            rigidBody.isKinematic = true;
            rigidBody.velocity = new Vector2(0, 0);
            boxCollider.enabled = false;
        }
    }
}
