using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] Transform pointPrefab;
    [SerializeField] int numberPoints = 11;
    // Vector3 position;
    Transform[] points;
    [SerializeField] FunctionLibrary.FunctionName functionIndex;

    void Start()
    {
        points = new Transform[numberPoints * numberPoints];
        Vector3 scale = Vector3.one / 10f;
        float step = scale.x * 5;
        Vector3 position = Vector3.zero;

        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == numberPoints)
            {
                x = 0;
                z++;
            }

            points[i] = Instantiate(pointPrefab, transform);
            position.x = (x - numberPoints / 2f) * step;
            position.z = (z - numberPoints / 2f) * step;
            // position.y = Mathf.Pow(position.x, 2f);
            points[i].localPosition = position / 10f;
            points[i].localScale = scale;
        }
    }

    void Update()
    {
        FunctionLibrary.Function function = FunctionLibrary.GetFunction(functionIndex);

        float time = Time.time;

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;

            position.y = function(position.x, position.z, time);

            point.localPosition = position;
        }
    }
}
