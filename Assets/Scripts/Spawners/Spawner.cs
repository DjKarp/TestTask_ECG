using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	private float spawnInterval = 3;
	[SerializeField] private GameObject moveTarget;

	private float lastSpawn = -1;

	private ObjectPool m_ObjectPool;
	private GameObject tempNewMonster;
	private Monster m_Monster;

	private void Awake()
    {
		GameObject tempMonsterGO = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		tempMonsterGO.transform.position = transform.position;
		Rigidbody m_Rigidbody = tempMonsterGO.AddComponent<Rigidbody>();
		m_Rigidbody.useGravity = false;
		m_Monster = tempMonsterGO.AddComponent<Monster>();
		m_Monster.SetMonsterSpawner(this);

		m_ObjectPool = new ObjectPool(tempMonsterGO, 1);

		if (moveTarget == null)
			moveTarget = GameObject.FindGameObjectWithTag("MoveTarget");
	}

    void Update () 
	{
		if (Time.time > lastSpawn + spawnInterval) 
		{
			SpawnMonster();
			lastSpawn = Time.time;
		}
	}

	private void SpawnMonster()
    {
		tempNewMonster = m_ObjectPool.Get();
		tempNewMonster.transform.position = transform.position;

		m_Monster = tempNewMonster.GetComponent<Monster>();
		m_Monster.SetMonsterSpawner(this);
		m_Monster.Init();		
	}

	public ObjectPool GetObjectPool()
    {
		return m_ObjectPool;
    }

	public Vector3 GetMoveTargetPosition()
    {
		return moveTarget.transform.position;
    }
}