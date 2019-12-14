using UnityEngine;

public class TestRandom : MonoBehaviour {

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            MakeRandom();
        }
    }

    void MakeRandom() {
        foreach (IChangeable item in GetComponents<IChangeable>()) {
            if (item != null) {
                item.ChangeRandom();
            }
        }
    }
}
