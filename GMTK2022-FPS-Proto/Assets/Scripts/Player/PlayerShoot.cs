using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
	bool canShoot = true;
	bool isShooting = false;
	Clock shootCooldownTimer;

	[Header("References")]
	[SerializeField] Transform cameraTransform;
	[SerializeField] Weapon defaultWeapon;

	[Header("Params")]
	[SerializeField] LayerMask enemyLayerMask;

	Weapon[] weapons = new Weapon[4];
	Weapon EquipedWeapon { get => weapons[0]; }

	public delegate void UIEventHandler();
	public event UIEventHandler UpdateWeaponUI;

	private void Start()
	{
		shootCooldownTimer = new Clock();
		shootCooldownTimer.ClockEnded += ShootCooldownTimer_ClockEnded;

		SetWeapon(defaultWeapon);
	}

	void ShootWeapon()
	{
		Debug.Log("Shooting");
		EquipedWeapon.Shoot(cameraTransform.position, cameraTransform.forward, enemyLayerMask);
		EquipedWeapon.currentBullets--;

		if(EquipedWeapon.currentBullets <= 0)
		{
			for (int i = 0; i < weapons.Length - 1; i++)
			{
				weapons[i] = weapons[i + 1];
			}
			weapons[weapons.Length - 1] = Instantiate(defaultWeapon);
		}

		UpdateWeaponUI?.Invoke();
	}

	private void ShootCooldownTimer_ClockEnded()
	{
		if (isShooting)
		{
			ShootWeapon();

			shootCooldownTimer.SetTime(EquipedWeapon.cooldown);
		}
		else
		{
			canShoot = true;
		}
	}

	void SetWeapon(Weapon weapon)
	{
		if (weapon == null)
			throw new System.ArgumentNullException("New weapon is null.");

		for (int i = 0; i < weapons.Length; i++)
		{
			weapons[i] = Instantiate(weapon);
		}

		UpdateWeaponUI?.Invoke();
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

			shootCooldownTimer.SetTime(EquipedWeapon.cooldown);
		}
		else
		{
			isShooting = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.GetComponent<Pickup>())
			return;

		Pickup pickup = other.GetComponent<Pickup>();
		SetWeapon(pickup.weapon);
		Destroy(pickup.gameObject);
	}
}