using UnityEngine;

public class ChangeTransform : MonoBehaviour, IChangeable {

    [SerializeField]
    float minScale;
    [SerializeField]
    float maxScale;
    [SerializeField]
    Vector3 minPosition;
    [SerializeField]
    Vector3 maxPosition;

    public void ChangeRandom() {
        //handle scale
        float rand = Random.Range(minScale, maxScale);
        transform.localScale = Vector3.one * rand;

        //handle scale
        Vector3 randPos;
        randPos.x = Random.Range(minPosition.x, maxPosition.x);
        randPos.y = Random.Range(minPosition.y, maxPosition.y);
        randPos.z = Random.Range(minPosition.z, maxPosition.z);
        transform.localPosition = randPos;
    }
}
