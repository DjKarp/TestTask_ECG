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
	}

	public override void Move()
	{
	}

	public void AddRelativeForce(Vector3 direction)
    {
		m_Rigidbody.velocity = direction * _speed;
	}
}
