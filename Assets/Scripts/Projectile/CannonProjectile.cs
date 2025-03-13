using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile
{
	protected Rigidbody m_Rigidbody;

	// Переопределяем Awake для кеширования компонента Rigidbody
	protected override void Awake()
	{
		base.Awake();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Rigidbody.useGravity = true;
		m_Rigidbody.mass *= 100.0f;
	}

	public override void Move()
	{
	}

	public void AddRelativeForce(Vector3 direction)
    {
		m_Rigidbody.velocity = direction * _speed / 12.0f;
	}
}
