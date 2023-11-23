using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform mainCam;

    public CinemachineFreeLook thirdPersonCam;

    private float speed = 4f;

    private float turnSmoothT = 0.25f;

    private float turnSmoothV;

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(0, 0, Mathf.Abs(v)).normalized;

        
        if (dir.magnitude <= 0)
        {
            return;
        }
        
        float angleDiff = 0f;
        float fwd = v > 0 ? 1 : -1;
        // Camera rotations
        if (Input.GetKey("a"))
        {
            angleDiff = -50 * Time.deltaTime * fwd;
        }
        if (Input.GetKey("d"))
        {
            angleDiff = 50 * Time.deltaTime * fwd;
        }

        thirdPersonCam.m_XAxis.Value += angleDiff;
        
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
        
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothV, turnSmoothT);
        // -90deg on X to correct the offset angle from the model
        // We're putting extra rotation to make the car appear to face in the direction it's turning
        transform.rotation = Quaternion.Euler(-90, angle + 2.5f * angleDiff, 0);

        Vector3 finalDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        controller.Move(Time.deltaTime * speed * fwd * finalDir.normalized);
    }
}
