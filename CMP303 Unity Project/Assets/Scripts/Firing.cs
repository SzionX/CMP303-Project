using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
       GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
       Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
       rigidbody.AddForce(firingPoint.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
