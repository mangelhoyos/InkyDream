using System.Collections;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private Transform projectileShootTransform;
    [SerializeField] private float projectileShootSpeed;
    
    private Transform playerTransform;

    void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    private void Start() 
    {
        StartCoroutine(ShootProjectileToPlayer(GetRandomDelay()));
    }


    IEnumerator ShootProjectileToPlayer(int delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.instance.Play("Shoot");
        Rigidbody projectile = Instantiate(projectilePrefab, projectileShootTransform.position, projectilePrefab.rotation);
        
        Vector3 shootDir = playerTransform.position - transform.position;
        shootDir.Normalize();
        shootDir.y = 0;
        projectile.velocity = shootDir * projectileShootSpeed;

        StartCoroutine(ShootProjectileToPlayer(GetRandomDelay()));
    }

    private int GetRandomDelay()
    {
        return Random.Range(2,6);
    }
}
