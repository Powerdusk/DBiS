using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	public float maxSpeed = 10;

	Animator anim;

	//JUMP STUF
	public bool grounded = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask isGround;
	public float jumpForce = 700;
	public int preJumpCount = 0;
	public int preJumpFrames = 5;
	public int shortHopWindow = 2;
	public bool isPrejump = false;
	public int jumpNum = 0;
	public int jumpMax;

	public LayerMask isLedge;
	public Transform wallCheck;
	public float wallRadius = 0.2f;
	public bool walled = false;
	public bool ledge = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		groundCheck = transform.FindChild ("GroundCheck");
	}
	
	// Update is called once per frame
	void Update () {

	}
	//anything sensitive to the framerate (movements) should go in here instead, this runs on a forced 60fps calculation
	//while update can run faster based on your computer
	void FixedUpdate()
	{
		
		//GROUNDED?
		grounded = Physics2D.OverlapCircle(groundCheck.position,groundRadius,isGround);
		anim.SetBool("Ground", grounded);
		if(grounded && !walled)
			jumpNum = 0;

		if(!grounded && Physics2D.OverlapCircle(wallCheck.position,wallRadius,isGround))
			walled = true;
		else
			walled = false;

		if(!grounded && Physics2D.OverlapCircle(wallCheck.position,wallRadius,isLedge))
			ledge = true;
		else
			ledge = false;

		if(walled && gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
			gameObject.GetComponent<Rigidbody2D>().gravityScale = .3f;
		else
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
		//print (grounded);
		
		//anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
		
		float move = Input.GetAxisRaw("Horizontal");

		if(!walled)
			GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		//anim
		anim.SetFloat("speed", Mathf.Abs(move));
		//avoid constantly referenceing input
		float Hinput = Input.GetAxisRaw ("Horizontal");
		//flip sprite
		if (Hinput != 0)
		{
			GetComponent<Transform>().localScale = new Vector3 (Mathf.Sign (Hinput),1,1);
		}
		if(Input.GetButtonUp("Jump"))
			jumpNum++;
		//if you are grounded, pressing the button and have not already started a jump
		if (jumpNum < jumpMax && Input.GetButtonDown ("Jump") && !isPrejump) {
			//start anim
			anim.SetBool ("preJump", true);
			//start a jump
			isPrejump = true;
			//reset frame counter
			preJumpCount = 0;
		} else if(grounded && Input.GetButton("Jump") && isPrejump) {
			//increment frame counter
			preJumpCount++;
			Debug.Log (preJumpCount);
		}
		//short hop
		else if (jumpNum < jumpMax && preJumpCount > 0 && preJumpCount <= shortHopWindow && Input.GetButtonUp ("Jump") && isPrejump) {
			preJumpCount = 0;
			isPrejump = false;
			anim.SetBool("preJump",false);
			anim.SetBool ("Jumping", true);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
			Debug.Log ("short hop");
		}
		//full hop
		else if (jumpNum < jumpMax && preJumpCount >= preJumpFrames || (isPrejump && !Input.GetButton("Jump"))) {
			anim.SetBool ("preJump", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 2*jumpForce));
			preJumpCount = 0;
			Debug.Log ("full hop");
			isPrejump = false;
		}
	}

}
