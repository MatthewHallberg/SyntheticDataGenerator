using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectController : Singleton<ObjectController> {

    public float minDistance;

    public void ActivateObjects() {
        //make sure at least one child gets activated
        int desiredChild = Random.Range(0, transform.childCount);
        foreach (Transform child in transform) {
            if (child.GetSiblingIndex() == desiredChild) {
                child.gameObject.SetActive(true);
            } else {
                child.gameObject.SetActive(1 == Random.Range(0, 2));
            }
        }
    }

    public Dictionary<GameObject, Rect> GetObjects() {
        DeactivateOverlap();
        return GetActiveObjects();
    }

    Dictionary<GameObject, Rect> GetActiveObjects() {
        //add all active objects to a dict
        Dictionary<GameObject, Rect> currObjects = new Dictionary<GameObject, Rect>();

        foreach (Transform child in transform) {
            if (child.gameObject.activeSelf) {
                currObjects.Add(child.gameObject, child.GetComponent<ObjectBounds>().GetBounds());
            }
        }

        return currObjects;
    }

    void DeactivateOverlap() {

        Dictionary<GameObject, Rect> currObjects = GetActiveObjects();

        //shuffle dict
        System.Random rand = new System.Random();
        currObjects = currObjects.OrderBy(x => rand.Next())
          .ToDictionary(item => item.Key, item => item.Value);

        //deactivate overlapping boxes
        foreach (KeyValuePair<GameObject, Rect> obj1 in currObjects) {
            if (obj1.Key.activeSelf) {
                foreach (KeyValuePair<GameObject, Rect> obj2 in currObjects) {
                    if (obj1.Key != obj2.Key && obj1.Key.activeSelf && obj1.Value.Overlaps(obj2.Value)) {
                        obj2.Key.SetActive(false);
                    }
                }
            }
        }
    }
}
