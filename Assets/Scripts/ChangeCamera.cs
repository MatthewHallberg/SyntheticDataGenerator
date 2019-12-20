using UnityEngine;

public class ChangeCamera : Singleton<ChangeCamera>, IChangeable {

    [SerializeField]
    Vector3 minPosition;
    [SerializeField]
    Vector3 maxPosition;

    public void ChangeRandom() {

        //handle position
        Vector3 randPos;
        randPos.x = Random.Range(minPosition.x, maxPosition.x);
        randPos.y = Random.Range(minPosition.y, maxPosition.y);
        randPos.z = Random.Range(minPosition.z, maxPosition.z);
        transform.localPosition = randPos;
    }

    public void UpdateCamera(Transform currTransform) {
        Transform center = currTransform.Find("center");
        Camera.main.transform.LookAt(center);

        //rotate random on y axis
        //Vector3 randAngle = transform.localEulerAngles;
        //randAngle.z = Random.Range(0, 360);
        //transform.localEulerAngles = randAngle;
    }
}
