using UnityEngine;

public class RenderTextureHandler : MonoBehaviour
{
    public static RenderTextureHandler Instance {private set; get;}
    private int actualIndex = 0;
    [SerializeField]private Texture2D[] textures;
    [SerializeField]RenderTexture renderTexture;

    public void ChangeRenderTexture(RenderTexture texture)
    {
        renderTexture = texture;
    }

    public enum DrawingType
    {
        ENEMIES,
        PROJECTILE,
        PAINTING,
        FLOATINGOBJECT,
        LAMP, 
        ARTIFACT,
        WALLPAINTING,
        PORTAL,
        LASTBOSS
    }

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            textures = new Texture2D[9];
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SaveDrawing()
    {
        Texture2D drawing = null;
        while(drawing == null)
        {
            drawing = ToTexture2D(renderTexture);
        }
        textures[actualIndex] = drawing;
        actualIndex++;
    }

    Texture2D ToTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(1280, 720, TextureFormat.RGBA32, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public Texture2D GetTextureOfType(DrawingType drawType)
    {
        switch(drawType)
        {
            case DrawingType.ENEMIES:
                return textures[0];
            case DrawingType.PROJECTILE:
                return textures[1];
            case DrawingType.PAINTING:
                return textures[2];
            case DrawingType.FLOATINGOBJECT:
                return textures[3];
            case DrawingType.LAMP:
                return textures[4];
            case DrawingType.ARTIFACT:
                return textures[5];
            case DrawingType.WALLPAINTING:
                return textures[6];
            case DrawingType.PORTAL:
                return textures[7];
            case DrawingType.LASTBOSS:
                return textures[8];
            default:
                return null;
        }
    }
}
