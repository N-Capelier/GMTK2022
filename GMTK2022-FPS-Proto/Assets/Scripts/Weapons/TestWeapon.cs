using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Test Weapon", fileName = "New Weapon", order = 50)]
public class TestWeapon : Weapon
{
	public override void Shoot(Vector3 origin, Vector3 direction, LayerMask layerMask)
	{
		HitInfo hit = RaySensor(origin, direction, layerMask);

		if (hit.enemy == null)
			Debug.Log("No enemy hit.");
		else
			Debug.Log("Enemy hit.");
	}
}