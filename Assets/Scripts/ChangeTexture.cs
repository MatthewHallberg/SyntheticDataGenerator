using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ChangeTexture : MonoBehaviour, IChangeable {

    RawImage rawImage;

    void Start() {
        rawImage = GetComponent<RawImage>();
    }

    public void ChangeRandom() {
        rawImage.texture = RandoTextures.Instance.GetRandomTexture();
    }
}
