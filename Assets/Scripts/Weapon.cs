using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float offset;

    public GameObject projectile;
    public GameObject shotEffect;
    public Transform shotPoint;
    public Animator camAnim;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
                camAnim.SetTrigger("shake");
                Instantiate(projectile, shotPoint.position, Quaternion.FromToRotation(Vector3.up, Vector3.right));
                timeBtwShots = startTimeBtwShots;
            }
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }

       
    }
}
