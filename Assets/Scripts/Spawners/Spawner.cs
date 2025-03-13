using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	public static Spawner instance = null;

	private float spawnInterval = 3;
	[SerializeField] private GameObject moveTarget;

	private float lastSpawn = -1;

	private ObjectPool m_ObjectPool;
	private GameObject tempNewMonster;
	private Monster m_Monster;

	private float tempDistance;

	private void Awake()
    {
		if (instance == null)
			instance = this;
		else if (instance == this)
			Destroy(gameObject);

		// Теперь нам нужно указать, чтобы объект не уничтожался
		// при переходе на другую сцену игры
		DontDestroyOnLoad(gameObject);

		tempNewMonster = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		tempNewMonster.transform.position = transform.position;
		Rigidbody m_Rigidbody = tempNewMonster.AddComponent<Rigidbody>();
		m_Rigidbody.useGravity = false;
		m_Monster = tempNewMonster.AddComponent<Monster>();
		m_Monster.SetMonsterSpawner(this);

		m_ObjectPool = new ObjectPool(tempNewMonster, 1);

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
	public Transform FindClosestEnemyTR()
	{
		return tempNewMonster != null ? tempNewMonster.transform : null;
	}
}