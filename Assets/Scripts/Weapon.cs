using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public AudioSource shootSound;

    public GameObject projectile;
    public GameObject shotEffect;
    public Transform shotPoint;

    public void Fire(float fireDirection)
    {
        Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
        Instantiate(projectile, shotPoint.position, Quaternion.FromToRotation(Vector3.up, fireDirection * Vector3.right));
        shootSound.Play();
    }
}
