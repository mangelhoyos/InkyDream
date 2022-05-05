using UnityEngine;
using UnityEngine.UI;

public class DrawingTextureRetriever : MonoBehaviour
{
    [SerializeField] private RenderTextureHandler.DrawingType entityType;

    void Start()
    {
        RetrieveTextureData();
    }
    
    private void RetrieveTextureData()
    {
        Texture2D entityTexture = RenderTextureHandler.Instance.GetTextureOfType(entityType);

        GetComponent<RawImage>().texture = entityTexture;
    }
}
