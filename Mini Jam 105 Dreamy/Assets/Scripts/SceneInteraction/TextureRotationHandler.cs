using UnityEngine;

public class TextureRotationHandler : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private RenderTextureHandler.DrawingType type;
    private float OFFSET = 90;

    void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        if(type == RenderTextureHandler.DrawingType.ENEMIES)
        {
            OFFSET = 90;
        }
        else
        {
            OFFSET = 0;
        }
    }
    void Update()
    {
        UpdateRotationTowardsPlayer();
    }

    void UpdateRotationTowardsPlayer()
    {
         
         transform.LookAt(playerTransform);

         if(type != RenderTextureHandler.DrawingType.ENEMIES && type != RenderTextureHandler.DrawingType.PROJECTILE)
         {
             transform.rotation = Quaternion.Euler(90 , transform.eulerAngles.y + 90, 90);
         }
         else
         {
             transform.localRotation = Quaternion.Euler(0 , transform.eulerAngles.y + OFFSET, 0);
         }
    }
}
