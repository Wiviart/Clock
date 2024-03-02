using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUGraph : MonoBehaviour
{
    const int maxResolution = 1000;
    [SerializeField, Range(10, maxResolution)] private int numberPoints = 100;
    private FunctionLibrary.Function function;
    [SerializeField] private FunctionName functionIndex;
    private FunctionName transitionFunctionIndex;
    [SerializeField] private float functionDuration = 5f;
    private float timer;
    [SerializeField] private float transitionDuration = 1f;
    private bool isTransitioning;
    ComputeBuffer positionBuffer;
    [SerializeField] ComputeShader computeShader;
    static readonly int
    positionsId = Shader.PropertyToID("_Positions"),
    resolutionId = Shader.PropertyToID("_Resolution"),
    stepId = Shader.PropertyToID("_Step"),
    timeId = Shader.PropertyToID("_Time"),
    transitionProgressId = Shader.PropertyToID("_TransitionProgress");
    [SerializeField] Material material;
    [SerializeField] Mesh mesh;

    void Awake()
    {
        positionBuffer = new ComputeBuffer(maxResolution * maxResolution, 3 * 4);
    }

    void OnEnable()
    {
        positionBuffer = new ComputeBuffer(maxResolution * maxResolution, 3 * 4);
    }

    void OnDisable()
    {
        positionBuffer.Release();
        positionBuffer = null;
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

        UpdateFunctionOnGPU();
    }

    void UpdateFunctionOnGPU()
    {
        float step = 2f / numberPoints;
        computeShader.SetInt(resolutionId, numberPoints);
        computeShader.SetFloat(stepId, step);
        computeShader.SetFloat(timeId, Time.time);

        if (isTransitioning)
        {
            computeShader.SetFloat(
                transitionProgressId,
                Mathf.SmoothStep(0f, 1f, timer / transitionDuration)
            );
        }

        var kernelIndex =
            (int)functionIndex + (int)(isTransitioning ? transitionFunctionIndex : functionIndex) * FunctionLibrary.FunctionCount;

        computeShader.SetBuffer(kernelIndex, positionsId, positionBuffer);

        int groups = Mathf.CeilToInt(numberPoints / 8f);
        computeShader.Dispatch(kernelIndex, groups, groups, 1);

        material.SetBuffer(positionsId, positionBuffer);
        material.SetFloat(stepId, step);

        var bounds = new Bounds(Vector3.zero, Vector3.one * (2f + 2f / numberPoints));
        Graphics.DrawMeshInstancedProcedural(
            mesh, 0, material, bounds, numberPoints * numberPoints);
    }
}
