using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using BaseGame.Assets;

public class Shoot : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public int _damage = 10;
	public float _shootSpeed = 20f;

	void Start(){

	}

	// Update is called once per frame
	[Command]
	public void CmdFire () {
		// Create the Bullet from the Bullet Prefab
    	var bullet = (GameObject)Instantiate (
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

    	// Add velocity to the bullet
    	bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _shootSpeed;
		bullet.GetComponent<Bullet>().Damage = _damage;
		NetworkServer.Spawn(bullet);
    	// Destroy the bullet after 2 seconds
    	Destroy(bullet, 3.0f);
	}
}
