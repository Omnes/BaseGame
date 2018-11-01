using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using BaseGame.Assets;

public class PlayerInput : NetworkBehaviour {

	public IMotor _tankMotor;
	private Shoot _shoot;
	// Use this for initialization
	void Start () {
		_tankMotor = GetComponent<RigidbodyMotor>();
		_shoot = GetComponent<Shoot>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer)
			return;

		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		var inputVector = new Vector2(horizontal, vertical);

		_tankMotor.InputVector = inputVector;

		var camHorizontal = (Input.GetKey(KeyCode.Q) ? 1 : 0) + (Input.GetKey(KeyCode.E) ? -1 : 0);
		Camera.main.GetComponent<CameraControl>().HandleInput(Vector2.right * camHorizontal);

		if(Input.GetKeyDown(KeyCode.Space)){
			_shoot.CmdFire();
		}
	}
}
