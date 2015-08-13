using UnityEngine;
using System.Collections;

public class AITest : MonoBehaviour {
	public float roamTime;
	public float roamMax;
	public int distance;
	public int speed;
	public Transform player;
	private bool fight;
	public GameObject attack;

	// Use this for initialization
	void Start () {
		fight = false;
		attack.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		if(player.localPosition.x < gameObject.transform.localPosition.x)
			speed = -1;
		else
			speed = 1;
		if(fight){
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
			if(Mathf.Abs(player.localPosition.x - gameObject.transform.localPosition.x) < distance)
				attack.SetActive(true);
			else
				attack.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player")
			fight = true;
	}
}
