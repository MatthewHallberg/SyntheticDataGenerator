using UnityEngine;
using UnityEngine.Rendering.LWRP;

public class ChangePipeline : MonoBehaviour, IChangeable {

    public UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset;

    public void ChangeRandom() {
        asset.renderScale = Random.Range(.2f, 4f);
    }
}
