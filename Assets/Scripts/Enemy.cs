using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed;

    public int health;
    public GameObject deathEffect;
    public GameObject explosion;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        int moveDirection = transform.position.x < target.position.x? 1 : -1;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        transform.localScale = new Vector3(moveDirection, 1, 1);
    }

    public void TakeDamage(int damage)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        health -= damage;

        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
