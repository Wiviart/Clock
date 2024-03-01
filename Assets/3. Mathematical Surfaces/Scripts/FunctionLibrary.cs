using UnityEngine;
using UnityEngine.Video;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);
    static Function[] functions =
    {
        Wave,
        MultiWave,
        Ripple,
        Sphere,
        ScalingSphere,
        VerticalBandsSphere,
        HorizontalBandsSphere,
        TwistingSphere,
        PulledSphere,
        HeartCylinder,
        TwistingUnknown,
        TwistingUnknown2,
        TwistingUnknown3,
        TwistingUnknown4,
        TwistingUnknown5,
        TwistingUnknown6,
        TwistingUnknown7,
        TwistingUnknown8,
        TwistingUnknown9,
        TwistingUnknown10,
        SpindleTorus,
        RingTorus,
        TwistingTorus
    };

    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
    }

    public static FunctionName GetNextFunctionName(FunctionName name)
    {
        return (int)name < functions.Length - 1 ? name + 1 : 0;
    }

    public static Vector3 Morph(float u, float v, float t, Function from, Function to, float progress)
    {
        return Vector3.LerpUnclamped(from(u, v, t), to(u, v, t), SmoothStep(0f, 1f, progress));
    }

    static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;
    }

    static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + 0.5f * t));
        p.y += Sin(2f * PI * (v + t)) * 0.5f;
        p.y += Sin(PI * (u + v + 0.25f * t));
        p.y *= 2f / 5f;
        p.z = v;
        return p;
    }

    static Vector3 Ripple(float u, float v, float t)
    {
        float d = Sqrt(u * u + v * v);
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = v;
        return p;
    }

    static Vector3 Sphere(float u, float v, float t)
    {
        float r = Cos(0.5f * PI * v);
        Vector3 p;
        p.x = Sin(PI * u) * r;
        p.y = Sin(PI * v * 0.5f);
        p.z = Cos(PI * u) * r;
        return p;
    }

    static Vector3 ScalingSphere(float u, float v, float t)
    {
        float r = 0.5f * (1 + Sin(PI * t));
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 VerticalBandsSphere(float u, float v, float t)
    {
        float r = (9f + Sin(8f * PI * u)) * 0.1f;
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 HorizontalBandsSphere(float u, float v, float t)
    {
        float r = (9f + Sin(8f * PI * v)) * 0.1f;
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 TwistingSphere(float u, float v, float t)
    {
        float r = 0.5f + Sin(PI * (6f * u + 4f * v + t)) * 0.1f;
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 PulledSphere(float u, float v, float t)
    {
        float r = 1f;
        float s = 0.5f + r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 HeartCylinder(float u, float v, float t)
    {
        float r = (9f + Sin(8f * PI * v)) * 0.1f;
        float s = r * Cos(0.5f * PI * u);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 TwistingUnknown(float u, float v, float t)
    {
        float r = (9 + Sin(PI * (6f * u + 4f * v + t))) * 0.1f;
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 TwistingUnknown2(float u, float v, float t)
    {
        float r = (9 + Sin(PI * (6f * u + 4f * v + t))) * 0.1f;
        float s = r * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 TwistingUnknown3(float u, float v, float t)
    {
        float r = 0.5f + Sin(PI * (3f * u + 3f * v + t));
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = r * Sin(PI * u);
        p.y = s * Sin(0.1f * PI * t);
        p.z = r * Cos(PI * u);
        return p;
    }

    static Vector3 TwistingUnknown4(float u, float v, float t)
    {
        float r = 1 + Sin(PI * (6f * u + 4f * v + t)) * 0.1f;
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * v);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * v);
        return p;
    }

    static Vector3 TwistingUnknown5(float u, float v, float t)
    {
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * t);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 TwistingUnknown6(float u, float v, float t)
    {
        float r = 0.5f + Sin(PI * (6f * u + t)) * 0.2f;
        float s = r * Cos(0.5f * PI * v);

        Vector3 p;
        p.x = s * Sin(2f * PI * u);
        p.y = r * Sin(PI * v);
        p.z = s * Cos(2f * PI * u);

        return p;
    }

    static Vector3 TwistingUnknown7(float u, float v, float t)
    {
        float r = 0.5f + Sin(PI * (6f * u + 4f * v + t)) * 0.1f;
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * v);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 TwistingUnknown8(float u, float v, float t)
    {
        float r = 0.5f + Sin(PI * (6f * u + 4f * v + t)) * 0.1f;
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = r * Sin(PI * u);
        p.y = s * Sin(0.5f * PI * v);
        p.z = r * Cos(PI * u);
        return p;
    }

    static Vector3 TwistingUnknown9(float u, float v, float t)
    {
        float r = Sin(8f * PI * v + t);
        float s = r + Cos(0.5f * PI * v);
        Vector3 p;
        p.x = r * Sin(PI * u);
        p.y = s * Sin(0.5f * PI * v);
        p.z = r * Cos(PI * u);
        return p;
    }

    static Vector3 TwistingUnknown10(float u, float v, float t)
    {
        float r = (9f + Sin(8f * PI * u + v * PI * 4f + t)) * 0.1f;
        float s = r * Cos(0.5f * PI * v + t);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 SpindleTorus(float u, float v, float t)
    {
        float r = 1f;
        float s = 0.5f + r * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 RingTorus(float u, float v, float t)
    {
        float r1 = 0.75f;
        float r2 = 0.25f;
        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    static Vector3 TwistingTorus(float u, float v, float t)
    {
        float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}

public enum FunctionName
{
    Wave,
    MultiWave,
    Ripple,
    Sphere,
    ScalingSphere,
    VerticalBandsSphere,
    HorizontalBandsSphere,
    TwistingSphere,
    PulledSphere,
    HeartCylinder,
    TwistingUnknown,
    TwistingUnknown2,
    TwistingUnknown3,
    TwistingUnknown4,
    TwistingUnknown5,
    TwistingUnknown6,
    TwistingUnknown7,
    TwistingUnknown8,
    TwistingUnknown9,
    TwistingUnknown10,
    SpindleTorus,
    RingTorus,
    TwistingTorus
}
