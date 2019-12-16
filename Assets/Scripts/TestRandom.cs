using System.Collections;
using UnityEngine;

public class TestRandom : MonoBehaviour {

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ChangeAllItems();
        }
    }

    void MakeRandom() {
        foreach (IChangeable item in GetComponentsInChildren<IChangeable>()) {
            if (item != null) {
                item.ChangeRandom();
            }
            Camera.main.transform.LookAt(transform.GetChild(0));
            Camera.main.transform.eulerAngles += new Vector3(-15, 0, 0);
        }
    }

    void ChangeAllItems() {
        foreach (GameObject item in FindObjectsOfType<GameObject>()) {
            IChangeable[] changeableItems = item.GetComponents<IChangeable>();
            foreach (IChangeable changeable in changeableItems) {
                if (changeable != null) {
                    changeable.ChangeRandom();
                }
            }
        }
        Transform desiredObject = transform.GetChild(0);
        ChangeCamera.Instance.UpdateCamera(desiredObject);
        ObjectBounds.Instance.UpdateBounds(desiredObject);
    }
}
