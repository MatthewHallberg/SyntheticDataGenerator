using System.Collections;
using UnityEngine;

public class RandomController : MonoBehaviour {

    void Start() {
        StartCoroutine(RandomRoutine());
    }

    IEnumerator RandomRoutine() {
        while (true) {
            ChangeAllItems();
            yield return new WaitForSeconds(.5f);
        }
    }

    void ChangeAllItems() {
        foreach (GameObject item in FindObjectsOfType<GameObject>()) {
            IChangeable changeableItem = item.GetComponent<IChangeable>();
            if (changeableItem != null) {
                changeableItem.ChangeRandom();
            }
        }
    }
}
