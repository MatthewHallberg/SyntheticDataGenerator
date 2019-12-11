using UnityEngine;

public class ObjectToTrain : MonoBehaviour {

    [SerializeField]
    Renderer rend;

    public bool IsVisible() {
        return rend.isVisible;
    }
}
