using UnityEngine;

public class ChangeTransform : MonoBehaviour, IChangeable {

    const int MAX_ANGLE = 20;

    Vector3 startPosition;
    Vector3 startAngle;

    void Start() {
        startPosition = transform.localPosition;
        startAngle = transform.localEulerAngles;
    }

    public void ChangeRandom() {

        //choose random position on screen (these numbers are not 1 -0 because the origin of my models was not in the center and I never change it)
        Vector3 randScreenPos = new Vector3(Random.Range(.1f, .9f), Random.Range(.6f, .9f), startPosition.z + Random.Range(.5f, 4.5f));
        transform.position = Camera.main.ViewportToWorldPoint(randScreenPos);

        //handle random rotation
        Vector3 randAngle;
        randAngle.x = startAngle.x + Random.Range(-MAX_ANGLE, MAX_ANGLE);
        randAngle.y = startAngle.y + Random.Range(-MAX_ANGLE, MAX_ANGLE);
        randAngle.z = startAngle.z + Random.Range(-MAX_ANGLE, MAX_ANGLE);
        transform.localEulerAngles = randAngle;
    }
}
