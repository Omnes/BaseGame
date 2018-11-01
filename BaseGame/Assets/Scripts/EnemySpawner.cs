using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {
	public GameObject _enemyPrefab;
	public Transform _spawnpoint;

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter(){
		CmdSpawnEnemy();
	}

	[Command]
	public void CmdSpawnEnemy(){
		// Create the Bullet from the Bullet Prefab
    	var enemy = (GameObject)Instantiate (
			_enemyPrefab,
			_spawnpoint.position,
			_spawnpoint.rotation);

		NetworkServer.Spawn(enemy);
	}
}
