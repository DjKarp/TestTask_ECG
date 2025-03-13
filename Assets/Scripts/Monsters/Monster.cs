using UnityEngine;
using System.Collections;

// При желании и этот класс легко сделать абстрактным. Но сейчас это не нужно, так как в задании один только вид Monster
public class Monster : MonoBehaviour 
{
	const float ReachDistance = 0.3f;

	protected float speed = 0.01f;
	[SerializeField] protected int maxHP = 30;
	[SerializeField] protected int HP;

	protected Spawner m_Spawner;

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
			DestroyMonster();
	}

	public void DestroyMonster()
    {
		m_Spawner.GetObjectPool().Release(gameObject.gameObject);
	}

    private void Move()
    {
		if (Vector3.Distance(transform.position, m_Spawner.GetMoveTargetPosition()) <= ReachDistance)
		{
			DestroyMonster();
			return;
		}

		var translation = m_Spawner.GetMoveTargetPosition() - transform.position;
		if (translation.magnitude > speed)
			translation = translation.normalized * speed;
		transform.Translate(translation);
	}

	public void SetMonsterSpawner(Spawner Spawner)
    {
		m_Spawner = Spawner;
	}
}