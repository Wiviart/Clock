using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameRateCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] Button button;

    int frames;
    float duration, bestDuration = float.MaxValue, worstDuration, averageDuration;
    enum DisplayMode { FPS, MS }
    [SerializeField] DisplayMode display = DisplayMode.FPS;

    [SerializeField, Range(0.1f, 2)] float sampleDuration = 1f;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        button.onClick.AddListener(ChangeDisplayMode);
    }

    void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;
        frames++;
        duration += frameDuration;

        if (frameDuration < bestDuration)
            bestDuration = frameDuration;
        if (frameDuration > worstDuration)
            worstDuration = frameDuration;

        if (duration >= sampleDuration)
        {
            if (display == DisplayMode.FPS)
            {
                text.text = TextDisplay(1 / bestDuration, frames / duration, 1 / worstDuration, 0);
            }
            else
            {
                text.text = TextDisplay(1000 * bestDuration, 1000 * duration / frames, 1000 * worstDuration, 1);
            }
            frames = 0;
            duration = 0;
            bestDuration = float.MaxValue;
            worstDuration = 0;
        }
    }

    string TextDisplay(float max, float aver, float min, int decimals = 0)
    {
        string mode = display == DisplayMode.FPS ? "FPS" : "MS";
        string formatString =
        $"{mode}\n<color=green>{{0:F{decimals}}}</color>\n"
        + $"<color=yellow>{{1:F{decimals}}}</color>\n"
        + $"<color=red>{{2:F{decimals}}}</color>";

        return string.Format(formatString, max, aver, min);

    }

    void ChangeDisplayMode()
    {
        display = display == DisplayMode.FPS ? DisplayMode.MS : DisplayMode.FPS;
    }
}