using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed;

    public int health;
    public GameObject deathEffectPrefab;
    public GameObject explosionPrefab;

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
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        health -= damage;

        if (health <= 0)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 0.5f);
            Destroy(gameObject);
        }
    }
}
