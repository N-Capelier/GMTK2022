using UnityEngine;

public abstract class Weapon : ScriptableObject
{
	public string weaponName;

	public int maxBullets;
	[HideInInspector] public int currentBullets;

	public float cooldown;

	public abstract void Shoot();

	public (Enemy enemy, Vector3 point) RaySensor(Vector3 origin, Vector3 direction, LayerMask layerMask)
	{
		RaycastHit hit;

		if(Physics.Raycast(origin, direction, out hit, 20f, layerMask))
		{
			if(hit.transform.CompareTag("Enemy"))
			{
				return (hit.transform.GetComponent<Enemy>(), hit.point);
			}
		}

		return (null, Vector3.zero);
	}
}