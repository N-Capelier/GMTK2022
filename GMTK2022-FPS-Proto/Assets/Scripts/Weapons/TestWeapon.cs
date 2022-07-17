using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Test Weapon", fileName = "New Weapon", order = 50)]
public class TestWeapon : Weapon
{
	public GameObject exploFx,bloodFx;
	public override void Shoot(Vector3 origin, Vector3 direction, LayerMask layerMask)
	{
		HitInfo hit = RaySensor(origin, direction, layerMask);

		if (hit.enemy != null)
        {
			hit.enemy.ReceiveDamage(damage);
			Instantiate(exploFx, hit.point, Quaternion.identity);
			Instantiate(bloodFx, hit.point, Quaternion.identity);
		}
		else if (hit.enemy = null)
		{
			Instantiate(exploFx, hit.point, Quaternion.identity);
		}
	}
}