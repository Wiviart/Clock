using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] Transform pointPrefab;
    [SerializeField] int numberPoints = 11;
    Transform[] points;
    [SerializeField] FunctionName functionIndex;

    void Start()
    {
        points = new Transform[numberPoints * numberPoints];
        float step = 2f / numberPoints;
        Vector3 scale = Vector3.one * step;

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = Instantiate(pointPrefab, transform);
            points[i].localScale = scale;
        }
    }

    void Update()
    {
        FunctionLibrary.Function function = FunctionLibrary.GetFunction(functionIndex);

        float time = Time.time;
        float step = 2f / numberPoints;
        float v = 0.5f * step - 1;

        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == numberPoints)
            {
                x = 0;
                z++;
                v = (z + 0.5f) * step - 1;
            }
            float u = (x + 0.5f) * step - 1;
            points[i].localPosition = function(u, v, time);
        }
    }
}
