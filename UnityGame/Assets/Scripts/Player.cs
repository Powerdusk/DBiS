using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxSpeed = 10;
	float move;
	public float jumpSpeed = 15;
	public float airTime = 1.3f;
	private float air = 0;				//jump duration

	private bool jumping = false;
	private bool doubleJump = false;		//jump length
	public bool grounded = false;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	

	void Start () {
	}

	void FixedUpdate(){
		grounded = 	Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
	}

	// Update is called once per frame
	void Update () {
		//Horizontal Movement
		move = Input.GetAxisRaw("Horizontal");
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		if(grounded){
			jumping = false;
			doubleJump = false;
			if (Input.GetButtonDown ("Jump")){
			    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
				jumping = true;
			}
		}
		else{

			if(Input.GetButtonUp("Jump")){
				air = 0;
				jumping = false;
			}
			if (Input.GetButtonDown ("Jump") && !doubleJump ){
				jumping = true;
				doubleJump = true;
			}
			if (Input.GetButton ("Jump") && jumping){
				air += Time.deltaTime;
				if(air > airTime){
					jumping = false;
					air = 0;
				}
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
			}

		}


	}
}
