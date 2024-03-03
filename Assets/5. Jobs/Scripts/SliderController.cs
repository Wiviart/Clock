using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI depth;
    [SerializeField] Fractal fractal;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        depth = GetComponentInChildren<TextMeshProUGUI>();
        // fractal = FindObjectOfType<Fractal>();
    }

    void Start()
    {
        slider.minValue = 1;
        slider.maxValue = fractal.maxDepth;
        slider.wholeNumbers = true;
    }

    private void OnSliderValueChanged(float arg0)
    {
        fractal.depth = (int)arg0;
        depth.text = arg0.ToString();
        fractal.UpdateFractal();
    }

    void Update()
    {
        slider.value = fractal.depth;
        depth.text = slider.value.ToString();
    }
}
