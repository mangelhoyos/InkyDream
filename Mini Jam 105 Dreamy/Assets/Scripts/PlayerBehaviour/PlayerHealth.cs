using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int health = 40;
    private bool isDead = false;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip resetClip;

    public void ReceiveDamage(int damage)
    {
        if(!isDead)
        {
            health -= damage;
            AudioManager.instance.Play("DamagePlayer");
            if(health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        isDead = true;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
        StartCoroutine(DieDialogue());
        if(AudioManager.instance.IsPlaying("Pasos"))
        {
            AudioManager.instance.Stop("Pasos");
        }
    }

    IEnumerator DieDialogue()
    {
        audioSource.Stop();
        audioSource.clip = resetClip;
        audioSource.Play();
        yield return new WaitForSeconds(2.5f);
        BlinkManager.Instance.Blink(SceneManager.GetActiveScene().name);
    }
}
