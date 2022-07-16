using UnityEngine;

public abstract class Weapon : ScriptableObject
{
	public string weaponName;

	public int maxBullets;
	[HideInInspector] public int currentBullets;

	public float cooldown;

	public float inaccuracy;

	public abstract void Shoot(Vector3 origin, Vector3 direction, LayerMask layerMask);

	public HitInfo RaySensor(Vector3 origin, Vector3 direction, LayerMask layerMask)
	{
		direction.x += Random.Range(-inaccuracy, inaccuracy);
		direction.y += Random.Range(-inaccuracy, inaccuracy);
		direction.z += Random.Range(-inaccuracy, inaccuracy);

		RaycastHit hit;

		if (Physics.Raycast(origin, direction, out hit, 20f, layerMask))
		{
			return new HitInfo(hit.transform.GetComponent<Enemy>(), hit.point);
		}
		Debug.DrawRay(origin, direction * 20f, Color.red, 2f);

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