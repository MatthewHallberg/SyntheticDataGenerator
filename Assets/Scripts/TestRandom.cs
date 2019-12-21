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
        ObjectController.Instance.ActivateObjects();
        StartCoroutine(DelayBounds());
    }

    IEnumerator DelayBounds() {
        yield return new WaitForEndOfFrame();
        foreach (ObjectBounds bounds in FindObjectsOfType<ObjectBounds>()) {
            bounds.UpdateBounds();
        }
        ObjectController.Instance.GetObjects();
    }
}
