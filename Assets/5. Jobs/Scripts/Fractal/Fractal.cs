using UnityEngine;

public abstract class Fractal : MonoBehaviour
{
    [SerializeField, Range(1, 8)]
    internal int depth = 4;
    internal int maxDepth = 8;
    internal virtual void UpdateFractal() { }
}