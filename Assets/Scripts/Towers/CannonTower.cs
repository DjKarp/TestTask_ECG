using UnityEngine;
using System.Collections;

public class CannonTower : Tower
{
	public Transform shootPoint;

	private void Awake()
	{
		m_TowersScriptableObject = Resources.Load<TowersScriptableObject>("ScriptableObject/CannonTowerData");
		Init();
	}

	public override void Shoot(GameObject Target, ObjectPool ObjectPool)
	{
		GameObject bullet = m_ObjectPool.Get();
		bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);

		Debug.LogError("CannonTower Shoot");
	}
}