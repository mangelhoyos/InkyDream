using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricMovement : MonoBehaviour
{
    [SerializeField] float movementVelocity;
    [SerializeField] float smoothTime;
    private Rigidbody rb;
    private bool canMove = true;

    float turnSmoothVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(canMove)
            CheckForMovement();
    }

    void CheckForMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if(z != 0)
        {
            if(!AudioManager.instance.IsPlaying("Pasos"))
            {
                AudioManager.instance.Play("Pasos");
            }
            rb.velocity = new Vector3(z * -movementVelocity, 0, 0);
            CheckRotation(z, false);
        }
        else if(h != 0)
        {
            if(!AudioManager.instance.IsPlaying("Pasos"))
            {
                AudioManager.instance.Play("Pasos");
            }
            rb.velocity = new Vector3(0, 0, h * movementVelocity);
            CheckRotation(h, true);
        }

        if(z == 0 && h == 0)
        {
            if(AudioManager.instance.IsPlaying("Pasos"))
            {
                AudioManager.instance.Stop("Pasos");
            }
        }
    }

    void CheckRotation(float axis, bool isHorizontal)
    {
        if(isHorizontal)
        {
            if(axis == 1)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 0, ref turnSmoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            else
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 180, ref turnSmoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }
        else
        {
            if(axis == 1)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, -90, ref turnSmoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            else
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 90, ref turnSmoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

}
