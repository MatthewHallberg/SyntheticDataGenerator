using UnityEngine;

[RequireComponent(typeof(Light))]
public class ChangeLight : MonoBehaviour, IChangeable {

    [SerializeField]
    float min;
    [SerializeField]
    float max;

    Light changeableLight;

    void Awake() {
        changeableLight = GetComponent<Light>();
    }

    public void ChangeRandom() {
        changeableLight.intensity = Random.Range(min, max);
        changeableLight.color = Random.ColorHSV();
    }
}
