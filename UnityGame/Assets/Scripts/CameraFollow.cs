using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float distance;
	public float height;
	public float width;
	public float damping;

	void Update()
	{
		Vector3 wantedPosition = target.TransformPoint(width,height, distance);
		transform.position = Vector3.Lerp(transform.position, wantedPosition,Time.deltaTime * damping);
	}
}