using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField] private int numberPoints = 100;
    private Transform[] points;
    private FunctionLibrary.Function function;
    [SerializeField] private FunctionName functionIndex;
    [SerializeField] private float functionDuration = 5f;
    private float timer;
    [SerializeField] private float transitionDuration = 1f;
    private bool isTransitioning;
    private FunctionName transitionFunctionIndex;

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
        timer += Time.deltaTime;
        
        if (isTransitioning)
        {
            if (timer >= transitionDuration)
            {
                timer -= transitionDuration;
                isTransitioning = false;
            }
        }
        else if (timer >= functionDuration)
        {
            timer -= functionDuration;
            isTransitioning = true;
            transitionFunctionIndex = functionIndex;
            functionIndex = FunctionLibrary.GetNextFunctionName(functionIndex);
        }

        if (isTransitioning)
        {
            UpdateFunctionTransition();
        }
        else
            UpdateFunction();
    }

    void UpdateFunction()
    {
        function = FunctionLibrary.GetFunction(functionIndex);

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

    void UpdateFunctionTransition()
    {
        FunctionLibrary.Function from = FunctionLibrary.GetFunction(transitionFunctionIndex);
        FunctionLibrary.Function to = FunctionLibrary.GetFunction(functionIndex);

        float progress = timer / transitionDuration;
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
            points[i].localPosition = FunctionLibrary.Morph(u, v, time, from, to, progress);
        }
    }
}
