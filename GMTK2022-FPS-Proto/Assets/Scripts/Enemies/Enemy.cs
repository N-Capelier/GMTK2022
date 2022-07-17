using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
	public int maxHealthPoints;
	[HideInInspector] public int currentHealthPoints;

	public int damages;

	[SerializeField] float dropPercentage;
	[SerializeField] GameObject[] pickupPrefabs;

	protected PlayerInstance player;

	public NavMeshAgent agent;

	public GameObject luckFx, dieFx;

	private void Start()
	{
		player = PlayerInstance.Instance;

		currentHealthPoints = maxHealthPoints;
	}

	private void Update()
	{
		UpdateDestination();
	}

	protected abstract void UpdateDestination();

	public void ReceiveDamage(int amount)
	{
		//CREATE FX
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
		int dropChance = Random.Range(0, 100);

		if(dropChance < dropPercentage)
		{
			int dropIndex = Random.Range(0, pickupPrefabs.Length);
			Instantiate(pickupPrefabs[dropIndex], transform.position, Quaternion.identity);
			Instantiate(luckFx, transform.position, Quaternion.identity);
        }
        else
		{
			Instantiate(dieFx, transform.position, Quaternion.identity);
		}

		Destroy(gameObject);
	}
}