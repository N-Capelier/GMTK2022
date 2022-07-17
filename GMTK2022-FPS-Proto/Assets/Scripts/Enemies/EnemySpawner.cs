using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject enemyPrefab;

	[SerializeField] float baseTimeBetweenSpawn;
	[SerializeField] float maxTimeBetweenSpawn;

	float timeBetweenSpawn;
	float nineMinutes = 60f * 9f;

	Clock spawnTimer;

	WaitForSeconds spawnDelay = new WaitForSeconds(1f);

	private void Start()
	{
		timeBetweenSpawn = baseTimeBetweenSpawn;

		spawnTimer = new Clock();
		spawnTimer.ClockEnded += SpawnTimer_ClockEnded;
		spawnTimer.SetTime(timeBetweenSpawn);

		StartCoroutine(LerpTimeBetweenSpawn());
	}

	private void SpawnTimer_ClockEnded()
	{
		StartCoroutine(SpawnTwoEnemies());
		spawnTimer.SetTime(timeBetweenSpawn);
	}

	IEnumerator SpawnTwoEnemies()
	{
		Instantiate(enemyPrefab, transform.position, Quaternion.identity);
		yield return spawnDelay;
		Instantiate(enemyPrefab, transform.position, Quaternion.identity);
	}

	IEnumerator LerpTimeBetweenSpawn()
	{
		float elapsedTime = 0f;
		float completion;

		while(elapsedTime < nineMinutes)
		{
			elapsedTime += Time.deltaTime;
			completion = elapsedTime / nineMinutes;
			timeBetweenSpawn = Mathf.Lerp(baseTimeBetweenSpawn, maxTimeBetweenSpawn, completion);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForEndOfFrame();
	}
}
