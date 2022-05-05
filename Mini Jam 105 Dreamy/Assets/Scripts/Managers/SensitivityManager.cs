using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityManager : MonoBehaviour
{
    MouseLook mouseScript;
    [SerializeField] Slider valueSlider;
    [SerializeField] GameObject pausePanel;

    bool isPaused = false;

    void Awake()
    {
        mouseScript = Camera.main.transform.GetComponent<MouseLook>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.Play("Button");
            if(AudioManager.instance.IsPlaying("Pasos"))
            {
                AudioManager.instance.Stop("Pasos");
            }
            if(isPaused)
            {
                mouseScript.LockCamera();
                isPaused = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                ChangeSensitivity();
            }
            else
            {
                mouseScript.UnlockCamera();
                isPaused = true;
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ChangeSensitivity()
    {
        mouseScript.ChangeSensitivity(valueSlider.value);
    }
}
