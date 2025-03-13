using UnityEngine;

// Интерфейс для башен, которые поворачиваются
interface IRotable
{
    public float RotationSpeed { get; }

    // Отдельный объект для поворота по горизонтале
    public Transform RotateHorizGO { get; }
    // Другой объект для поворота по вертикали
    public Transform RotateVertGO { get; }

    public void RotateOnTarget();
}
