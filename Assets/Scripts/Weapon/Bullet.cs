﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public float randomDirectAngle = 4.0f;
    public LayerMask whatIsSolid;

    public GameObject destroyEffectPrefab;

    private void Start()
    {
        Invoke("DestroyBullet", lifeTime);
        transform.Rotate(new Vector3(0, 0, Random.Range(-randomDirectAngle, randomDirectAngle)), Space.World);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                int finalDanamge = Random.Range(1, 5) == 3? 2 * damage : damage;
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(finalDanamge, transform.rotation.eulerAngles.z > 180? 1 : -1, finalDanamge == (2 * damage));
            }

            DestroyBullet();
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        GameObject destoryEffect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(destoryEffect, 0.1f);
        Destroy(gameObject);
    }
}