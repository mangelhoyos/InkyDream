using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Rigidbody bulletPrefab;
    [SerializeField] float shootDelay;
    float actualDelay;
    void Update()
    {
        actualDelay += Time.deltaTime;

        if(Input.GetMouseButtonDown(0) && actualDelay >= shootDelay)
        {
            actualDelay = 0;
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        AudioManager.instance.Play("Shoot");
        Rigidbody projectile = Instantiate(bulletPrefab, transform.position, bulletPrefab.rotation);
        
        Vector3 shootDir = transform.forward;
        shootDir.y = 0;
        projectile.velocity = shootDir * 20f;

    }
}
