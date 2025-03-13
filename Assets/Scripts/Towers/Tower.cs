using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Почти все поля и методы остались в абстрактном классе. И для показа возможного расширения этотго класса, создан интерфейс для поворотных башен. 
public abstract class Tower : MonoBehaviour
{
    protected float _shootInterval;
    protected float _range;
    protected float _lastShotTime;
    protected GameObject _projectilePrefab;

    protected ObjectPool m_ObjectPool;
    protected TowersScriptableObject m_TowersScriptableObject;

    protected void Init()
    {
        // Инициализируем параметры каждой башни, используя ScriptableObject, для примера. Так ГеймДизайнеру удобно тестировать, создавая разные ScriptableObject.
        Init(m_TowersScriptableObject.ShootInterval, m_TowersScriptableObject.Range, m_TowersScriptableObject.LastShotTime, m_TowersScriptableObject.ProjectilePrefab);

        m_ObjectPool = new ObjectPool(m_TowersScriptableObject.ProjectilePrefab, 1);
    }

    protected void Init(float ShootInterval, float Range, float LastShotTime, GameObject ProjectilePrefab)
    {
        _shootInterval = ShootInterval;
        _range = Range;
        _lastShotTime = LastShotTime;
        _projectilePrefab = ProjectilePrefab;
    }

    public abstract void Shoot(GameObject Target, ObjectPool ObjectPool);

    void Update()
    {
        CalcShootingTime();
    }

    protected void CalcShootingTime()
    {
        if (_projectilePrefab != null)
        {
            foreach (var monster in FindObjectsOfType<Monster>())
            {
                if (Vector3.Distance(transform.position, monster.transform.position) > _range || _lastShotTime + _shootInterval > Time.time)
                    continue;

                _lastShotTime = Time.time;

                // Tower Shoot
                Shoot(monster.gameObject, m_ObjectPool);
            }
        }
    }
}
