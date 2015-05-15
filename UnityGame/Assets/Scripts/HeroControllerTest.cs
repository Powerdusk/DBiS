using UnityEngine;
using System.Collections;

public class HeroControllerTest : MonoBehaviour {

	public float maxSpeed = 10;

	Animator anim;

	//JUMP STUF
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask isGround;
	public float jumpForce = 700;
	int preJumpCount = 0;
	int preJumpFrames = 7;
	private float move;
	private float speedDir;


	private string heroState = "IDLE";
	private Rigidbody2D rBody;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		//GROUNDED?

		// M O V E \\
		float move = Input.GetAxisRaw("Horizontal");
		speedDir = move * maxSpeed;

		// G R O U N D E D \\
		grounded = Physics2D.OverlapCircle(groundCheck.position,groundRadius,isGround);
		anim.SetBool("Ground", grounded);

		// S T A T E + M A C H I N E \\
		
		
		if (move == 0 && grounded == true)
		{
			heroState = "IDLE";
		}	
		if (move != 0 && grounded == true)
		{
			heroState = "RUN";
		}	
		if (grounded == true && Input.GetButtonDown("Jump"))
		{
			heroState = "JUMP";
		}



		switch(heroState)
		{
			case "IDLE":
				
				rBody.AddForce (new Vector2(move * maxSpeed, 0));
				//rBody.velocity.x = speedDir;
				break;
			case "RUN":
				rBody.AddForce(new Vector2(move * maxSpeed, 0));
				//rBody.velocity.x = speedDir;
				break;

		}
		Debug.Log(heroState); 

		// S P R I T E && A N I M \\
		//anim 
		anim.SetFloat("speed", Mathf.Abs(move));
		//flip sprite
		if (Input.GetAxisRaw("Horizontal") != 0)
		{
			GetComponent<Transform>().localScale = new Vector3 (Mathf.Sign (Input.GetAxisRaw("Horizontal")),1,1);
		}
	//}

	//void Update()
	//{
		if (grounded && Input.GetButtonDown ("Jump")) {
			anim.SetBool ("preJump", true);
			preJumpCount = 0;
		}
		preJumpCount += 1;
		if (preJumpCount == preJumpFrames) {
			anim.SetBool ("preJump", false);
			//anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
		}
	}


}
