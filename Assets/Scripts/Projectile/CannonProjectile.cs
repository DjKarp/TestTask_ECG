using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile
{
    private void Awake()
	{
		m_ProjectileSO = Resources.Load<ProjectileScriptableObject>("ScriptableObject/CannonProjectileData");
		Init();
	}

	public override void Move()
	{
		transform.Translate(transform.forward * _speed);
	}
}
