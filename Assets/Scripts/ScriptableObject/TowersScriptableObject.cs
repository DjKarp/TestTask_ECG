using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/CreateTowerData", order = 10)]
public class TowersScriptableObject : ScriptableObject
{
    public float ShootInterval = 1.0f;
    public float Range = 10.0f;
    public float LastShotTime = -0.5f;
    public GameObject ProjectilePrefab;
}
