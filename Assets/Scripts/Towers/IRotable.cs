using UnityEngine;

interface IRotable
{
    public float RotationSpeed { get; }
    public Transform RotateHorizGO { get; }

    public Transform RotateVertGO { get; }

    public void RotateOnTarget();
}
