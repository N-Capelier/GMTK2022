using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

	// UI elements
	public GameObject gunUi;
	public GameObject gunPoint;
	public Animator gunUiAnimator;
	public GameObject handUi;
	public Animator handUiAnimator;
	public GameObject reloadDice;
	public Animator reloadDiceAnimator;
	public GameObject shotFx;
	public Animator shotFxAnimator;
	public GameObject crossHair;
	private Vector2 crossHairOriginalSize;

	private void Start()
	{
		shootCooldownTimer = new Clock();
		shootCooldownTimer.ClockEnded += ShootCooldownTimer_ClockEnded;
		SetWeapon(defaultWeapon);
		crossHairOriginalSize = crossHair.transform.localScale;


	}

	void ShootWeapon()
	{
		Debug.Log("Shooting");
		EquipedWeapon.Shoot(cameraTransform.position, cameraTransform.forward, enemyLayerMask);
		EquipedWeapon.currentBullets--;
		//VISUALS
		ShotFeedback();
		CrosshairFeedback();

		if (EquipedWeapon.currentBullets <= 0)
		{
			for (int i = 0; i < weapons.Length - 1; i++)
			{
				weapons[i] = weapons[i + 1];
				//StartCoroutine(reloadAnimation(weapons[i]));
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

	public float reloadTime;

	public IEnumerator reloadAnimation(Weapon weapon)
    {
		//HAND ANIM
		handUiAnimator.Play("hand_reload");

		//ENABLE IMAGE
		reloadDice.GetComponent<Image>().enabled = true;
		//STOP TWEEN
		//TWEEN SIZE
		reloadDice.LeanScale(new Vector3(1.5f,1.5f),reloadTime/5).setLoopPingPong();
		//SET DICE POS
		Vector3 handPos = handUi.transform.position;
		reloadDice.transform.position = handPos;
		//SET DICE ANIM
		reloadDiceAnimator.Play(weapon.weaponName);
		//MOVE DICE UP
		reloadDice.LeanMoveY(handPos.y + 1000, reloadTime * 0.2f);
		//ROTATE DICE ANIM
		for (int i = 0;i < 50; i++)
        {
			reloadDice.LeanRotateZ(reloadDice.transform.localRotation.z + 40 * i, 0.05f);
			yield return new WaitForSeconds(reloadTime * 0.2f / 50);
		}
		yield return new WaitForSeconds(0.6f * reloadTime);
		//SET DICE POS
		Vector3 gunPos =  gunUi.transform.position;
		gunPos.y = gunPos.y + 1000;
		reloadDice.transform.position = gunPos;
		//SET GUN ANIM
		gunUiAnimator.Play("gun_reload");
		//MOVE DICE DOWN
		reloadDice.LeanMoveY(reloadDice.transform.position.y - gunPos.y * 0.7f, reloadTime *0.2f);
		//ROTATE DICE
		for (int i = 0; i < 50; i++)
		{
			reloadDice.LeanRotateZ(reloadDice.transform.localRotation.z + 40 * i, 0.05f);
			yield return new WaitForSeconds(reloadTime *0.2f/60);
		}
		//SET GUN ANIM
		gunUiAnimator.Play("gun_reload2");
		// SET INACTIVE
		reloadDice.GetComponent<Image>().enabled = false;
		yield return null;
    }

	void CrosshairFeedback()
    {
		crossHair.transform.localScale = crossHairOriginalSize;
		crossHair.LeanRotateZ(Random.Range(90,360), 0.05f).setLoopPingPong(1);
		crossHair.LeanScale(new Vector3(crossHairOriginalSize.x * Random.Range(1.2f, 2f), crossHairOriginalSize.y * Random.Range(1.2f, 2f),1),0.06f).setLoopPingPong(1);
    }

	private int shotFxLoop = 1;

	void ShotFeedback()
    {
		//FX OF THE SHOT
		gunUiAnimator.Play("gun_shoot");
		shotFx.transform.position = gunPoint.transform.position;
		shotFxAnimator.Play("explo" + shotFxLoop.ToString());
		shotFx.transform.Rotate(0, 0, Random.Range(0, 360));
		CrosshairFeedback();
		//RESET
		if (shotFxLoop == 3)
		{
			shotFxLoop = 1;
		}
		//INCREASE
		else
		{
			shotFxLoop++;
		}
	}
}