using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ChangeColor : MonoBehaviour, IChangeable {

    public Color max;

    Color min = Color.white;
    Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
    }

    public void ChangeRandom() {
        foreach (Material mat in rend.materials) {
            //do albedo
            float randT = Random.Range(0f, 1f);
            Color randColor = Color.Lerp(min, max, randT);
            mat.SetColor("_BaseColor", randColor);
        }
    }
}
