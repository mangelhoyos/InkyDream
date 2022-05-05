using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    [SerializeField] Transform exitPortalTransform;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = exitPortalTransform.position;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }
}
