using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using BaseGame.Assets;

public class FixedMotor : NetworkBehaviour, IMotor {

	public float _friction = 0.02f;
	public float _acceleration = 1f;
	public float _maxSpeed = 8f;
	public float _turnspeed = 3f;
	public AnimationCurve accelerationCurve;

	public Vector2 InputVector {get; set;}
	private float Direction {get; set;}
	public float Velocity {get; set;}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!isLocalPlayer)
			return;

		Direction += _turnspeed * InputVector.x;
		var acceleration = _acceleration * accelerationCurve.Evaluate(Velocity/_maxSpeed);
		Velocity += InputVector.y * acceleration;

		transform.localPosition += transform.forward * Velocity * Time.deltaTime;
		transform.rotation = Quaternion.AngleAxis(Direction, Vector3.up);

		Velocity *= 1f - _friction * (1f - Mathf.Abs(InputVector.y));
	}

}
