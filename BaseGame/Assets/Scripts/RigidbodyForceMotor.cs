using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using BaseGame.Assets;

public class RigidbodyForceMotor : NetworkBehaviour, IMotor {

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
        Vector3 movement = transform.forward * _velocity;
        MoveByForce(movement, _rigidbody);

		float turn = InputVector.x * _turnspeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
        _rigidbody.MoveRotation (_rigidbody.rotation * turnRotation);

		_velocity *= 1f - _friction * (1f - Mathf.Abs(InputVector.y));
		//_rigidbody.AddForce(Vector3.down * _rigidbody.mass);
	}

	void MoveByForce(Vector3 targetVelocity, Rigidbody rigidBody){
		var velocity = rigidBody.velocity;
		var delta = targetVelocity - velocity;
		var clampedVelocityChange = ClampAnd0Y(delta, -_acceleration*2, _acceleration*2);
		rigidBody.AddForce(clampedVelocityChange, ForceMode.VelocityChange);
	}

	Vector3 ClampAnd0Y(Vector3 vector, float min, float max){
		return new Vector3(
			Mathf.Clamp(vector.x, min, max),
			Mathf.Clamp(0, min, max),
			Mathf.Clamp(vector.z, min, max)
		);
	}

}
