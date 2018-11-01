using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int Damage {get;set;}
    
    void OnCollisionEnter(Collision collision)
    {
		var hit = collision.gameObject;
		var health = hit.GetComponentInParent<Health>();
		if (health != null)
		{
			health.TakeDamage(Damage);
		}
        Destroy(gameObject);
    }
}
