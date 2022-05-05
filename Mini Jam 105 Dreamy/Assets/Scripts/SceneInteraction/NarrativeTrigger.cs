using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    [SerializeField] AudioClip clipToTrigger;
    [SerializeField] AudioSource listener;
    private bool isUsed = false;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player") && !isUsed)
        {
            isUsed = true;
            listener.clip = clipToTrigger;
            listener.Play();
        }
    }
}
