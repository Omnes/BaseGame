using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour 
{
    public int maxHealth = 100;
	[SyncVar]
    private int _currentHealth;

	void Start(){
		_currentHealth = maxHealth;
	}

   	public void TakeDamage(int amount)
    {
		if (!isServer)
		{
			return;
		}
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            RpcKill();
        }
    }

	[ClientRpc]
	public void RpcKill(){
		Destroy(gameObject);
	}
}