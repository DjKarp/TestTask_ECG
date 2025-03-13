using UnityEngine;
using System.Collections;

public class CannonTower : Tower, IRotable
{
	[SerializeField] protected Transform shootPoint;
	[SerializeField] protected Transform _RotateGO;
	[SerializeField] protected Transform _RotateVertGO;

	// Данные скорости поворота башни, которая может вращаться и значение упреждения. Оставил здесь, чтобы не изменяли. 
	private float _rotationSpeed = 150.0f;
	private float offset = 3.0f;

	// Стараюсь все поля кештровать. А не создавать в каждом кадре новые объекты. 
	private Vector3 direction;
	private Quaternion rotateToActual;
	private Transform rotateTarget;
	private GameObject bullet;

	public float RotationSpeed { get { return _rotationSpeed; } }
	public Transform RotateHorizGO { get { return _RotateGO; } }
	public Transform RotateVertGO { get { return _RotateVertGO; } }

	// TODO 4
	private float angle = 45.0f;
	private float g = Physics.gravity.y;
	private Vector3 towerUpDir;

	private void Awake()
	{
		m_TowersScriptableObject = Resources.Load<TowersScriptableObject>("ScriptableObject/CannonTowerData");
		Init();
	}

	public override void Shoot(GameObject Target, ObjectPool ObjectPool)
	{
		bullet = m_ObjectPool.Get();
		bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.localRotation);

		// Можно создать более специализированный пул для объектов GetComponent, чтобы сразу можно было обращаться к Projectile. Чтобы не GetComponent'тить
		CannonProjectile projectile = bullet.GetComponent<CannonProjectile>();
		projectile.SetObjectPool(m_ObjectPool);
		//projectile.AddRelativeForce(shootPoint.position - RotateVertGO.position); // Отключено для TODO 4
		// TODO 4 - вычисления нашёл в интернете и исправил для нашего случая. пример того, что умею искать и пользуюсь поиском.  Url: https://unityhub.ru/guides/sozdayom-ballisticheskuyu-traektoriyu-dvizheniya-obekta_31
		
		towerUpDir = GetDirection();				
		float x = towerUpDir.magnitude;
		float angleRadians = angle * Mathf.PI / 180;
		float v2 = (g * x * x) / (2 * (towerUpDir.y - Mathf.Tan(angleRadians) * x) * Mathf.Pow(Mathf.Cos(angleRadians), 2));
		float v = Mathf.Sqrt(Mathf.Abs(v2));

		projectile.AddRelativeForce(shootPoint.forward, v);		
	}

	private void LateUpdate()
    {
		// Если цель мертва или убежала (выключена), то ищем новую цель
		if (rotateTarget == null || !rotateTarget.gameObject.activeSelf)
			FindTarget();
		else
			RotateOnTarget();
	}

    public void RotateOnTarget()
    {
		direction = GetPositionWhitOffset() - RotateHorizGO.position;
		rotateToActual = Quaternion.LookRotation(direction);		

		//Поворачиваем разные Child по разным осям
		RotateHorizGO.rotation = Quaternion.RotateTowards(RotateHorizGO.rotation, new Quaternion(0.0f, rotateToActual.y, rotateToActual.z, rotateToActual.w), RotationSpeed * Time.deltaTime);
		RotateVertGO.rotation = Quaternion.RotateTowards(RotateVertGO.rotation, new Quaternion(rotateToActual.x, RotateHorizGO.rotation.y, RotateHorizGO.rotation.z, RotateHorizGO.rotation.w), RotationSpeed * Time.deltaTime);
	}

	private void FindTarget()
    {
		rotateTarget = Spawner.instance.FindClosestEnemyTR();
    }

	Vector3 dir = new Vector3();
	private Vector3 GetDirection()
    {
		dir = GetPositionWhitOffset() - shootPoint.position;
		return new Vector3(dir.x, 0f, dir.z);
	}
	private Vector3 GetPositionWhitOffset()
    {
		return new Vector3(rotateTarget.position.x - offset, rotateTarget.position.y, rotateTarget.position.z);
	}
}