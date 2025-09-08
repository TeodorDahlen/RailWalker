using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomPostProcessPass : ScriptableRenderPass
{
    private Material m_bloomMaterial;
    private Material m_compositeMaterial;

    public CustomPostProcessPass(Material bloomMat, Material compositeMat)
    {
        m_bloomMaterial = bloomMat;
        m_compositeMaterial = compositeMat;
        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (m_bloomMaterial == null || m_compositeMaterial == null)
            return;

        var stack = VolumeManager.instance.stack;
        var bloom = stack.GetComponent<BenDayBloomEffectComponent>();
        if (bloom == null) return;

        RTHandle colorTarget = renderingData.cameraData.renderer.cameraColorTargetHandle;
        RTHandle depthTarget = renderingData.cameraData.renderer.cameraDepthTargetHandle;

        CommandBuffer cmd = CommandBufferPool.Get("BenDayBloom");

        // Set shader properties
        m_compositeMaterial.SetFloat("_CutOff", bloom.dotsCutOff.value);
        m_compositeMaterial.SetFloat("_Density", bloom.dotsDensity.value);
        m_compositeMaterial.SetVector("_Direction", bloom.scrollDirection.value);

        // Basic blit: bloom then composite
        Blitter.BlitCameraTexture(cmd, colorTarget, depthTarget, m_bloomMaterial, 0);
        Blitter.BlitCameraTexture(cmd, colorTarget, depthTarget, m_compositeMaterial, 0);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}
