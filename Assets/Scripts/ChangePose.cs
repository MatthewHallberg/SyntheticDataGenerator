using UnityEngine;

public class ChangePose : MonoBehaviour, IChangeable {

    public Vector3 minAngle;
    public Vector3 maxAngle;

    Vector3 randAngle = Vector3.zero;

    public void ChangeRandom() {
        randAngle.x = Mathf.LerpAngle(minAngle.x, maxAngle.x, Random.Range(0f, 1f));
        randAngle.y = Mathf.LerpAngle(minAngle.y, maxAngle.y, Random.Range(0f, 1f));
        randAngle.z = Mathf.LerpAngle(minAngle.z, maxAngle.z, Random.Range(0f, 1f));
        transform.localEulerAngles = randAngle;
    }
}
