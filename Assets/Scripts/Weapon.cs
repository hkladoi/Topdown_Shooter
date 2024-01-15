using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform FirePos;
    public GameObject muzzle;
    public float FireRate;
    public float BulletForce;
    private float nextFire;
    private bool isFiring = false;
    void Update()
    {
        RotateGun();
        nextFire -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
            if (isFiring)
            {
                StartCoroutine(FireContinuously());
            }
            //Shoot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }
    }
    IEnumerator FireContinuously()
    {
        while (isFiring)
        {
            if (Time.time > nextFire)
            {
                Shoot();
                nextFire = Time.time + FireRate;
            }
            yield return null;
        }
    }
    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lockDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lockDir.y, lockDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle);
        transform.rotation = rot;
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(1, -1, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
    }

    void Shoot()
    {
        nextFire = FireRate;
        GameObject bullet = Instantiate(this.bullet, FirePos.position, Quaternion.identity);
        Instantiate(muzzle, FirePos.position, transform.rotation, transform);
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(transform.right * BulletForce, ForceMode2D.Impulse);
    }
}
