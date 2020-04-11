using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ChangeTexture : MonoBehaviour, IChangeable {

    RawImage backgroundImage;

    void Start() {
        backgroundImage = GetComponent<RawImage>();
    }
    public void ChangeRandom() {
        backgroundImage.material.mainTexture = RandoTextures.Instance.GetRandomTexture();
        backgroundImage.SetMaterialDirty();
    }
}
