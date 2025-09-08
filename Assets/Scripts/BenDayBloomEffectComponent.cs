using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[VolumeComponentMenu("Custom/Ben Day Bloom")]
[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
public class BenDayBloomEffectComponent : VolumeComponent, IPostProcessComponent
{
    [Header("Bloom Settings")]
    public FloatParameter threshold = new FloatParameter(0.9f);
    public FloatParameter intensity = new FloatParameter(1f);
    public ClampedFloatParameter scatter = new ClampedFloatParameter(0.7f, 0f, 1f);
    public IntParameter clamp = new IntParameter(65472);
    public ClampedIntParameter maxIterations = new ClampedIntParameter(6, 0, 10);

    [Header("Ben-Day Dots")]
    public IntParameter dotsDensity = new IntParameter(10);
    public ClampedFloatParameter dotsCutOff = new ClampedFloatParameter(0.4f, 0f, 1f);
    public Vector2Parameter scrollDirection = new Vector2Parameter(Vector2.zero);

    public bool IsActive() => true;
    public bool IsTileCompatible() => false;
}
