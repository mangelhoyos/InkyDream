using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] CharacterController controller;

    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.81f;

    [Header("Ground check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundmask;

    [Header("Animation controller")]
    [SerializeField] Animator anim;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(x != 0 || z != 0)
        {
            if(!AudioManager.instance.IsPlaying("Pasos"))
            {
                AudioManager.instance.Play("Pasos");
            }
        }
        else
        {
            if(AudioManager.instance.IsPlaying("Pasos"))
            {
                AudioManager.instance.Stop("Pasos");
            }
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(anim != null)
            anim.SetFloat("Velocity", Mathf.Abs(x) + Mathf.Abs(z));  
    }
}