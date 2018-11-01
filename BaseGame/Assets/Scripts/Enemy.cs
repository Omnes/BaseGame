using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using BaseGame.Assets.Scripts;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {

	public Transform _tower;
	private NavMeshAgent _navAgent;

	[SyncVar]
	private Transform _target;
	public Transform Target {
		get{
			if(_target == null && isServer){
				_target = Util.FindClosestGameObjectWithTag(transform.position,"Player")?.transform;
			}
			return _target;
		}
	}

	// Use this for initialization
	void Start () {
		_navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () { 
		if(Target != null){
			_navAgent.SetDestination(Target.position);
			_tower.rotation = Quaternion.LookRotation(transform.position - Target.position);
		}
	}
}
