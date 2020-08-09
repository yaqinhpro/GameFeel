using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public AudioSource shootSound;

    public int bulletNumPerFire = 3;
    public GameObject bulletPrefab;
    public GameObject usedBulletPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform shotPoint;
    public Transform usedBulletShotPoint;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Fire(float fireDirection)
    {
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, shotPoint.position, Quaternion.identity);

        for (int i = 0; i < bulletNumPerFire; i++)
        {
            Instantiate(bulletPrefab, shotPoint.position, Quaternion.FromToRotation(Vector3.up, fireDirection * Vector3.right));
        }

        GameObject usedBullet = Instantiate(usedBulletPrefab, usedBulletShotPoint.position, Quaternion.FromToRotation(Vector3.up, -fireDirection * Vector3.right));
        usedBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-fireDirection, 4));

        anim.Play("Fire");
        shootSound.Play();

        Destroy(muzzleFlash, 0.2f);
    }
}
