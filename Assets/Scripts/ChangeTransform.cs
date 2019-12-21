using UnityEngine;

public class ChangeTransform : MonoBehaviour, IChangeable {

    [SerializeField]
    Vector3 minPosition;
    [SerializeField]
    Vector3 maxPosition;
    [SerializeField]
    Vector3 minAngle;
    [SerializeField]
    Vector3 maxAngle;

    public void ChangeRandom() {

        //handle position
        Vector3 randPos;
        randPos.x = Random.Range(minPosition.x, maxPosition.x);
        randPos.y = Random.Range(minPosition.y, maxPosition.y);
        randPos.z = Random.Range(minPosition.z, maxPosition.z);
        transform.localPosition = randPos;

        //handle random rotation
        Vector3 randAngle;
        randAngle.x = Random.Range(minAngle.x, maxAngle.x);
        randAngle.y = Random.Range(minAngle.y, maxAngle.y);
        randAngle.z = Random.Range(minAngle.z, maxAngle.z);
        transform.localEulerAngles = randAngle;
    }
}
