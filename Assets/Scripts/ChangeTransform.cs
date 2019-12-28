using UnityEngine;

public class ChangeTransform : MonoBehaviour, IChangeable {

    const int MAX_ANGLE = 15;

    Vector3 startPosition;
    Vector3 startAngle;

    void Start() {
        startPosition = transform.localPosition;
        startAngle = transform.localEulerAngles;
    }

    public void ChangeRandom() {

        //choose random position on screen
        Vector3 randScreenPos = new Vector3(Random.Range(.15f, .85f), Random.Range(.4f, 1.2f), startPosition.z + Random.Range(0, 6));
        transform.position = Camera.main.ViewportToWorldPoint(randScreenPos);

        //handle random rotation
        Vector3 randAngle;
        randAngle.x = startAngle.x + Random.Range(-MAX_ANGLE, MAX_ANGLE);
        randAngle.y = startAngle.y + Random.Range(-MAX_ANGLE, MAX_ANGLE);
        randAngle.z = startAngle.z + Random.Range(-MAX_ANGLE, MAX_ANGLE);
        transform.localEulerAngles = randAngle;
    }
}
