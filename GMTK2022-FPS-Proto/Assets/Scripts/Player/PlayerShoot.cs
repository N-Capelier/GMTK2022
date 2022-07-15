using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
	bool canShoot = true;
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

	private void ShootCooldownTimer_ClockEnded()
	{
		canShoot = true;
	}

	void OnShoot(InputValue value)
	{
		if (!canShoot)
			return;

		canShoot = false;

		equipedWeapon.Shoot(cameraTransform.position, transform.forward, enemyLayerMask);

		shootCooldownTimer.SetTime(equipedWeapon.cooldown);
	}
}