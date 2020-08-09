using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public GameObject deathEffectPrefab;
    public GameObject explosionPrefab;

    public float speed = 5;
    public int health = 2;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private bool isDead = false;
    private float waitCounter = 0;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Movement();
        }
    }

    private void Movement()
    {
        int moveDirection = transform.position.x < target.position.x? 1 : -1;
        rigidBody.velocity = new Vector2(moveDirection * speed, 0);
        transform.localScale = new Vector3(-moveDirection, 1, 1);

        animator.SetBool("isMoving", true);
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
        rigidBody.isKinematic = true;
        rigidBody.velocity = new Vector2(0, 0);
        boxCollider.enabled = false;
        GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(deathEffect, 0.5f);

        animator.SetBool("isMoving", false);
        animator.Play(hitterDirection > 0 ? "RightFallDeath" : "LeftFallDeath");

        StartCoroutine(DieInPeace());
    }

    private IEnumerator DieInPeace()
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
