using UnityEngine;
using System.Collections;

public class CannonTower : Tower, IRotable
{
	[SerializeField] protected Transform shootPoint;
	[SerializeField] protected Transform _RotateGO;
	[SerializeField] protected Transform _RotateVertGO;
	private float _rotationSpeed = 100.0f;
	private float uprezdenie = 4.0f;

	private Vector3 direction;
	private Quaternion rotateToActual;
	private Transform rotateTarget;
	private Spawner m_Spawner;
	private GameObject bullet;

	public float RotationSpeed { get { return _rotationSpeed; } }
	public Transform RotateHorizGO { get { return _RotateGO; } }
	public Transform RotateVertGO { get { return _RotateVertGO; } }

	private void Awake()
	{
		m_TowersScriptableObject = Resources.Load<TowersScriptableObject>("ScriptableObject/CannonTowerData");
		Init();
	}

	public override void Shoot(GameObject Target, ObjectPool ObjectPool)
	{
		bullet = m_ObjectPool.Get();
		bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
		CannonProjectile projectile = bullet.GetComponent<CannonProjectile>();
		projectile.SetObjectPool(m_ObjectPool);
		projectile.AddRelativeForce((shootPoint.position - RotateVertGO.position).normalized);
	}

    private void LateUpdate()
    {
		if (rotateTarget == null || !rotateTarget.gameObject.activeSelf)
			FindTarget();
		else
			RotateOnTarget();
	}

    public void RotateOnTarget()
    {
		direction = rotateTarget.position - RotateHorizGO.position;
		rotateToActual = Quaternion.LookRotation(direction);

		// Добавляем упреждение
		//rotateToActual *= Quaternion.AngleAxis(-uprezdenie, Vector3.up);
		rotateToActual *= Quaternion.AngleAxis(uprezdenie, Vector3.right);

		RotateHorizGO.rotation = Quaternion.RotateTowards(RotateHorizGO.rotation, new Quaternion(0.0f, rotateToActual.y, rotateToActual.z, rotateToActual.w), RotationSpeed * Time.deltaTime);
		RotateVertGO.rotation = Quaternion.RotateTowards(RotateVertGO.rotation, new Quaternion(rotateToActual.x, RotateHorizGO.rotation.y, RotateHorizGO.rotation.z, RotateHorizGO.rotation.w), RotationSpeed * Time.deltaTime);
	}

	private void FindTarget()
    {
		rotateTarget = Spawner.instance.FindClosestEnemyTR();
    }
}