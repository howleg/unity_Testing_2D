using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	Rigidbody2D rb2d; 
	SpriteRenderer sprRend; 
	Animator anim;
	CircleCollider2D cC;

    
    public float hSpd;

    public float maxVSpd = 20f;
	public float vSpd;

	public bool touchingGround;




	// Use this for initialization
	void Start () 
	{
		rb2d = GetComponent <Rigidbody2D> ();
		anim = GetComponent <Animator> ();
		sprRend = GetComponent <SpriteRenderer> ();
		cC = GetComponent<CircleCollider2D>();  //need this to know if player is touching the ground using the circle collider for feet
    }


	void FixedUpdate()
	{
		float joyHorzPos = Input.GetAxisRaw ("Horizontal"); //float number from -1 to 1 represents the postion of joystick

		//flips the sprite on the X if we're going backwards
		if (joyHorzPos < 0 && !sprRend.flipX)
			sprRend.flipX = true;
		else if (joyHorzPos > 0 && sprRend.flipX)
			sprRend.flipX = false;


        //moves character depending horizontal detpending on position of stick and variable hSpd
		rb2d.velocity = new Vector2 (joyHorzPos * hSpd, rb2d.velocity.y);  //multiply by hSpd to get the speed for player
		anim.SetFloat ("hSpeed", Mathf.Abs( joyHorzPos)); //sets variable for animator


		//******************************************************************************************
		//******************************************************************************************
		//Capping when falling to VSpd * -1.  What will happen if going up though??? more testing!!!! *Fo Realz
		if (rb2d.velocity.magnitude > maxVSpd)
			rb2d.velocity = new Vector2(joyHorzPos * hSpd,-maxVSpd);

		vSpd = rb2d.velocity.y;
		anim.SetFloat ("vSpeed", vSpd);



        //sets wether circle collider at feet is touching layer 1. Whatever that layer is???
		//******Look into this!!!*************
        touchingGround = cC.IsTouchingLayers(1);  // if its touching default layer? not sure but this works on unmodified layers on surfaces
		anim.SetBool("touchingGround",touchingGround);   //sets anim bool for falling/jumping animation






	}//end of FixedUpdate






	// Update is called once per frame
	void Update () 
	{
		//This is how I'm making the character jump
		//*This can go here in update() and not FixedUpdate() because its just a single force and not continuously
		if (touchingGround && Input.GetKeyDown(KeyCode.JoystickButton0)) 
		{
			rb2d.AddForce(new Vector2(0, 800f));  //using 800 as force because i'm setting the gravity scale in project really high. -30 *Crisp jumps
		}
	
	}//end of Update

} //end of PlayerController
