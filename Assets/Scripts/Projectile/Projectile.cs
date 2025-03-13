using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float _speed;
    protected int _damage;

    protected ProjectileScriptableObject m_ProjectileSO;
    protected ObjectPool m_ObjectPool;

    // Всю инициализацию я стараюсь проводить в Awake, и не использую Start
    protected virtual void Awake()
    {
        Init();
    }

    protected void Init()
    {
        // Из ресурсов загружаем нужный ScriptableObject. Здесь автоматически создаём имя из названия класса. 
        m_ProjectileSO = Resources.Load<ProjectileScriptableObject>("ScriptableObject/" + this.GetType().FullName + "Data");
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

    private void OnEnable()
    {
        StartCoroutine(DeathCount());
    }
    // Использована для примера Coroutine для выключения Projectile через небольшое время, если он не столкнулся ни с кем. 
    IEnumerator DeathCount()
    {
        yield return new WaitForSeconds(5.0f);
        DestroyProjectile();
    }
}
