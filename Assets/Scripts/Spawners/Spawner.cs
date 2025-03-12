using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	public float spawnInterval = 3;
	public GameObject moveTarget;

	private float lastSpawn = -1;	

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
		var newMonster = GameObject.CreatePrimitive(PrimitiveType.Capsule);

		var r = newMonster.AddComponent<Rigidbody>();
		r.useGravity = false;

		newMonster.transform.position = transform.position;
		var monsterBeh = newMonster.AddComponent<Monster>();
		monsterBeh.SetMoveTarget(moveTarget);
	}
}