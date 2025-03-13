using UnityEngine;
using System.Collections;

public class GuidedProjectile : Projectile
{
	public GameObject target;
	private Vector3 translation;

    public override void Move()
	{
		if (!target.activeSelf)
			DestroyProjectile();
		else
		{
			translation = target.transform.position - transform.position;
			if (translation.magnitude > _speed)
				translation = translation.normalized * _speed;
			transform.Translate(translation);
		}
	}

	public void SetTarget(GameObject Target)
    {
		target = Target;
	}
}
