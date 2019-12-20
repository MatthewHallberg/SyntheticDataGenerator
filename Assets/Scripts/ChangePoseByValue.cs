using UnityEngine;

public class ChangePoseByValue : MonoBehaviour, IChangeable {

    [SerializeField]
    float maxVal;

    Vector3 startAngle = Vector3.zero;

    void Awake() {
        startAngle = transform.localEulerAngles;
    }

    public void ChangeRandom() {
        transform.localEulerAngles = startAngle;
        Vector3 randAngle = startAngle;
        randAngle.x += Random.Range(-maxVal, maxVal);
        randAngle.y += Random.Range(-maxVal, maxVal);
        randAngle.z += Random.Range(-maxVal, maxVal);
        transform.localEulerAngles = randAngle;
    }
}
