using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button switchButton;
    [SerializeField] TextMeshProUGUI modeText;

    void Awake()
    {
        switchButton.onClick.AddListener(SwitchMode);
    }

    void Start()
    {
        SwitchMode();
    }

    void SwitchMode()
    {
        modeText.text = Clock.Instance.ToggleAutomaticWatches();
    }
}
