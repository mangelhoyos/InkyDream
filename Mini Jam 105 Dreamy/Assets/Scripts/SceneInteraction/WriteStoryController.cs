using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WriteStoryController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera eventCamera;
    [SerializeField] UnityEvent OnDeskActivated;
    [SerializeField] TMP_Text helperText;
    private bool isOnTrigger = false;
    private bool used = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isOnTrigger && !used)
        {
            GameManager.Instance.ChangeCamera(eventCamera);
            if(AudioManager.instance.IsPlaying("Pasos"))
            {
                AudioManager.instance.Stop("Pasos");
            }
            OnDeskActivated?.Invoke();
            used = true;
            helperText.text = "";
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player") && !used)
        {
            helperText.text = "Press E to interact";
            isOnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player") && !used)
        {
            helperText.text = "";
            isOnTrigger = false;
        }
    }

}
