using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using BaseGame.Assets;

public class RigidbodyMotor : NetworkBehaviour, IMotor {

	public float _friction = 0.02f;
	public float _acceleration = 1f;
	public float _maxSpeed = 8f;
	public float _turnspeed = 3f;
	public AnimationCurve accelerationCurve;

	public Vector2 InputVector {get; set;}

	public float Velocity => _velocity;

	public float _velocity;

	private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!isLocalPlayer)
			return;

		var accelerationMultiplier = accelerationCurve.Evaluate(_velocity/_maxSpeed);
		_velocity += _acceleration * accelerationMultiplier * InputVector.y * Time.deltaTime;
        Vector3 movement = transform.forward  * _velocity * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);

		float turn = InputVector.x * _turnspeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
        _rigidbody.MoveRotation (_rigidbody.rotation * turnRotation);

		_velocity *= 1f - _friction * (1f - Mathf.Abs(InputVector.y));
	}

}