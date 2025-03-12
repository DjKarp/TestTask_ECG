using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour 
{
	const float ReachDistance = 0.3f;

	protected GameObject moveTarget;
	protected float speed = 0.1f;
	[SerializeField] protected int maxHP = 30;
	[SerializeField] protected int HP;

    private void Awake()
    {
		Init();
	}

	public void Init()
    {
		HP = maxHP;
	}

	void Update () 
	{
		Move();
	}

	public void AddedDamage(int Damage)
    {
		HP -= Damage;

		if (HP <= 0)
			Destroy(gameObject);
	}

	public void SetMoveTarget(GameObject Target)
    {
		moveTarget = Target;
	}

    private void Move()
    {
		if (moveTarget != null)
        {
			if (Vector3.Distance(transform.position, moveTarget.transform.position) <= ReachDistance)
			{
				Destroy(gameObject);
				return;
			}

			var translation = moveTarget.transform.position - transform.position;
			if (translation.magnitude > speed)
				translation = translation.normalized * speed;
			transform.Translate(translation);
		}
	}
}