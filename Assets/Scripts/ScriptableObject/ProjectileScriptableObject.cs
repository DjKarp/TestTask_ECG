using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/CreateProjectileData", order = 20)]
public class ProjectileScriptableObject : ScriptableObject
{
    public float Speed = 0.2f;
    public int Damage = 10;
}
