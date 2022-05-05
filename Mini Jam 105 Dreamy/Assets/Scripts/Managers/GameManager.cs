using System.Collections;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {private set; get;}

    [Header("Writting settings")]
    [SerializeField] CinemachineVirtualCamera actualCamera;
    private CinemachineVirtualCamera mainCamera;
    [SerializeField] CanvasGroup writteCanvasGroup;
    [SerializeField] float alphaDecreaseRate;
    [SerializeField] DrawLine lineDrawer;
    [SerializeField] IsometricMovement movementScript;
    private bool readyToSleep;
    
    private int actualGameStage = 1;
    [Header("Game behaviour settings")]
    [SerializeField] DreamNode[] gameStageNode;

    void Awake()
    {
        Instance = this;
        Cursor.visible = false;
    }

    void Start()
    {
        mainCamera = actualCamera;
        readyToSleep = false;
        actualGameStage = ActualNodeHandler.Instance.GetActualNode();
        TextWriter.instance.ChangeNode(gameStageNode[actualGameStage-1]);
    }

    public void ChangeCamera(CinemachineVirtualCamera newCamera)
    {
        actualCamera.Priority = 10;
        newCamera.Priority = 11;
        actualCamera = newCamera;
    }

    public void ReturnToMainCamera()
    {
        actualCamera.Priority = 10;
        mainCamera.Priority = 11;
        actualCamera = mainCamera;
    }

    public void EnableCanvasWritten()
    {
        writteCanvasGroup.gameObject.SetActive(true);
        movementScript.DisableMovement();
        TextWriter.instance.SitPlayer();
        StartCoroutine(AlphaBlendCanvas(1));
    }

    public void DisableCanvasWritten()
    {
        StartCoroutine(AlphaBlendCanvas(0));
    }

    IEnumerator AlphaBlendCanvas(float target)
    {
        float blendValue = 1;
        bool writting = true;

        if(target < writteCanvasGroup.alpha)
        {
            blendValue *= -1;
            writting = false;
        }

        while(writteCanvasGroup.alpha != target)
        {
            writteCanvasGroup.alpha += blendValue * alphaDecreaseRate * Time.deltaTime;
            yield return null;
        }

        if(writting)
        {
            TextWriter.instance.WriteText();
        }
        else
        {
            writteCanvasGroup.gameObject.SetActive(false);
            ReturnToMainCamera();
            movementScript.EnableMovement();
            readyToSleep = true;
        }
        
    }

    public void EnableWriteSprite()
    {
        lineDrawer.EnableDrawing();
    }

    public void DisableWriteSprite()
    {
        lineDrawer.DisableCursor();
        RenderTextureHandler.Instance.SaveDrawing();
        lineDrawer.DisableDrawing();
    }

    public int GetActualStage()
    {
        return actualGameStage;
    }

    public bool IsReadyToSleep()
    {
        return readyToSleep;
    }

}
