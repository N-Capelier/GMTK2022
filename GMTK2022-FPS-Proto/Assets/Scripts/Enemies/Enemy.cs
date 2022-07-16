using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
	public int maxHealthPoints;
	[HideInInspector] public int currentHealthPoints;

	public int damages;

	protected PlayerInstance player;

	public NavMeshAgent agent;

	private void Start()
	{
		player = PlayerInstance.Instance;

		currentHealthPoints = maxHealthPoints;
	}

	private void Update()
	{
		UpdateRendererRotation(player.transform.position);
		UpdateDestination();
	}

	protected abstract void UpdateDestination();

	public void ReceiveDamage(int amount)
	{
		if(currentHealthPoints - amount <= 0)
		{
			currentHealthPoints = 0;
			Death();
		}
		else
		{
			currentHealthPoints -= amount;
		}
	}

	public void Death()
	{
		Destroy(gameObject);
	}

	void UpdateRendererRotation(Vector3 playerPosition)
	{
		playerPosition.y = transform.position.y;
		transform.LookAt(playerPosition);
	}
}