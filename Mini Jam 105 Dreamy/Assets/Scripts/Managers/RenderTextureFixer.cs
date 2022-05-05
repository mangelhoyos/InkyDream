using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureFixer : MonoBehaviour
{
    [SerializeField] RenderTexture textureToFix;
    [SerializeField] Camera cameraToFix;
    [SerializeField] RawImage imagePreVisualization;
    
    private void Start()
    {
        FixTexture();
    }

    void FixTexture()
    {
        if (textureToFix != null ) 
        {
            textureToFix.Release( );
        }
 
        textureToFix = new RenderTexture( Screen.width, Screen.height, 24 );
        Debug.Log(Screen.width +" "+ Screen.height);

        cameraToFix.targetTexture = textureToFix;
        imagePreVisualization.texture = textureToFix;
        RenderTextureHandler.Instance.ChangeRenderTexture(textureToFix);
    }
    
}
