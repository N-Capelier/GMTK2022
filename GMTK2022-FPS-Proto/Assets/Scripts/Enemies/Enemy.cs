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

	public GameObject luckFx, dieFx;
	float baseSpeed = 3f;
	float maxSpeed = 4f;
	float nineMinutes = 60f * 9f;

	//SOUND
	AudioManager audioManager;

	private GameObject render;
	public GameObject remains;

	private void Start()
	{
		player = PlayerInstance.Instance;

		currentHealthPoints = maxHealthPoints;

		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

		StartCoroutine(LerpSpeed());
		render = gameObject.transform.GetChild(0).gameObject;
		StartCoroutine(mirrorSelf());
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
			Instantiate(remains, new Vector3(transform.position.x,transform.position.y - 0.25f,transform.position.z), Quaternion.identity);
		}
		audioManager.PlaySoundVariant(5, transform.position);
		Destroy(gameObject);
	}

	IEnumerator mirrorSelf()
	{
		yield return new WaitForSeconds(0.6f);
		render.transform.localScale = new Vector3(-render.transform.localScale.x, render.transform.localScale.y, render.transform.localScale.z);
		StartCoroutine(mirrorSelf());
	}
}