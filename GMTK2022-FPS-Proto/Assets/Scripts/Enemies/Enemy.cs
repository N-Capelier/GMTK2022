using System.Collections;
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

	float baseSpeed = 3f;
	float maxSpeed = 4f;
	float nineMinutes = 60f * 9f;

	private void Start()
	{
		player = PlayerInstance.Instance;

		currentHealthPoints = maxHealthPoints;

		StartCoroutine(LerpSpeed());
	}

	private void Update()
	{
		UpdateDestination();
	}

	protected abstract void UpdateDestination();

	IEnumerator LerpSpeed()
	{
		float elapsedTime = 0f;
		float completion;

		while (elapsedTime < nineMinutes)
		{
			elapsedTime += Time.deltaTime;
			completion = elapsedTime / nineMinutes;
			agent.speed = Mathf.Lerp(baseSpeed, maxSpeed, completion);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForEndOfFrame();
	}

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
		int dropChance = Random.Range(0, 100);

		if(dropChance < dropPercentage)
		{
			int dropIndex = Random.Range(0, pickupPrefabs.Length);
			Instantiate(pickupPrefabs[dropIndex], transform.position, Quaternion.identity);
		}

		Destroy(gameObject);
	}
}