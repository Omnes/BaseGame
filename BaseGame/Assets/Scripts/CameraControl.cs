using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Transform player;
	public float distance = 7;
	public float tiltOffset = 20;
	public float tiltAngle = 45;
	public float angle = 0;
	public float rotationSpeed = 10;
	
	// Use this for initialization
	void Start () {
		
	}

	public void HandleInput(Vector2 inputVector){
		angle += inputVector.x * Time.deltaTime * rotationSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.rotation = Quaternion.Euler(tiltAngle-tiltOffset,-angle,0);
		var v = new Vector3(Mathf.Cos((270+angle)*Mathf.Deg2Rad), Mathf.Sin(tiltAngle*Mathf.Deg2Rad), Mathf.Sin((270+angle)*Mathf.Deg2Rad)) * distance;
		transform.position = player.position + v;
	}
}
