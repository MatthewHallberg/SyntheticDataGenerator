using UnityEngine;

public class CollisionBehavior : MonoBehaviour {

    void OnTriggerEnter(Collider col) {
        gameObject.SetActive(false);
    }
}
