using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject enemyPrefab;

	[SerializeField] float baseTimeBetweenSpawn;
	[SerializeField] float maxTimeBetweenSpawn;

	float timeBetweenSpawn;
	float nineMinutes;

	Clock spawnTimer;

	private void Start()
	{
		nineMinutes = 60f * 9f;
		timeBetweenSpawn = baseTimeBetweenSpawn;

		spawnTimer = new Clock();
		spawnTimer.ClockEnded += SpawnTimer_ClockEnded;
		spawnTimer.SetTime(timeBetweenSpawn);

		LerpTimeBetweenSpawn();
	}

	private void SpawnTimer_ClockEnded()
	{
		SpawnEnemy();
		spawnTimer.SetTime(timeBetweenSpawn);
	}

	void SpawnEnemy()
	{
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
