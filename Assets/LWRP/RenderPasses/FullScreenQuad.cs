namespace UnityEngine.Rendering.LWRP
{
    public class FullScreenQuad : UnityEngine.Rendering.Universal.ScriptableRendererFeature
    {
        [System.Serializable]
        public struct FullScreenQuadSettings
        {
            public UnityEngine.Rendering.Universal.RenderPassEvent renderPassEvent;
            public Material material;
        }

        public FullScreenQuadSettings m_Settings;
        FullScreenQuadPass m_RenderQuadPass;

        public override void Create()
        {
            m_RenderQuadPass = new FullScreenQuadPass(m_Settings);
        }

        public override void AddRenderPasses(UnityEngine.Rendering.Universal.ScriptableRenderer renderer, ref UnityEngine.Rendering.Universal.RenderingData renderingData)
        {
            if (m_Settings.material != null)
                renderer.EnqueuePass(m_RenderQuadPass);
        }
    }
}
