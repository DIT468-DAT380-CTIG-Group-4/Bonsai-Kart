using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;

    public float speed = 2;

    public float turnSmoothT = 0.1f;

    private float turnSmoothV;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v).normalized;

        if (dir.magnitude > 0)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothV, turnSmoothT);
            transform.rotation = Quaternion.Euler(-90, angle, 0);

            Vector3 finalDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(Time.deltaTime * speed * finalDir.normalized);
        }
    }
}
