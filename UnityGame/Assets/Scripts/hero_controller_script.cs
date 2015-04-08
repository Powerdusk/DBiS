using UnityEngine;
using System.Collections;

public class hero_controller_script : MonoBehaviour {

	public float maxSpeed = 10;

	Animator anim;

	//JUMP STUF
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask isGround;
	public float jumpForce = 700;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//GROUNDED?
		grounded = Physics2D.OverlapCircle(groundCheck.position,groundRadius,isGround);
		anim.SetBool("Ground", grounded);

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		float move = Input.GetAxis("Horizontal");
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		//anim
		anim.SetFloat("speed", Mathf.Abs(move));


		//flip sprite
		if (Input.GetAxisRaw("Horizontal") != 0)
		{
			GetComponent<Transform>().localScale = new Vector3 (Input.GetAxisRaw("Horizontal"),1,1);
		}
	}

	void Update()
	{
		if (grounded && Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetBool("Ground", false);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}
	}


}
