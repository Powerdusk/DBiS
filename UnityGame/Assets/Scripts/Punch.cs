using UnityEngine;
using System.Collections;

public class Punch : MonoBehaviour {

	public GameObject a;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1")){
			a.SetActive(true);
		}
		else
			a.SetActive(false);
	}
}
