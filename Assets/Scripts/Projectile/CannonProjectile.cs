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
	}

	public override void Move()
	{
	}

	public void AddRelativeForce(Vector3 direction, float overrideSpeed = -1.0f)
    {
		m_Rigidbody.velocity = direction * (overrideSpeed < 0 ? _speed : overrideSpeed);
	}
}
