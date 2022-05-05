using UnityEngine;

public class EntityTextureRetriever : MonoBehaviour
{
    [SerializeField] private RenderTextureHandler.DrawingType entityType;
    Material entityMaterial;
    void Start()
    {
        RetrieveTextureData();
    }
    
    private void RetrieveTextureData()
    {
        Texture2D entityTexture = RenderTextureHandler.Instance.GetTextureOfType(entityType);

        Material materialInstance = GetComponent<MeshRenderer>().material;
        materialInstance.mainTexture = entityTexture;
    }
}
