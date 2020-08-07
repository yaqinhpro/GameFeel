using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public AudioSource shootSound;

    public GameObject projectile;
    public GameObject shotEffect;
    public Transform shotPoint;
    public Animator camAnim;

    public void Fire()
    {
        Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
        camAnim.SetTrigger("shake");
        Instantiate(projectile, shotPoint.position, Quaternion.FromToRotation(Vector3.up, Vector3.right));
        shootSound.Play();
    }
}
