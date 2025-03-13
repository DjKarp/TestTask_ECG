using UnityEngine;
using System.Collections;

public class CannonTower : Tower, IRotable
{
	[SerializeField] protected Transform shootPoint;
	[SerializeField] protected Transform _RotateGO;
	[SerializeField] protected Transform _RotateVertGO;

	// Данные скорости поворота башни, которая может вращаться и значение упреждения. Оставил здесь, чтобы не изменяли. 
	private float _rotationSpeed = 150.0f;
	private float uprezdenie = 4.5f;

	// Стараюсь все поля кештровать. А не создавать в каждом кадре новые объекты. 
	private Vector3 direction;
	private Quaternion rotateToActual;
	private Transform rotateTarget;
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
		bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.localRotation);

		// Можно создать более специализированный пул для объектов GetComponent, чтобы сразу можно было обращаться к Projectile. Чтобы не GetComponent'тить
		CannonProjectile projectile = bullet.GetComponent<CannonProjectile>();
		projectile.SetObjectPool(m_ObjectPool);
		projectile.AddRelativeForce(shootPoint.position - RotateVertGO.position);
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
		direction = rotateTarget.position - RotateHorizGO.position;
		rotateToActual = Quaternion.LookRotation(direction);

		// Добавляем упреждение по Y - для полёта снаряда и по X - для высоты Monster, чтобы стреляла в середину
		rotateToActual *= Quaternion.AngleAxis(-uprezdenie, Vector3.up);
		rotateToActual *= Quaternion.AngleAxis(uprezdenie, Vector3.right);

		//Поворачиваем разные Child по разным осям
		RotateHorizGO.rotation = Quaternion.RotateTowards(RotateHorizGO.rotation, new Quaternion(0.0f, rotateToActual.y, rotateToActual.z, rotateToActual.w), RotationSpeed * Time.deltaTime);
		RotateVertGO.rotation = Quaternion.RotateTowards(RotateVertGO.rotation, new Quaternion(rotateToActual.x, RotateHorizGO.rotation.y, RotateHorizGO.rotation.z, RotateHorizGO.rotation.w), RotationSpeed * Time.deltaTime);
	}

	private void FindTarget()
    {
		rotateTarget = Spawner.instance.FindClosestEnemyTR();
    }
}