using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private bool isPlayerBullet;
    private void OnTriggerEnter(Collider other) 
    {
        if(isPlayerBullet)
        {
            if(other.CompareTag("Enemy"))
            {
                AudioManager.instance.Play("Damage");
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else if(other.CompareTag("Pared"))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealth>().ReceiveDamage(5);
                Destroy(gameObject);
            }
            if(other.CompareTag("Pared"))
            {
                Destroy(gameObject);
            }
        }

    }
}
