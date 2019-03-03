using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset; 
    void LateUpdate()
    {
        offset = transform.position - target.transform.position;
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10));

        // Smoothly move the camera towards that target position
        if (Input.GetKey (KeyCode.D) == false && Input.GetKey (KeyCode.A)==false && Input.GetKey (KeyCode.W)==false && Input.GetKey (KeyCode.S)==false)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            transform.position = target.transform.position + offset;
        }
    }
}
