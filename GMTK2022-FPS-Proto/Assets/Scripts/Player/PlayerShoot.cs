using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
	bool canShoot = true;
	bool isShooting = false;
	Clock shootCooldownTimer;

	[SerializeField] Transform cameraTransform;
	[SerializeField] LayerMask enemyLayerMask;


	[Header("Serialized for debug")]
	[SerializeField] Weapon equipedWeapon;


	private void Start()
	{
		shootCooldownTimer = new Clock();
		shootCooldownTimer.ClockEnded += ShootCooldownTimer_ClockEnded;
	}

	void ShootWeapon()
	{
		Debug.Log("Shooting");
		equipedWeapon.Shoot(cameraTransform.position, cameraTransform.forward, enemyLayerMask);
	}

	private void ShootCooldownTimer_ClockEnded()
	{
		if (isShooting)
		{
			ShootWeapon();

			shootCooldownTimer.SetTime(equipedWeapon.cooldown);
		}
		else
		{
			canShoot = true;
		}
	}

	void OnShoot(InputValue value)
	{
		if (value.isPressed)
		{
			if (!canShoot)
				return;

			isShooting = true;
			canShoot = false;

			ShootWeapon();

			shootCooldownTimer.SetTime(equipedWeapon.cooldown);
		}
		else
		{
			isShooting = false;
		}

	}
}