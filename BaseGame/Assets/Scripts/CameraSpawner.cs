using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraSpawner : NetworkBehaviour {

	public GameObject cameraPrefab;
	// Use this for initialization
	public override void OnStartLocalPlayer(){
		var cam = Instantiate(cameraPrefab);
		cam.GetComponent<CameraControl>().player = transform;
	}
}
