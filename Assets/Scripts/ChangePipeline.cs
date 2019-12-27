using UnityEngine;
using UnityEngine.Rendering.LWRP;

public class ChangePipeline : MonoBehaviour, IChangeable {

    public LightweightRenderPipelineAsset asset;

    public void ChangeRandom() {
        asset.renderScale = Random.Range(.2f, 4f);
    }
}
