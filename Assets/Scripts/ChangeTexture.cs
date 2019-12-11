using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ChangeTexture : MonoBehaviour, IChangeable {

    Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
    }

    public void ChangeRandom() {
        rend.material.mainTexture = RandoTextures.Instance.GetRandomTexture();
    }
}
