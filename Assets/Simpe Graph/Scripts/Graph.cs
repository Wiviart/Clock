using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] Transform pointPrefab;
    [SerializeField] int numberPoints = 11;
    Vector3 position;
    Transform[] points;

    void Start()
    {
        points = new Transform[numberPoints];

        Vector3 scale = Vector3.one / 10f;
        for (int i = 0; i < numberPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform);
            position.x = (i - numberPoints / 2) / 5f;
            // position.y = Mathf.Pow(position.x, 2f);
            points[i].localPosition = position / 5f;
            points[i].localScale = scale;
        }
    }

    void Update()
    {
        for (int i = 0; i < numberPoints; i++)
        {
            Transform point = points[i];
            position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
            point.localPosition = position;
        }
    }
}
