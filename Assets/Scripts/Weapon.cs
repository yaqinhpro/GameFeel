using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public AudioSource shootSound;

    public int bulletNumPerFire = 3;
    public GameObject bulletPrefab;
    public GameObject shotEffect;
    public Transform shotPoint;

    public void Fire(float fireDirection)
    {
        Instantiate(shotEffect, shotPoint.position, Quaternion.identity);

        for (int i = 0; i < bulletNumPerFire; i++)
        {
            Instantiate(bulletPrefab, shotPoint.position, Quaternion.FromToRotation(Vector3.up, fireDirection * Vector3.right));
        }

        shootSound.Play();
    }
}
