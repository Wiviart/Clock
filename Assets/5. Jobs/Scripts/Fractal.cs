using UnityEngine;

public abstract class Fractal : MonoBehaviour
{
    [SerializeField, Range(1, 8)]
    internal int depth = 4;
}