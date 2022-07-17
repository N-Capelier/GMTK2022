using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
	[SerializeField] string gameScene;

	bool canPlay = false;

	[SerializeField] bool isMainMenu = false;

	private void Start()
	{
		StartCoroutine(Wait());
		Cursor.lockState = CursorLockMode.None;
	}

	IEnumerator Wait()
	{
		float time = isMainMenu ? 0.6f : 1.8f;
		yield return new WaitForSeconds(time);
		canPlay = true;
	}

	public void PlayAgainButton()
	{
		if(canPlay)
			SceneManager.LoadScene(gameScene);
	}
}