using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float speed;
	private Vector2 thisTrans;
	

	void Start()
	{

	}
	void Update()
	{
		float mDist = Mathf.Sign((transform.position.x - target.position.x)* -1);
		thisTrans = new Vector2(mDist * speed,0);

		Debug.Log(mDist);
		Debug.Log(thisTrans);

		if ( Mathf.Abs(transform.position.x - target.position.x) > 2)
		transform.Translate((0.08f * mDist),0,0);
	}
}