using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public GameObject player;
    private float helpCameraFollowFaster = 3;
   
    void FixedUpdate () {
        this.transform.position = new Vector3 (Mathf.Lerp(this.transform.position.x, player.transform.position.x, Time.deltaTime*helpCameraFollowFaster),
        Mathf.Lerp(this.transform.position.y, player.transform.position.y, Time.deltaTime*helpCameraFollowFaster),0);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);                                 
    }
}
