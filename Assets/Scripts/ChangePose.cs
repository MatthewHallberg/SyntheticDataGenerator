using UnityEngine;

public class ChangePose : MonoBehaviour, IChangeable {

    const float MOVE_AMOUNT = 5f;

    public void ChangeRandom() {
        //go through all child transforms recursively and slighty change pose
        foreach (Transform trans in GetComponentsInChildren<Transform>()) {
            trans.localEulerAngles -= Vector3.one * Random.Range(-MOVE_AMOUNT, MOVE_AMOUNT);
        }
    }
}
