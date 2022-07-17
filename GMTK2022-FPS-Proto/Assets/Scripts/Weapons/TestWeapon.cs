using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Test Weapon", fileName = "New Weapon", order = 50)]
public class TestWeapon : Weapon
{
	public GameObject exploFx,bloodFx,plingFx;
	public override void Shoot(Vector3 origin, Vector3 direction, LayerMask layerMask, LayerMask defaultLayer)
	{
		HitInfo hit = RaySensor(origin, direction, layerMask, defaultLayer);

		if (hit.enemy != null)
        {
			hit.enemy.ReceiveDamage(damage);
			Instantiate(exploFx, hit.point, Quaternion.identity);
			Instantiate(bloodFx, hit.point, Quaternion.identity);
		}
		else if (hit.enemy == null && hit.point != Vector3.zero)
        {
            Instantiate(plingFx, hit.point, Quaternion.identity);
			//SFX
        }
    }
}