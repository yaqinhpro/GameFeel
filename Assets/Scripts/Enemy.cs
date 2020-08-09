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

    private bool isDead;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;

    private float waitCounter = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Movement();
        }
    }

    void Movement()
    {
        int moveDirection = transform.position.x < target.position.x? 1 : -1;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
        transform.localScale = new Vector3(moveDirection, 1, 1);

        anim.SetBool("isMoving", true);
    }

    public void TakeDamage(int damage, int hitterDirection, bool isExplosion)
    {
        if (!isDead)
        {
            if (isExplosion)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 0.5f);
            }

            health -= damage;

            if (health <= 0)
            {
                Die(hitterDirection);
            }
        }
    }

    private void Die(int hitterDirection)
    {
        isDead = true;
        rb.isKinematic = true;
        rb.velocity = new Vector2(0, 0);
        bc.enabled = false;
        GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(deathEffect, 0.5f);

        anim.SetBool("isMoving", false);
        anim.Play(hitterDirection > 0 ? "RightFallDeath" : "LeftFallDeath");

        StartCoroutine(DieInPeace());
    }

    IEnumerator DieInPeace()
    {
        Time.timeScale = 0;
        waitCounter = 0;
        while (waitCounter < 0.05f)
        {
            waitCounter += Time.unscaledDeltaTime;

            //Yield until the next frame
            yield return null;
        }

        Time.timeScale = 1;
    }
}
