using UnityEngine;

[RequireComponent(typeof(Light))]
public class ChangeLight : MonoBehaviour, IChangeable {

    [SerializeField]
    float minIntensity;
    [SerializeField]
    float maxIntensity;

    Light changeableLight;

    void Awake() {
        changeableLight = GetComponent<Light>();
    }

    public void ChangeRandom() {
        changeableLight.intensity = Random.Range(minIntensity, maxIntensity);
        //changeableLight.color = Random.ColorHSV();
    }
}
