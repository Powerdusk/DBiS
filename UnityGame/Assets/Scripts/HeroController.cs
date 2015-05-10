using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	public float maxSpeed = 10;

	Animator anim;

	//JUMP STUF
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask isGround;
	public float jumpForce = 700;
	int preJumpCount = 0;
	int preJumpFrames = 15;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		//GROUNDED?
		grounded = Physics2D.OverlapCircle(groundCheck.position,groundRadius,isGround);
		anim.SetBool("Ground", grounded);
		//print (grounded);

		//anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		float move = Input.GetAxisRaw("Horizontal");
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

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
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
		}
	}


}
