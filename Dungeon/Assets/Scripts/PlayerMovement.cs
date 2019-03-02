using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	private Rigidbody2D rb2d;
	 public float moveSpeed;
	Animator anim;
    string animation_name = "";
    float player_horizontal = 0.0f;
    float player_vertical = 0.0f;

	// Use this for initialization
	void Start () {
		
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator>();
		rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;



        

	}

	public void freezemovement(){
		rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
		Debug.Log("rigidbody has been frozen");
	}

	public void unfreezemovement()
	{
		rb2d.constraints = RigidbodyConstraints2D.None;
		rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
		Debug.Log("rigidbody has been unfrozen");
	}

	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.speed = 2;
            moveSpeed = 30;
        }
        else
        {
            anim.speed = 1;
            moveSpeed = 16;
        }
		if (Input.GetKey (KeyCode.D)) {
            anim.enabled = true;
            animation_name = "Right";
            player_horizontal = 0.5f;
        }
        if (Input.GetKey (KeyCode.A))
        {
            anim.enabled = true;
            animation_name = "Left";
            player_horizontal = -0.5f;
        }
        if (Input.GetKey (KeyCode.W))
        {
            anim.enabled = true;
            animation_name = "Up";
            player_vertical = 0.5f;
        }
        if (Input.GetKey (KeyCode.S))
        {
            anim.enabled = true;
            animation_name = "Down";
            player_vertical = -0.5f;
        }
        if (Input.GetKey (KeyCode.D) == false && Input.GetKey (KeyCode.A)==false && Input.GetKey (KeyCode.W)==false && Input.GetKey (KeyCode.S)==false)
        {
            player_vertical = 0.0f;
            player_horizontal = 0.0f;
            anim.enabled = false;
        }
		rb2d.velocity = new Vector2(Mathf.Lerp(0, player_horizontal* moveSpeed, 0.8f),
			Mathf.Lerp(0, player_vertical* moveSpeed, 0.8f));
        anim.Play(animation_name);
		
	}
}
