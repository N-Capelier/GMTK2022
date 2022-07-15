using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
	bool canShoot = true;
	Clock shootCooldownTimer;

	private void Start()
	{
		shootCooldownTimer = new Clock();
		shootCooldownTimer.ClockEnded += ShootCooldownTimer_ClockEnded;
	}

	private void ShootCooldownTimer_ClockEnded()
	{
		canShoot = true;
	}

	void OnShoot(InputValue value)
	{
		if (!canShoot)
			return;

		canShoot = false;

		Debug.Log("Shoot");

		shootCooldownTimer.SetTime(2f);
	}


}