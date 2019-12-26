using UnityEngine;

public class ChangeToon : MonoBehaviour, IChangeable {

    public Material toonMat;

    public void ChangeRandom() {
        toonMat.SetInt("_PosterizationCount", Random.Range(2, 10));
    }
}
