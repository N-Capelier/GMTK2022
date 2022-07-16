using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	PlayerInstance player;

	private void Start()
	{
		player = PlayerInstance.Instance;
	}

	private void Update()
	{
		UpdateRotation(player.transform.position);
	}

	void UpdateRotation(Vector3 playerPosition)
	{
		playerPosition.y = transform.position.y;
		transform.LookAt(playerPosition);
	}
}