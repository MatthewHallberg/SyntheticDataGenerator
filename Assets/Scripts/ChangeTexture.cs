using UnityEngine;
using UnityEngine.UI;

public class ChangeTexture : MonoBehaviour, IChangeable {

    public Material backgroundMat;

    public void ChangeRandom() {
        backgroundMat.SetTexture("_BaseMap", RandoTextures.Instance.GetRandomTexture());
    }
}
