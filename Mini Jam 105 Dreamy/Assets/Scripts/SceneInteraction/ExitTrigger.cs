using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private bool used = false;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player") && !used)
        {
            used = true;

            other.GetComponent<PlayerMovement>().enabled = false;
            ActualNodeHandler.Instance.NextNode();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if(AudioManager.instance.IsPlaying("Pasos"))
            {
                AudioManager.instance.Stop("Pasos");
            }
            
            if(ActualNodeHandler.Instance.GetActualNode() == 4)
            {
                BlinkManager.Instance.Blink("Credits");
                Cursor.visible = true;
            }
            else
            {
                BlinkManager.Instance.Blink("RealWorldScene");
            }
        }
    }
}
