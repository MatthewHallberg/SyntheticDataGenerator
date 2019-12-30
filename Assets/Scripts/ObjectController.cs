using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectController : Singleton<ObjectController> {

    const float PERCENT_OVERLAP = .40f;

    public void ActivateObjects() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
    }

    public Dictionary<GameObject, Rect> GetObjects() {
        CheckForOverlap();
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

    void CheckForOverlap() {

        Dictionary<GameObject, Rect> currObjects = GetActiveObjects();

        //shuffle dict
        System.Random rand = new System.Random();
        currObjects = currObjects.OrderBy(x => rand.Next())
          .ToDictionary(item => item.Key, item => item.Value);

        //deactivate overlapping boxes
        foreach (KeyValuePair<GameObject, Rect> obj1 in currObjects) {
            if (obj1.Key.activeSelf) {
                foreach (KeyValuePair<GameObject, Rect> obj2 in currObjects) {
                    if (obj1.Key != obj2.Key && obj1.Key.activeSelf && isOverlapping(obj1.Value, obj2.Value)) {
                        obj2.Key.SetActive(false);
                    }
                }
            }
        }
    }

    bool isOverlapping(Rect rect1, Rect rect2) {
        if (rect1.Overlaps(rect2)) {

            float area1 = Mathf.Abs(rect1.min.x - rect1.max.x) * Mathf.Abs(rect1.min.y - rect1.max.y);
            float area2 = Mathf.Abs(rect2.min.x - rect2.max.x) * Mathf.Abs(rect2.min.y - rect2.max.y);

            float areaI = (Mathf.Min(rect1.max.x, rect2.max.x) - Mathf.Max(rect1.min.x, rect2.min.x)) * (Mathf.Min(rect1.max.y, rect2.max.y) - Mathf.Max(rect1.min.y, rect2.min.y));
            float percentOverlap = areaI / Mathf.Min(area1, area2);

            return percentOverlap > PERCENT_OVERLAP;
        } else {
            return false;
        }
    }
}
