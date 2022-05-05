using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;   
    }

    public void StartGame()
    {
        BlinkManager.Instance.Blink("RealWorldScene");
    }
}
