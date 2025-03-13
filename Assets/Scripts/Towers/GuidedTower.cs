using UnityEngine;
using System.Collections;

public class GuidedTower : Tower
{
	protected GuidedProjectile m_Projectile;

	private void Awake()
	{
		m_TowersScriptableObject = Resources.Load<TowersScriptableObject>("ScriptableObject/GuidedTowerData");
		Init();
	}

	public override void Shoot(GameObject Target, ObjectPool ObjectPool)
	{
		var projectile = m_ObjectPool.Get();
		projectile.transform.SetPositionAndRotation(transform.position + Vector3.up * 1.5f, Quaternion.identity);
		m_Projectile = projectile.GetComponent<GuidedProjectile>();
		m_Projectile.SetTarget(Target);
		m_Projectile.SetObjectPool(m_ObjectPool);
	}
}