using System.Collections;
using System.IO;
using UnityEngine;

public class RandomController : Singleton<RandomController> {

    static readonly int IMAGES_PER_OBJECT = 50;

    public Transform objectParent;

    int currChild;
    int currImageNum;
    Transform currObject;

    void Start() {
        ActivateCurrChild();
    }

    void Update() {
        ChangeAllItems();
    }

    IEnumerator OnPostRender() {
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

        if (currChild >= objectParent.childCount) {
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log("Training complete!");
            return;
        }

        TakePicture();
    }

    void TakePicture() {
        currImageNum++;

        //create folder for images if it doesn't exist
        string filePath = Application.streamingAssetsPath + "/" + currObject.name;
        if (!File.Exists(filePath)) {
            Directory.CreateDirectory(filePath);
        }

        //take picture
        Debug.Log("taking picture num: " + currImageNum);

        //random image size
        Rect bounds = ObjectBounds.Instance.GetBounds();

        //take photo from portion of screen
        Texture2D photoBounds = new Texture2D((int)bounds.width, (int)bounds.height, TextureFormat.RGB24, false);
        photoBounds.ReadPixels(bounds, 0, 0, false);

        ////take picture from full screen
        //int screenHeight = Screen.height;
        //int screenWidth = Screen.width;
        //Texture2D photoBounds = new Texture2D(screenWidth, screenHeight, TextureFormat.RGB24, false);
        //photoBounds.ReadPixels(new Rect(0, 0, screenWidth, screenHeight), 0, 0, false);

        photoBounds.Apply();
        byte[] data = photoBounds.EncodeToJPG(80);
        DestroyImmediate(photoBounds);
        File.WriteAllBytes(filePath + "/" + currImageNum + ".jpg", data);

        if (currImageNum >= IMAGES_PER_OBJECT) {
            currChild++;
            ActivateCurrChild();
        }
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
            }
            currObject = objectParent.GetChild(currChild);
            ChangeCamera.Instance.UpdateCamera(currObject);
            ObjectBounds.Instance.UpdateBounds(currObject, true);
        }
    }
}
