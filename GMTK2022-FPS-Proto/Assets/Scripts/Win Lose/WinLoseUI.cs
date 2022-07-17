using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
	[SerializeField] string gameScene;

	bool canPlay = false;

	private void Start()
	{
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(2.5f);
		canPlay = true;
	}

	public void PlayAgainButton()
	{
		if(canPlay)
			SceneManager.LoadScene(gameScene);
	}
}