using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile
{
	protected Rigidbody m_Rigidbody;

    private void Awake()
	{
		m_ProjectileSO = Resources.Load<ProjectileScriptableObject>("ScriptableObject/CannonProjectileData");
		Init();

		m_Rigidbody = GetComponent<Rigidbody>();
	}

	public override void Move()
	{
		
	}

	public void AddRelativeForce(Vector3 direction)
    {
		m_Rigidbody.AddRelativeForce(direction * 2000.0f, ForceMode.Acceleration);
	}
}
