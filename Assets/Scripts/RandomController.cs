using System.Collections;
using System.IO;
using UnityEngine;

public class RandomController : Singleton<RandomController> {

    static readonly int IMAGES_PER_OBJECT = 300;

    public Transform objectParent;

    int currChild;
    int currImageNum;
    Transform currObject;

    void Start() {
        ActivateCurrChild();
    }

    void Update() {
        ChangeAllItems();
        StartCoroutine(DelayPicture());
    }

    IEnumerator DelayPicture() {
        yield return new WaitForEndOfFrame();
        CheckForPicture();
    }

    void ActivateCurrChild() {

        currImageNum = 0;

        foreach (Transform child in objectParent) {
            if (child.GetSiblingIndex() == currChild) {
                child.gameObject.SetActive(true);
            } else {
                child.gameObject.SetActive(false);
            }
        }
    }

    void CheckForPicture() {

        //if (currChild >= objectParent.childCount) {
        //    UnityEditor.EditorApplication.isPlaying = false;
        //    Debug.Log("Training complete!");
        //    return;
        //}

        //currImageNum++;

        ////create folder for images if it doesn't exist
        //string filePath = Application.streamingAssetsPath + "/" + currObject.name;
        //if (!File.Exists(filePath)) {
        //    Directory.CreateDirectory(filePath);
        //}

        ////take picture
        //Debug.Log("taking picture num: " + currImageNum);

        ////random image size
        //Rect bounds = ObjectBounds.Instance.GetBounds();

        ////make sure we dont go off screen
        //bounds.yMin = Mathf.Max(0, bounds.yMin);
        //bounds.xMin = Mathf.Max(0, bounds.xMin);
        //bounds.yMax = Mathf.Min(Screen.height, bounds.yMax);
        //bounds.xMax = Mathf.Min(Screen.width, bounds.xMax);

        ////Texture2D photo = TakeCroppedPicture(bounds);
        //Texture2D photo = TakeFullScreenPicture(bounds);

        //photo.Apply();
        //byte[] data = photo.EncodeToJPG(80);
        //DestroyImmediate(photo);
        //File.WriteAllBytes(filePath + "/" + currImageNum + ".jpg", data);

        //if (currImageNum >= IMAGES_PER_OBJECT) {
        //    currChild++;
        //    ActivateCurrChild();
        //}
    }

    Texture2D TakeCroppedPicture(Rect bounds) {
        Texture2D photo = new Texture2D((int)bounds.width, (int)bounds.height, TextureFormat.RGB24, false);
        photo.ReadPixels(bounds, 0, 0, false);
        return photo;
    }

    Texture2D TakeFullScreenPicture(Rect bounds) {
        int screenHeight = Screen.height;
        int screenWidth = Screen.width;
        Texture2D photo = new Texture2D(screenWidth, screenHeight, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, screenWidth, screenHeight), 0, 0, false);
        return photo;
    }

    void ChangeAllItems() {

        if (currChild < objectParent.childCount) {

            foreach (GameObject item in FindObjectsOfType<GameObject>()) {
                IChangeable[] changeableItems = item.GetComponents<IChangeable>();
                foreach (IChangeable changeable in changeableItems) {
                    if (changeable != null) {
                        changeable.ChangeRandom();
                    }
                }
                ObjectBounds bounds = item.GetComponent<ObjectBounds>();
                if (bounds != null) {
                    bounds.UpdateBounds();
                }
            }
        }
    }
}
