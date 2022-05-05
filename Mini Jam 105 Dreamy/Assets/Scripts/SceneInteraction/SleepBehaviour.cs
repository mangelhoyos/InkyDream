using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepBehaviour : MonoBehaviour
{
    private bool isOnTrigger = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isOnTrigger && GameManager.Instance.IsReadyToSleep())
        {
            AudioManager.instance.Play("Sleep");
            BlinkManager.Instance.Blink("Dream"+GameManager.Instance.GetActualStage().ToString());
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            isOnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            isOnTrigger = false;
        }
    }
}
