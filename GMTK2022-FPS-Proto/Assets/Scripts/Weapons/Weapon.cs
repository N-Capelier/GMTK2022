using UnityEngine;

public abstract class Weapon : ScriptableObject
{
	public string weaponName;

	public Sprite sprite;

	public int maxBullets;
	[HideInInspector] public int currentBullets;

	public int damage;
	public float cooldown;
	public float inaccuracy;

	public abstract void Shoot(Vector3 origin, Vector3 direction, LayerMask layerMask, LayerMask defaultLayer);

	public HitInfo RaySensor(Vector3 origin, Vector3 direction, LayerMask layerMask, LayerMask defaultLayer)
	{
		direction.x += Random.Range(-inaccuracy, inaccuracy);
		direction.y += Random.Range(-inaccuracy, inaccuracy);
		direction.z += Random.Range(-inaccuracy, inaccuracy);

		RaycastHit hit;

		if (Physics.Raycast(origin, direction, out hit, 40f, layerMask))
		{
			Enemy enemy = hit.transform.GetComponent<Enemy>();

			if (enemy != null)
			{
				return new HitInfo(hit.transform.GetComponent<Enemy>(), hit.point);
			}
		}

		if(Physics.Raycast(origin, direction, out hit, 40f, defaultLayer))
		{
			return new HitInfo(null, hit.point);
		}

		return new HitInfo(null, Vector3.zero);
	}

	public struct HitInfo
	{
		public Enemy enemy;
		public Vector3 point;

		public HitInfo(Enemy enemy, Vector3 point)
		{
			this.enemy = enemy;
			this.point = point;
		}
	}
}