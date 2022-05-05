using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlinkManager : MonoBehaviour
{
    public static BlinkManager Instance {private set; get;}

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float alphaDecreaseRate;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Blink(string sceneName)
    {
        StartCoroutine(AlphaBlendCanvas(sceneName));
    }

    IEnumerator AlphaBlendCanvas(string sceneName)
    {
        float blendValue = 1;


        while(canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += blendValue * alphaDecreaseRate * Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(2f);

        blendValue = -1;
        while(canvasGroup.alpha != 0)
        {
            canvasGroup.alpha += blendValue * alphaDecreaseRate * Time.deltaTime;
            yield return null;
        }
    }
}
