using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [Header("Line renderer setup")]
    [SerializeField] GameObject linePrefab;
    [SerializeField] Camera renderCamera;
    [SerializeField] GameObject drawSpaceGameObject;
    GameObject currentLine;
    LineRenderer lineRenderer;
    List<Vector2> mousePosition = new List<Vector2>();
    List<GameObject> lines = new List<GameObject>();
    [SerializeField] GameObject mouseObj;
    [SerializeField] GameObject drawTexture;
    private bool canDraw = false;

    void Update() 
    {
       if(canDraw){
            Vector3 mousePos = renderCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mouseObj.transform.position = mousePos;
            if(Input.GetMouseButtonDown(0))
            {
                CreateLine(mousePos);
            }
            if(Input.GetMouseButton(0))
            {
                if(!AudioManager.instance.IsPlaying("Lapiz"))
                {
                    AudioManager.instance.Play("Lapiz");
                }
                Vector2 tempFingerpos = mousePos;
                if(Vector2.Distance(tempFingerpos, mousePosition[mousePosition.Count-1])>0.1f)
                {
                    UpdateLine(tempFingerpos);
                }
            }
            else
            {
                if(AudioManager.instance.IsPlaying("Lapiz"))
                {
                    AudioManager.instance.Stop("Lapiz");
                }
            }
       }
    }
    
    void CreateLine(Vector3 mousePos)
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lines.Add(currentLine);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        mousePosition.Clear();
        mousePosition.Add(mousePos);
        mousePosition.Add(mousePos);
        lineRenderer.SetPosition(0, mousePosition[0]);
        lineRenderer.SetPosition(1, mousePosition[1]);
        
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        mousePosition.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, newFingerPos);     
    }

    public void EnableDrawing()
    {
        mouseObj.SetActive(true);
        canDraw = true;
        drawTexture.SetActive(true);
        drawSpaceGameObject.SetActive(true);
        renderCamera.gameObject.SetActive(true);
    }

    public void DisableCursor()
    {
        mouseObj.SetActive(false);
    }
    public void DisableDrawing()
    {
        if(AudioManager.instance.IsPlaying("Lapiz"))
        {
             AudioManager.instance.Stop("Lapiz");
        }
        mouseObj.SetActive(false);
        drawSpaceGameObject.SetActive(false);
        drawTexture.SetActive(false);
        canDraw = false;
        renderCamera.gameObject.SetActive(false);
        foreach(GameObject line in lines)
        {
            Destroy(line);
        }
        lines = new List<GameObject>();
    }
    
}
