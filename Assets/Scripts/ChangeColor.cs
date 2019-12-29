using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ChangeColor : MonoBehaviour, IChangeable {

    Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
    }

    public void ChangeRandom() {
        foreach (Material mat in rend.materials) {
            Color randColor = Random.ColorHSV();
            mat.SetColor("_BaseColor", randColor);
        }
    }
}
