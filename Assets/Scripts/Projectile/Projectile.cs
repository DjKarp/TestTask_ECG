using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float _speed;
    protected int _damage;

    protected ProjectileScriptableObject m_ProjectileSO;
    protected ObjectPool m_ObjectPool;

    protected void Init()
    {
        Init(m_ProjectileSO.Speed, m_ProjectileSO.Damage);
    }

    protected void Init(float Speed, int Damage)
    {
        _speed = Speed;
        _damage = Damage;
    }

    protected void DestroyProjectile()
    {
        m_ObjectPool.Release(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.LogError(m_ProjectileSO.name + " -> Hit on Target");

        var monster = other.gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            monster.AddedDamage(_damage);
            DestroyProjectile();
        }
    }

    public abstract void Move();

    private void Update()
    {
        Move();
    }

    public void SetObjectPool(ObjectPool ObjectPool)
    {
        m_ObjectPool = ObjectPool;
    }
    public ObjectPool GetObjectPool()
    {
        return m_ObjectPool;
    }
}
